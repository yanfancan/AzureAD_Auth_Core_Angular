using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Schema;
using Microsoft.Extensions.Logging;

using HSC.RTD.AVLAggregatorCore.BusinessLogic;
using HSC.RTD.AVLAggregatorCore.Extensions;
using HSC.RTD.AVLAggregatorCore.Logging;
using HSC.RTD.AVLAggregatorCore.Models;

namespace HSC.RTD.AVLAggregatorCore
{
    public class AvlAggregatorService : IAvlAggregatorService
    {
        protected IAvlAggregatorServiceBL BL;
        protected IAvlConfiguration Config;
        protected readonly IUtils Utils;
        private readonly IAvlLogger<AvlAggregatorService> Logger;

        public AvlAggregatorService(IAvlAggregatorServiceBL businessLogic, IAvlConfiguration configuration, IUtils utils, IAvlLogger<AvlAggregatorService> logger)
        {
            this.BL = businessLogic;
            this.Config = configuration;
            this.Utils = utils;
            this.Logger = logger;
        }

        public string LastPosition(string messageXml)
        {
            LastPositionResponseTypeMessage responseMsg;
            LastPositionRequestTypeMessage request = null;
            try
            {
                $"<LastPositionRequestType>{messageXml}</LastPositionRequestType>".XDocValidate(Utils.GetSchemas());
                request = messageXml.DeserializeFromXmlString<LastPositionRequestTypeMessage>();
                Logger.LogInformation(AvlLogEvent.LastPositionCall, request.Header.SessionID, null);
                responseMsg = BL.LastPosition(request);
            }
            catch (XmlSchemaValidationException ex)
            {
                Logger.LogError(ex, request != null ? request.Header.SessionID : 0, "LastPosition request validation error.");
                responseMsg = new LastPositionResponseTypeMessage()
                {
                    Header = new Header()
                    {
                        MessageIDSpecified = true,
                        SessionIDSpecified = true,
                        MessageID = -1,
                        SessionID = -1
                    },
                    Body = new LastPositionResponseTypeMessageBody()
                    {
                        LastPositionResponse = new LastPositionResponseTypeMessageBodyLastPositionResponse()
                        {
                            Status = false,
                            Description = "XML syntax error in request",
                            VehicleList = new LastPositionResponseTypeMessageBodyLastPositionResponseVehicle[] { }
                        }
                    }
                };
            }
            string responseStr = responseMsg.SerializeToXmlString();
            $"<LastPositionResponseType>{responseStr}</LastPositionResponseType>".XDocValidate(Utils.GetSchemas());
            Logger.LogDebug(AvlLogEvent.LastPositionCall, responseMsg.Header.SessionID, "Response sent: {responseStr}", responseStr);
            return responseStr;
        }

        public string Login(string messageXml)
        {
            LoginResponseTypeMessage responseMsg;
            LoginRequestTypeMessage request = null;
            try
            {
                $"<LoginRequestType>{messageXml}</LoginRequestType>".XDocValidate(Utils.GetSchemas());
                request = messageXml.DeserializeFromXmlString<LoginRequestTypeMessage>();
                Logger.LogInformation(AvlLogEvent.LoginCall, request.Header.SessionID, "Login Name: {loginName}", request.Body.LoginRequest.CustomerName);
                responseMsg = BL.Login(request);
            }
            catch (XmlSchemaValidationException ex) {
                Logger.LogError(ex, request != null ? request.Header.SessionID : 0,"Login request validation error.");
                responseMsg = BL.GetDefaultLoginResponseMessage();
                responseMsg.Body.LoginResponse.Status = false;
                responseMsg.Body.LoginResponse.Description = "XML syntax error in request";
            }
            var responseStr = responseMsg.SerializeToXmlString();
            $"<LoginResponseType>{responseStr}</LoginResponseType>".XDocValidate(Utils.GetSchemas());
            Logger.LogDebug(AvlLogEvent.LoginCall, responseMsg.Header.SessionID, "Response sent: {responseStr}", responseStr);
            return responseStr;
        }

        public string Logout(string messageXml)
        {
            LogoutResponseTypeMessage responseMsg;
            LogoutRequestTypeMessage request = null;
            try
            {
                $"<LogoutRequestType>{messageXml}</LogoutRequestType>".XDocValidate(Utils.GetSchemas());
                request = messageXml.DeserializeFromXmlString<LogoutRequestTypeMessage>();
                Logger.LogInformation(AvlLogEvent.LogoutCall, request.Header.SessionID, null);
                responseMsg = BL.Logout(request);
            }
            catch (XmlSchemaValidationException ex)
            {
                Logger.LogError(ex, request != null ? request.Header.SessionID : 0, "Logout request validation error.");
                responseMsg = new LogoutResponseTypeMessage()
                {
                    Header = new Header()
                    {
                        MessageIDSpecified = true,
                        MessageID = -1,
                        SessionIDSpecified = true,
                        SessionID = -1
                    },
                    Body = new LogoutResponseTypeMessageBody()
                    {
                        LogoutResponse = new LogoutResponseTypeMessageBodyLogoutResponse()
                        {
                            Description = "XML syntax error in request",
                            Status = false
                        }
                    }
                };
            }

            var responseStr = responseMsg.SerializeToXmlString();
            $"<LogoutResponseType>{responseStr}</LogoutResponseType>".XDocValidate(Utils.GetSchemas());
            Logger.LogDebug(AvlLogEvent.LogoutCall, responseMsg.Header.SessionID, "Response sent: {responseStr}", responseStr);
            return responseStr;
        }

        public string ExportData(string messageXml)
        {
            ExportDataResponseTypeMessage responseMsg;
            ExportDataRequestTypeMessage request = null;
            try
            {
                $"<ExportDataRequestType>{messageXml}</ExportDataRequestType>".XDocValidate(Utils.GetSchemas());
                request = messageXml.DeserializeFromXmlString<ExportDataRequestTypeMessage>();
                Logger.LogInformation(AvlLogEvent.ExportDataCall, request.Header.SessionID, null);
                responseMsg = BL.ExportData(request);
            }
            catch (XmlSchemaValidationException ex)
            {
                Logger.LogError(ex, request != null ? request.Header.SessionID : 0, "ExportData request validation error.");
                responseMsg = new ExportDataResponseTypeMessage() {
                    Header = new Header() {
                        MessageIDSpecified = true,
                        SessionIDSpecified = true,
                        MessageID = -1,
                        SessionID = -1 },
                    Body = new ExportDataResponseTypeMessageBody() {
                         DataInsertResponse = new  ExportDataResponseTypeMessageBodyDataInsertResponse ()
                        {
                             Description = "XML syntax error in request",
                             Status = false
                        }
                    }
                };
            }
            var responseStr = responseMsg.SerializeToXmlString();
            $"<ExportDataResponseType>{responseStr}</ExportDataResponseType>".XDocValidate(Utils.GetSchemas());
            Logger.LogDebug(AvlLogEvent.ExportDataCall, responseMsg.Header.SessionID, "Response sent: {responseStr}", responseStr);
            return responseStr;
        }
    }
}
