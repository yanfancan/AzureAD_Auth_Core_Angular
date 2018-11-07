namespace HSC.RTD.AVLAggregatorCore.BusinessLogic
{
    public interface IAvlAggregatorServiceBL
    {
        ExportDataResponseTypeMessage ExportData(ExportDataRequestTypeMessage message);
        LastPositionResponseTypeMessage LastPosition(LastPositionRequestTypeMessage message);
        LoginResponseTypeMessage Login(LoginRequestTypeMessage message);
        LogoutResponseTypeMessage Logout(LogoutRequestTypeMessage message);

        LoginResponseTypeMessage GetDefaultLoginResponseMessage();
    }
}