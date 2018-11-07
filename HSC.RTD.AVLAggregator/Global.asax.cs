using System;
using Castle.Windsor;
using HSC.RTD.AVLAggregator.Logging;


namespace HSC.RTD.AVLAggregator
{
    public class Global : System.Web.HttpApplication
    {
        IWindsorContainer container;
        IAvlLogger Log;

        protected void Application_Start(object sender, EventArgs e)
        {
            //System.Diagnostics.Debugger.Launch();
            container = Bootstrap.Initialize();
            Log = container.Resolve<IAvlLogger>();
            Log.LogInfo("Service Started");
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            if (container != null) container.Dispose();
            Log.LogInfo("Service Stopped");
        }
    }
}