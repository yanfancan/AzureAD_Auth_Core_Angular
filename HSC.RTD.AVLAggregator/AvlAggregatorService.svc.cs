using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.Security;
using HSC.RTD.AVLAggregator.BusinessLogic;
using HSC.RTD.AVLAggregator.Extensions;


namespace HSC.RTD.AVLAggregator
{
    [ServiceBehavior(Namespace = "http://mohltc.on.ca/xmlns/avl")]
    public class AvlAggregatorService : IAvlAggregatorService
    {
        protected IAvlAggregatorServiceBL BL;
        protected IAvlConfiguration Config;

        public AvlAggregatorService(IAvlAggregatorServiceBL businessLogic, IAvlConfiguration configuration)
        {
            this.BL = businessLogic;
            this.Config = configuration;
        }

        public string LastPosition(string messageXml)
        {
            $"<LastPositionRequestType>{messageXml}</LastPositionRequestType>".XDocValidate(Utils.GetSchemas());
            var request = messageXml.DeserializeFromXmlString<LastPositionRequestTypeMessage>();
            var responseMsg = BL.LastPosition(request);
            return responseMsg.SerializeToXmlString();
        }

        public string Login(string messageXml)
        {
            $"<LoginRequestType>{messageXml}</LoginRequestType>".XDocValidate(Utils.GetSchemas());

            var request = messageXml.DeserializeFromXmlString<LoginRequestTypeMessage>();
            var responseMsg = BL.Login(request);
            if (responseMsg.Body.LoginResponse.Status)
            {
                var ticket = new FormsAuthenticationTicket(
                        1,
                        request.Body.LoginRequest.CustomerName,
                        DateTime.Now,
                        DateTime.Now.AddDays(int.Parse(this.Config["LoginExpirationDays"])),
                        true,
                        request.Body.LoginRequest.ClientIP
                    );
                string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            var responseStr = responseMsg.SerializeToXmlString();

            $"<LoginResponseType>{responseStr}</LoginResponseType>".XDocValidate(Utils.GetSchemas());
            return responseStr;
        }

        public string Logout(string messageXml)
        {
            $"<LogoutRequestType>{messageXml}</LogoutRequestType>".XDocValidate(Utils.GetSchemas());

            var request = messageXml.DeserializeFromXmlString<LogoutRequestTypeMessage>();
            var responseMsg = BL.Logout(request);
            FormsAuthentication.SignOut();
            return responseMsg.SerializeToXmlString();
        }

        public string ExportData(string messageXml)
        {
            $"<ExportDataRequestType>{messageXml}</ExportDataRequestType>".XDocValidate(Utils.GetSchemas());
            var request = messageXml.DeserializeFromXmlString<ExportDataRequestTypeMessage>();
            var responseMsg = BL.ExportData(request);
            var responseStr = responseMsg.SerializeToXmlString();
            $"<ExportDataResponseType>{responseStr}</ExportDataResponseType>".XDocValidate(Utils.GetSchemas());
            return responseStr;
        }
    }
}
