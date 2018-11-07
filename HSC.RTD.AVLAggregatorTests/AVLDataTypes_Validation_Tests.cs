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
    public class AVLDataTypes_Validation_Tests
    {
        [TestMethod()]
        public void Validate_LoginRequest_Test()
        {
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(LoginRequestType), typeof(LoginRequestType_Metadata)), typeof(LoginRequestType));

            string request = @"<LoginRequestType></LoginRequestType>";

            try
            {
                var result = request.DeserializeFromXmlString<LoginRequestType>();
                result.ValidateInbound();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("The Message field is required."));
            }

        }

        [TestMethod()]
        public void Validate_LoginResponse_Test()
        {
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(LoginResponseType), typeof(LoginResponseType_Metadata)), typeof(LoginResponseType));

            string request = @"<LoginResponseType></LoginResponseType>";

            try
            {
                var result = request.DeserializeFromXmlString<LoginResponseType>();
                result.ValidateInbound();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("The Message field is required."));
            }
        }

        [TestMethod()]
        public void Validate_LogoutRequest_Test()
        {
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(LogoutRequestType), typeof(LogoutRequestType_Metadata)), typeof(LogoutRequestType));

            string request = @"<LogoutRequestType></LogoutRequestType>";

            try
            {
                var result = request.DeserializeFromXmlString<LogoutRequestType>();
                result.ValidateInbound();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("The Message field is required."));
            }
        }

        [TestMethod()]
        public void Validate_LogoutResponse_Test()
        {
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(LogoutResponseType), typeof(LogoutResponseType_Metadata)), typeof(LogoutResponseType));

            string request = @"<LogoutResponseType></LogoutResponseType>";

            try
            {
                var result = request.DeserializeFromXmlString<LogoutResponseType>();
                result.ValidateInbound();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("The Message field is required."));
            }
        }

        [TestMethod()]
        public void Validate_LastPositionRequest_Test()
        {
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(LastPositionRequestType), typeof(LastPositionRequestType_Metadata)), typeof(LastPositionRequestType));

            string request = @"<LastPositionRequestType></LastPositionRequestType>";

            try
            {
                var result = request.DeserializeFromXmlString<LastPositionRequestType>();
                result.ValidateInbound();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("The Message field is required."));
            }
        }

        [TestMethod()]
        public void Validate_LastPositionResponseType_Test()
        {
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(LastPositionResponseType), typeof(LastPositionResponseType_Metadata)), typeof(LastPositionResponseType));

            string request = @"<LastPositionResponseType></LastPositionResponseType>";

            try
            {
                var result = request.DeserializeFromXmlString<LastPositionResponseType>();
                result.ValidateInbound();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("The Message field is required."));
            }
        }

        [TestMethod()]
        public void Validate_ExportDataRequestType_Test()
        {
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(ExportDataRequestType), typeof(ExportDataRequestType_Metadata)), typeof(ExportDataRequestType));

            string request = @"<ExportDataRequestType></ExportDataRequestType>";

            try
            {
                var result = request.DeserializeFromXmlString<ExportDataRequestType>();
                result.ValidateInbound();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("The Message field is required."));
            }
        }

        [TestMethod()]
        public void Validate_LoginRequest1_Test()
        {
            string request = @"<LoginRequestType></LoginRequestType>";
            try
            {
                request.XDocValidate(Utils.GetSchemas());
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("The Message field is required."));
            }

        }

    }
}