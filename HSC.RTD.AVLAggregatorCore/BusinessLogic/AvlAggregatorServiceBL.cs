using System;
using HSC.RTD.AVLAggregatorCore.Data;
using HSC.RTD.AVLAggregatorCore.Data.POCO;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HSC.RTD.AVLAggregatorCore.BusinessLogic
{
    public class AvlAggregatorServiceBL : IAvlAggregatorServiceBL
    {
        protected readonly object _lock = new object();
        protected IAvlRepository Repo;
        protected string ServiceName;
        protected IAvlConfiguration Configuration;
        protected ICachedDictionary<string, Session> ActiveSessions;

        public AvlAggregatorServiceBL(IAvlRepository repo, string serviceName, IAvlConfiguration config, ICachedDictionary<string, Session> sessions)
        {
            this.Repo = repo;
            this.ServiceName = serviceName;
            this.Configuration = config;
            this.ActiveSessions = sessions;
        }

        public LastPositionResponseTypeMessage LastPosition(LastPositionRequestTypeMessage message)
        {
            var response = new LastPositionResponseTypeMessage() { Header = new Header() {
                                                                                            MessageIDSpecified = true,
                                                                                            SessionIDSpecified = true,
                                                                                            MessageID = message.Header.MessageID,
                                                                                            SessionID = message.Header.SessionID },
                                                                  Body = new LastPositionResponseTypeMessageBody() {
                                                                                            LastPositionResponse = new LastPositionResponseTypeMessageBodyLastPositionResponse() { VehicleList = new LastPositionResponseTypeMessageBodyLastPositionResponseVehicle[] { } } }};
            var sess = this.ActiveSessions[message.Header.SessionID.ToString()];

            if (sess != null && sess.Status == Enums.SessionStatus.Active)
            {
                if ((sess.Roles & Enums.ServiceAccountRole.Consumer) == 0)
                {
                    response.Body.LastPositionResponse.Status = false;
                    response.Body.LastPositionResponse.Description = "Unauthorized.";
                    return response;
                }

                IEnumerable<Position> positions = Repo.GetChangedPositionsBySessionId(sess.ServiceAccountId, int.Parse(Configuration["MaxFeedAgeSec"]));
                var vList = positions.Select(x => new LastPositionResponseTypeMessageBodyLastPositionResponseVehicle()
                {
                    Bearing = x.Direction,
                    DateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(x.AvlDateTime, "UTC", sess.TimeZone).ToString("MM/dd/yyyy hh:mm:ss tt"),                   
                    Datum = this.Configuration["Datum"],
                    ID = x.Address,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    Speed = x.Velocity,
                    VehicleName = x.VehicleName,
                    Rates = new LastPositionResponseTypeMessageBodyLastPositionResponseVehicleRates() {
                        Rate1 = "0.0000",
                        Rate2 = "0.0000",
                        Rate3 = "0.0000",
                        Rate4 = "0.0000",
                        Rate5 = "0.0000",
                        Rate6 = "0.0000",
                        Rate7 = "0.0000",
                        Rate8 = "0.0000"
                    },
                    IP = "255.255.255.255"
                });
                response.Body.LastPositionResponse.VehicleList = vList.ToArray();
                response.Body.LastPositionResponse.Status = true;
                response.Body.LastPositionResponse.Description = "Success";
            }
            else
            {
                response.Body.LastPositionResponse.Status = false;
                response.Body.LastPositionResponse.Description = "Customer name not logged in";
            }
            return response;
        }

        public LoginResponseTypeMessage Login(LoginRequestTypeMessage message)
        {
            var response = this.GetDefaultLoginResponseMessage();
            response.Header.MessageID = message.Header.MessageID;
            if (!string.IsNullOrEmpty(message.Body.LoginRequest.Password) && !string.IsNullOrEmpty(message.Body.LoginRequest.CustomerName))
            {
                var user = Repo.GetServiceAccountByName(message.Body.LoginRequest.CustomerName);
                if (user != null && user.Password == message.Body.LoginRequest.Password)
                {
                    var session = this.ActiveSessions.Find(x => x.Value.ServiceAccountId == user.Id && x.Value.ClientIp.Equals(message.Body.LoginRequest.ClientIP, StringComparison.InvariantCultureIgnoreCase));
                    if (session == null)
                    {
                        session = Repo.NewSession(user.Id, message.Body.LoginRequest.ClientIP);
                        this.ActiveSessions.Clear(); //refresh the list of Active Sessions
                        response.Body.LoginResponse.Description = "Success";
                    }
                    else
                    {
                        response.Body.LoginResponse.Description = "Customer name already logged in";
                    }
                    response.Body.LoginResponse.Status = true;
                    response.Header.SessionID = session.Id;
                    return response;
                }
            }
            response.Body.LoginResponse.Status = false;
            response.Body.LoginResponse.Description = "Invalid Customer Name/Incorrect Password";
            return response; 
        }

        public LogoutResponseTypeMessage Logout(LogoutRequestTypeMessage message)
        {
            var response = new LogoutResponseTypeMessage() { Header = new Header() { MessageIDSpecified = true, MessageID = message.Header.MessageID, SessionIDSpecified = true, SessionID = message.Header.SessionID }, Body = new LogoutResponseTypeMessageBody() { LogoutResponse = new LogoutResponseTypeMessageBodyLogoutResponse()} };

            var session = Repo.GetSessionByClientIPName(message.Body.LogoutRequest.ClientIP, message.Body.LogoutRequest.CustomerName);
            if (session != null && session.Status == Enums.SessionStatus.Active && session.ClientIp.Equals(message.Body.LogoutRequest.ClientIP, StringComparison.InvariantCultureIgnoreCase) )
            {
                session = Repo.UpdateSession(session.Id, Enums.SessionStatus.Closed);
                lock (_lock)
                {
                    ActiveSessions.Clear();
                }
                if (session.Status == Enums.SessionStatus.Closed)
                {
                    response.Body.LogoutResponse.Status = true;
                    response.Body.LogoutResponse.Description = "Success";
                }
                else
                {
                    response.Body.LogoutResponse.Status = true;
                    response.Body.LogoutResponse.Description = "Wasn't able to close the session. Please try again.";
                }
            }
            else
            {
                response.Body.LogoutResponse.Status = false;
                response.Body.LogoutResponse.Description = "Customer name not logged in";
            }
            return response;
        }

        public ExportDataResponseTypeMessage ExportData(ExportDataRequestTypeMessage message)
        {
            var response = new ExportDataResponseTypeMessage() { Header = new Header() { MessageIDSpecified = true, SessionIDSpecified = true, MessageID = message.Header.MessageID, SessionID = message.Header.SessionID } , Body = new ExportDataResponseTypeMessageBody() { DataInsertResponse = new ExportDataResponseTypeMessageBodyDataInsertResponse()} };
            var sess = this.ActiveSessions[message.Header.SessionID.ToString()];

            if (sess != null && sess.Status == Enums.SessionStatus.Active)
            {
                if ((sess.Roles & Enums.ServiceAccountRole.Provider) == 0)
                {
                    response.Body.DataInsertResponse.Status = false;
                    response.Body.DataInsertResponse.Description = "Unauthorized";
                    return response;
                }

                var positions = message.Body.ExportDataRequest.Reports.Select(x => new Position()
                {
                    Address = x.Address,
                    AvlDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Parse(x.Date), sess.TimeZone, "UTC"),
                    Direction = x.Direction,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    Velocity = x.Velocity,
                    ModifiedBy = this.ServiceName
                });
                Repo.UpdatePositions(positions);
                response.Body.DataInsertResponse.Status = true;
                response.Body.DataInsertResponse.Description = "Success";
            }
            else
            {
                response.Body.DataInsertResponse.Status = false;
                response.Body.DataInsertResponse.Description = "Customer name not logged in";
            }
            return response;
        }

        public LoginResponseTypeMessage GetDefaultLoginResponseMessage()
        {
            return new LoginResponseTypeMessage() {
                Header = new Header() {
                    MessageIDSpecified = true,
                    MessageID = -1,
                    SessionID = -1,
                    SessionIDSpecified = true},
                Body = new LoginResponseTypeMessageBody() {
                    LoginResponse = new LoginResponseTypeMessageBodyLoginResponse() { ServerDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss zzz") } }
                };
        }
    }
}