using System;
using System.ComponentModel.DataAnnotations;

namespace HSC.RTD.AVLAggregatorCore
{
    #region Login
    public class LoginRequestType_Metadata
    {
        [HasValue]
        public LoginRequestTypeMessage Message { get; set; }
    }

    public class LoginResponseType_Metadata
    {
        [HasValue]
        public LoginResponseTypeMessage Message { get; set; }
    }

    public class LoginRequestTypeMessage_Metadata
    {
        [HasValue]
        public Header Header { get; set; }
        [HasValue]
        public LoginRequestTypeMessageBody Body { get; set; }
    }

    public class LoginRequestTypeMessageBody_Metadata
    {
        [HasValue]
        public LoginRequestTypeMessageBodyLoginRequest LoginRequest { get; set; }
    }

    public class LoginRequestTypeMessageBodyLoginRequest_Metadata
    {
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ClientIP { get; set; }

    }

    public class LoginResponseTypeMessage_Metadata
    {
        [HasValue]
        public Header Header { get; set; }
        [HasValue]
        public LoginResponseTypeMessageBody Body { get; set; }
    }

    public class LoginResponseTypeMessageBodyLoginResponse_Metadata
    {
        [Required]
        public bool Status { get; set; }
        [HasValue]
        public string Description { get; set; }
    }
    #endregion

    #region Logout
    public class LogoutRequestType_Metadata
    {
        [HasValue]
        public LogoutRequestTypeMessage Message { get; set; }
    }

    public class LogoutResponseType_Metadata
    {
        [HasValue]
        public LogoutResponseTypeMessage Message { get; set; }
    }

    public class LogoutRequestTypeMessage_Metadata
    {
        [HasValue]
        public Header Header { get; set; }
        [HasValue]
        public LogoutRequestTypeMessageBody Body { get; set; }
    }

    public class LogoutRequestTypeMessageBodyLogoutRequest_Metadata
    {
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string ClientIP { get; set; }
    }

    public class LogoutResponseTypeMessage_Metadata
    {
        [HasValue]
        public Header Header { get; set; }
        [HasValue]
        public LogoutResponseTypeMessageBody Body { get; set; }
    }

    public class LogoutResponseTypeMessageBodyLogoutResponse_Metadata
    {
        [Required]
        public string Status { get; set; }
        [Required]
        public string Description { get; set; }
    }

    #endregion

    #region LastPosition
    public class LastPositionRequestType_Metadata
    {
        [HasValue]
        public LastPositionRequestTypeMessage Message { get; set; }
    }

    public class LastPositionResponseType_Metadata
    {
        [HasValue]
        public LastPositionResponseTypeMessage Message { get; set; }
    }
    public class LastPositionRequestTypeMessage_Metadata
    {
        [HasValue]
        public Header Header { get; set; }
        [HasValue]
        public LastPositionRequestTypeMessageBody Body { get; set; }
    }

    public class LastPositionResponseTypeMessage_Metadata
    {
        [HasValue]
        public Header Header { get; set; }
        [HasValue]
        public LastPositionResponseTypeMessageBody Body { get; set; }
    }
    public class LastPositionResponseTypeMessageBody_Metadata
    {
        [HasValue]
        public LastPositionResponseTypeMessageBodyLastPositionResponse LastPositionResponse { get; set; }
    }
    public class LastPositionResponseTypeMessageBodyLastPositionResponse_Metadata
    {
        [Required]
        public string Status { get; set; }
        [Required]
        public string Description { get; set; }
        [HasValue]
        public LastPositionResponseTypeMessageBodyLastPositionResponseVehicle[] VehicleList { get; set; }
    }

    public class LastPositionResponseTypeMessageBodyLastPositionResponseVehicle_Metadata
    {
        [Required]
        public string VehicleName { get; set; }
        [Required]
        public decimal Latitude { get; set; }
        [Required]
        public decimal Longitude { get; set; }
        [Required]
        public string Datum { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public int Speed { get; set; }
        [Required]
        public int Bearing { get; set; }
        [Required]
        public int ID { get; set; }
    }
    #endregion

    #region ExpordData
    public class ExportDataRequestType_Metadata
    {
        [HasValue]
        public ExportDataRequestTypeMessage Message { get; set; }
    }

    public class ExportDataRequestTypeMessage_Metadata
    {
        [HasValue]
        public Header Header { get; set; }
        [HasValue]
        public ExportDataRequestTypeMessageBody Body { get; set; }
    }

    public class ExportDataRequestTypeMessageBody_Metadata
    {
        [HasValue]
        public ExportDataRequestTypeMessageBodyExportDataRequest ExportDataRequest { get; set; }
    }

    public class ExportDataRequestTypeMessageBodyExportDataRequest_Metadata
    {
        [HasValue]
        public ExportDataRequestTypeMessageBodyExportDataRequestReport[] Reports { get; set; }
    }

    public class ExportDataRequestTypeMessageBodyExportDataRequestReport_Metadata
    {
        [Required]
        public string Address { get; set; }
        [Required]
        public decimal Longitude { get; set; }
        [Required]
        public decimal Latitude { get; set; }
        [Required]
        public int Velocity { get; set; }
        [Required]
        public int Direction { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
    #endregion
}