using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace HSC.RTD.AVLAggregatorCore
{
    #region Login
    [ModelMetadataType(typeof(LoginRequestType_Metadata))]
    public partial class LoginRequestType : IValidatable
    {

    }

    [ModelMetadataType(typeof(LoginResponseType_Metadata))]
    public partial class LoginResponseType : IValidatable
    {

    }

    [ModelMetadataType(typeof(LoginRequestTypeMessage_Metadata))]
    [System.Xml.Serialization.XmlRootAttribute(ElementName = "Message")]
    public partial class LoginRequestTypeMessage : IValidatable
    {

    }

    [ModelMetadataType(typeof(LoginRequestTypeMessageBody_Metadata))]
    public partial class LoginRequestTypeMessageBody : IValidatable
    {

    }

    [ModelMetadataType(typeof(LoginRequestTypeMessageBodyLoginRequest_Metadata))]
    public partial class LoginRequestTypeMessageBodyLoginRequest : IValidatable
    {

    }

    [ModelMetadataType(typeof(LoginResponseTypeMessage_Metadata))]
    [System.Xml.Serialization.XmlRootAttribute(ElementName = "Message")]
    public partial class LoginResponseTypeMessage : IValidatable
    {

    }

    [ModelMetadataType(typeof(LoginResponseTypeMessageBodyLoginResponse_Metadata))]
    public partial class LoginResponseTypeMessageBodyLoginResponse : IValidatable
    {

    }
    #endregion

    #region Logout
    [ModelMetadataType(typeof(LogoutRequestType_Metadata))]
    public partial class LogoutRequestType : IValidatable
    {

    }

    [System.Xml.Serialization.XmlRootAttribute(ElementName = "Message")]
    [ModelMetadataType(typeof(LogoutRequestTypeMessage_Metadata))]
    public partial class LogoutRequestTypeMessage : IValidatable
    {

    }

    [ModelMetadataType(typeof(LogoutResponseType_Metadata))]
    public partial class LogoutResponseType : IValidatable
    {

    }

    [System.Xml.Serialization.XmlRootAttribute(ElementName = "Message")]
    public partial class LogoutResponseTypeMessage : IValidatable
    {

    }
    #endregion

    #region LastPosition
    [ModelMetadataType(typeof(LastPositionRequestType_Metadata))]
    public partial class LastPositionRequestType : IValidatable
    {

    }

    [ModelMetadataType(typeof(LastPositionRequestTypeMessage_Metadata))]
    [System.Xml.Serialization.XmlRootAttribute(ElementName = "Message")]
    public partial class LastPositionRequestTypeMessage : IValidatable
    {

    }
    
    [ModelMetadataType(typeof(LastPositionResponseType_Metadata))]
    public partial class LastPositionResponseType : IValidatable
    {

    }

    [System.Xml.Serialization.XmlRootAttribute(ElementName = "Message")]
    public partial class LastPositionResponseTypeMessage : IValidatable
    {

    }
    #endregion

    #region ExportDate

    [ModelMetadataType(typeof(ExportDataRequestType_Metadata))]
    public partial class ExportDataRequestType : IValidatable
    {

    }

    [ModelMetadataType(typeof(ExportDataRequestTypeMessage_Metadata))]
    [System.Xml.Serialization.XmlRootAttribute(ElementName = "Message")]
    public partial class ExportDataRequestTypeMessage : IValidatable
    {

    }

    [System.Xml.Serialization.XmlRootAttribute(ElementName = "Message")]
    public partial class ExportDataResponseTypeMessage : IValidatable
    {

    }

    #endregion
}