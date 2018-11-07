using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Xml.Schema;
using HSC.RTD.AVLAggregator;


namespace HSC.RTD.AVLAggregator.Extensions.Tests
{
    [TestClass()]
    public class SerializeExtensionsTests
    {

        [TestMethod()]
        public void Deserialize_LoginRequest_Test()
        {
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(LoginRequestType), typeof(LoginRequestType_Metadata)), typeof(LoginRequestType));


            string request = @"<LoginRequestType><Message>
                  <Header>
                    <MessageID>0</MessageID>
                    <SessionID>0</SessionID>
                    <ServerDateTime>2009-03-24 11:14:00 -04:00</ServerDateTime>
                  </Header>
                  <Body>
                    <LoginRequest>
                      <CustomerName>interfleet</CustomerName>
                      <Password>gps</Password>
                      <ClientIP>154.107.34.268</ClientIP>
                      <TimeZone>Canada Central Standard Time</TimeZone>
                    </LoginRequest>
                  </Body>
                </Message></LoginRequestType>";

            var result = request.DeserializeFromXmlString<LoginRequestType>();

            request = @"<LoginRequestType><Message>
                  <Header>
                    <MessageID>abc</MessageID>
                    <SessionID>0</SessionID>
                  </Header>
                  <Body>
                    <LoginRequest>
                      <CustomerName>interfleet</CustomerName>
                      <Password>gps</Password>
                      <ClientIP>154.107.34.268</ClientIP>
                      <TimeZone>Canada Central Standard Time</TimeZone>
                    </LoginRequest>
                  </Body>
                </Message></LoginRequestType>";

            try
            {
                result = request.DeserializeFromXmlString<LoginRequestType>();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("There is an error in XML document"));
            }
        }
    }
}