using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSC.RTD.AVLAggregatorCore.Data
{
    public interface IAvlRepository
    {
        Dictionary<string, string> GetConfigurationDictionary(string componentName);
        POCO.ServiceAccount GetServiceAccountByName(string loginName);
        POCO.User GetUserByEmail(string email);
        POCO.Session NewSession(int serviceAccountId, string clientIp);
        POCO.Session GetSessionByClientIPName(string clientIp, string customerName);
        IEnumerable<POCO.Session> GetSessionsByStatus(Enums.SessionStatus sessionStatus);
        POCO.Session UpdateSession(int sessionId, Enums.SessionStatus status);
        void UpdatePositions(IEnumerable<POCO.Position> positions);
        IEnumerable<POCO.Position> GetChangedPositionsBySessionId(int serviceAccountId, int maxFeedAgeSec);
        IEnumerable<POCO.ServiceAccount> GetServiceAccounts();

        IEnumerable<POCO.User> GetUsers();
        void updateUsers(IEnumerable<POCO.User> users);
        void newUsers(IEnumerable<POCO.User> users);
        void deleteUsers(IEnumerable<POCO.User> users);
        
        void updateServiceAccounts(IEnumerable<POCO.ServiceAccount> users);
        void newServiceAccounts(IEnumerable<POCO.ServiceAccount> serviceAccounts);
        void deleteServiceAccounts(IEnumerable<POCO.ServiceAccount> serviceAccounts);

        IEnumerable<POCO.Device> GetDevices();
        void updateDevices(IEnumerable<POCO.Device> users);
        void newDevices(IEnumerable<POCO.Device> serviceAccounts);
        void deleteDevices(IEnumerable<POCO.Device> serviceAccounts);

        IEnumerable<POCO.Service> GetServices();
        IEnumerable<POCO.Service> GetAssociatedServices(int i);





    }
}
