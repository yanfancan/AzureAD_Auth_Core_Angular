using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using Dapper.Contrib.Extensions;
    
namespace HSC.RTD.AVLAggregatorCore.Data
{
    public class AvlRepository : IAvlRepository
    {
        readonly string connString;
        readonly string ServiceName;

        public AvlRepository(string connectionString,  string serviceName)
        {
            this.connString = connectionString;
            this.ServiceName = serviceName;
        }
        public Dictionary<string, string> GetConfigurationDictionary(string component)
        {
            using (var conn = new SqlConnection(connString))
            {
                var r = conn.Query<POCO.AvlConfiguration>("select ConfigurationKey, ConfigurationValue from AvlConfiguration where (Component = @Component or Component is null) and (ComponentName = @ComponentName or ComponentName is null)", new { Component = component, ComponentName = this.ServiceName });
                return r.ToDictionary(x => x.ConfigurationKey, x => x.ConfigurationValue);
            }
 
        }

        public POCO.ServiceAccount GetServiceAccountByName(string loginName)
        {
            using (var conn = new SqlConnection(connString))
            {
                return conn.QueryFirstOrDefault<POCO.ServiceAccount>("select * from ServiceAccounts (nolock) where LoginName = @LoginName", new { LoginName = loginName } );
            }
        }

        public POCO.User GetUserByEmail(string email)
        {
            using (var conn = new SqlConnection(connString))
            {
                return conn.QueryFirstOrDefault<POCO.User>("select * from Users (nolock) where Email = @Email", new { Email = email });
            }
        }
 
        public IEnumerable<POCO.ServiceAccount> GetServiceAccounts()
        {
            using (var conn = new SqlConnection(connString))
            {
                return conn.Query<POCO.ServiceAccount>("SELECT * FROM ServiceAccounts (nolock)");
            }
        }
        
        public POCO.Session NewSession(int serviceAccountId, string clientIp)
        {
            using (var conn = new SqlConnection(connString))
            {
                return conn.QueryFirst<POCO.Session>(sql_AvlRepository.sql_NewSession, new { AddedBy = this.ServiceName, ServiceAccountId = serviceAccountId, ClientIp = clientIp });
            }
        }

        public POCO.Session GetSessionByClientIPName(string clientIp, string customerName)
        {
            using (var conn = new SqlConnection(connString))
            {
                return conn.QueryFirstOrDefault<POCO.Session>(sql_AvlRepository.sql_GetSessionsbyClientIpName, new { ClientIp = clientIp, CustomerName = customerName });
            }
        }

        public IEnumerable<POCO.Session> GetSessionsByStatus(Enums.SessionStatus sessionStatus)
        {
            using (var conn = new SqlConnection(connString))
            {
                return conn.Query<POCO.Session>(@"select s.*, sa.Roles, sa.TimeZone from Sessions s (nolock) inner join ServiceAccounts sa (nolock) on s.ServiceAccountId = sa.Id where s.Status = @Status", new { Status = sessionStatus });
            }
        }

        public POCO.Session UpdateSession(int sessionId, Enums.SessionStatus status)
        {
            using (var conn = new SqlConnection(connString))
            {
                return conn.QueryFirstOrDefault<POCO.Session>(sql_AvlRepository.sql_UpdateSession, new { SessionId = sessionId, Status = status, ModifiedBy = this.ServiceName, });
            }
        }

        public void UpdatePositions(IEnumerable<POCO.Position> positions)
        {
            using (var conn = new SqlConnection(connString))
            {
                foreach (var pos in positions)
                {
                    conn.Execute(sql_AvlRepository.sql_UpdatePosition, pos);
                }
            }
        }

        public IEnumerable<POCO.Position> GetChangedPositionsBySessionId(int serviceAccountId, int maxFeedAgeSec)
        {
            using (var conn = new SqlConnection(connString))
            {
                var result = conn.Query<POCO.Position>(sql_AvlRepository.sql_GetChangedPositionsByServiceAccount, new { ServiceAccountId = serviceAccountId, MaxFeedAgeSec = maxFeedAgeSec });
                var a = result.ToArray();
                return result;
            }
        }

        // get all users
        public IEnumerable<POCO.User> GetUsers()
        {
            using (var conn = new SqlConnection(connString))
            {
                return conn.Query<POCO.User>("SELECT *  FROM Users");
            }
        }

        // add new users from IEnumerable
        public void newUsers(IEnumerable<POCO.User> users)
        {
            using (var conn = new SqlConnection(connString))
            {
                foreach (POCO.User user in users)
                {

                    //conn.Update(users);
                    //string name = user.Name;
                    conn.Execute("INSERT INTO Users (FirstName, LastName, Department, Email, Position, AccessLevel, AccountLocked, AccountDisabled, Password, AddedDateTime, AddedBy, ModifiedDateTime, ModifiedBy) VALUES (@FirstName, @LastName, @Department, @Email, @Position, @AccessLevel, @AccountLocked, @AccountDisabled, @Password, CURRENT_TIMESTAMP, @AddedBy, Null, Null)", //@CLientIP, @CurrentTime, @AddedBy, Null, Null) GO",
                        new { user.FirstName, user.LastName, user.Department, user.Email, user.Position, user.AccessLevel, user.AccountLocked, user.AccountDisabled, user.Password, user.AddedBy }); //TimeZone = user.TimeZone, CurrentTime = user.AddedDateTime, AddedBy = user.AddedBy  });


                    //conn.Insert<POCO.User>(user);
                }
            }
        }

        // delete users from IEnumerable
        public void deleteUsers(IEnumerable<POCO.User> users)
        {
            using (var conn = new SqlConnection(connString))
            {
                foreach (POCO.User user in users)
                {
                    conn.Execute("DELETE FROM Users WHERE Id = @Id", new { user.Id });

                }
            }
        }

        // update users from IEnumerable
        public void updateUsers(IEnumerable<POCO.User> users)
        {
            using (var conn = new SqlConnection(connString))
            {
                foreach (POCO.User user in users)
                {

                    //conn.Update(users);
                    conn.Execute("UPDATE Users SET FirstName = @FirstName, LastName = @LastName, Department = @Department, Email = @Email, Position = @Position, AccessLevel = @AccessLevel, AccountLocked = @AccountLocked, AccountDisabled = @AccountDisabled, ModifiedDateTime = CURRENT_TIMESTAMP, ModifiedBy = @ModifiedBy WHERE Id = @Id", //@CLientIP, @CurrentTime, @AddedBy, Null, Null) GO",
                        new { user.FirstName, user.LastName, user.Department, user.Email, user.Position, user.AccessLevel, user.AccountLocked, user.AccountDisabled, user.ModifiedBy, user.Id }); 

                }
            }
        }

        // update Service Accounts from IEnumerable
        public void updateServiceAccounts(IEnumerable<POCO.ServiceAccount> users)
        {
            using (var conn = new SqlConnection(connString))
            {
                foreach (POCO.ServiceAccount user in users)
                {

                    //conn.Update(users);
                    conn.Execute("UPDATE ServiceAccounts SET LoginName = @LoginName, Name = @Name, Password = @Password, Roles = @Roles, Status = @Status, TimeZone = @TimeZone, ModifiedDateTime = CURRENT_TIMESTAMP, ModifiedBy = @ModifiedBy WHERE Id = @Id", 
                        new { user.LoginName, user.Name, user.Password, user.Roles, user.Status, user.TimeZone, user.ModifiedDateTime, user.ModifiedBy, user.Id }); 

                } 
            }
        }

        // add new service Accounts from IEnumerable
        public void newServiceAccounts(IEnumerable<POCO.ServiceAccount> serviceAccounts)
        {
            using (var conn = new SqlConnection(connString))
            {
                foreach (POCO.ServiceAccount serviceAccount in serviceAccounts)
                {

                    //conn.Update(users);
                    //string name = user.Name;
                    conn.Execute("INSERT INTO ServiceAccounts (LoginName, Name, Password, Roles, Status, TimeZone, AddedDateTime, AddedBy, ModifiedDateTime, ModifiedBy) VALUES ( @LoginName, @Name, @Password, @Roles, @Status, @TimeZone, CURRENT_TIMESTAMP, @AddedBy, Null, Null)", //@CLientIP, @CurrentTime, @AddedBy, Null, Null) GO",
                    new {serviceAccount.LoginName, serviceAccount.Name, serviceAccount.Password, serviceAccount.Roles, serviceAccount.Status, serviceAccount.TimeZone, serviceAccount.AddedBy }); //TimeZone = user.TimeZone, CurrentTime = user.AddedDateTime, AddedBy = user.AddedBy  });
                    //conn.Insert<POCO.User>(user);
                }
            }
        }

        // delete serviceAccounts from IEnumerable
        public void deleteServiceAccounts(IEnumerable<POCO.ServiceAccount> serviceAccounts)
        {
            using (var conn = new SqlConnection(connString))
            {
                foreach (POCO.ServiceAccount serviceAccount in serviceAccounts)
                {
                    conn.Execute("DELETE FROM ServiceAccounts WHERE Id = @Id", new { serviceAccount.Id });

                }
            }
        }

        //get all devices
        public IEnumerable<POCO.Device> GetDevices()
        {
            using (var conn = new SqlConnection(connString))
            {
                return conn.Query<POCO.Device>("SELECT *  FROM Devices");
            }
        }

        // add new devices from IEnumerable
        public void newDevices(IEnumerable<POCO.Device> devices)
        {
            using (var conn = new SqlConnection(connString))
            {
                foreach (POCO.Device device in devices)
                {
                    
                    //conn.Execute("INSERT INTO Devices (Id, Address, VehicleID, AddedDateTime, AddedBy, ModifiedDateTime, ModifiedBy) VALUES (@Id, @Address, @VehicleID, CURRENT_TIMESTAMP, @AddedBy, Null, Null)", 
                    conn.Execute("INSERT INTO Devices (Address, VehicleID, ServiceId, VehicleName, DeviceType, AddedDateTime, AddedBy) VALUES (@Address, @VehicleID, @ServiceId, @VehicleName, @DeviceType, CURRENT_TIMESTAMP, @AddedBy)", 
                        new {device.Address, device.VehicleId, device.ServiceId, device.VehicleName, device.DeviceType, device.AddedBy }); 
                    
                }
            }
        }

        // delete devices from IEnumerable
        public void deleteDevices(IEnumerable<POCO.Device> devices)
        {
            using (var conn = new SqlConnection(connString))
            {
                foreach (POCO.Device device in devices)
                {
                    conn.Execute("DELETE FROM Devices WHERE Id = @Id", new { device.Id });

                }
            }
        }

        // update devices from IEnumerable
        public void updateDevices(IEnumerable<POCO.Device> devices)
        {
            using (var conn = new SqlConnection(connString))
            {
                foreach (POCO.Device device in devices)
                {
                    //conn.Execute("UPDATE Devices SET Address = @Address, VehicleId = @VehicleId, ModifiedDateTime = CURRENT_TIMESTAMP, ModifiedBy = @ModifiedBy WHERE Id = @Id", 
                    conn.Execute("UPDATE Devices SET Address = @Address, VehicleId = @VehicleId, ServiceId = @ServiceId, VehicleName = @VehicleName, DeviceType = @DeviceType WHERE Id = @Id", 
                        new { device.Id, device.Address, device.VehicleId, device.ServiceId, device.VehicleName, device.DeviceType, device.ModifiedBy,}); 
                }
            }
        }

        // get all services
        public IEnumerable<POCO.Service> GetServices()
        {
            using (var conn = new SqlConnection(connString))
            {
                return conn.Query<POCO.Service>("SELECT *  FROM Services");
            }
        }

        // get all services
        public IEnumerable<POCO.Service> GetAssociatedServices(int i)
        {
            using (var conn = new SqlConnection(connString))
            {
                return conn.Query<POCO.Service>("Select s.* from dbo.Services as s "
                                                + "Inner join ServiceAccounts_Services as r "
                                                + "on r.ServiceId = s.Id "
                                                + "where r.ServiceAccountId = @ServiceAccountId;", new { @ServiceAccountId = i});
            
        }
        }

    }
}