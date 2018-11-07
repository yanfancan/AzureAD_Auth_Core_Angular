using System;
using System.Collections.Generic;
using System.Configuration;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using System.ComponentModel.DataAnnotations;
using Castle.Facilities.WcfIntegration;

using HSC.RTD.AVLAggregator.Data;
using HSC.RTD.AVLAggregator.BusinessLogic;
using HSC.RTD.AVLAggregator.Logging;


namespace HSC.RTD.AVLAggregator
{
    public static class Bootstrap
    {
        public static IWindsorContainer Initialize()
        {
            //We need to apply some additional attributes to auto generated code using *_Metadata classes.
            System.ComponentModel.TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(LoginRequestType), typeof(LoginRequestType_Metadata)), typeof(LoginRequestType));

            string serviceName = Utils.GetServiceName();
            IWindsorContainer container = new WindsorContainer();

            container.Register(Component.For<IAvlRepository>().ImplementedBy<AvlRepository>()
                .DynamicParameters((k,parameters) => {
                    parameters["connectionString"] = ConfigurationManager.ConnectionStrings["avl"].ConnectionString;
                    parameters["serviceName"] = serviceName;
                }).LifestyleSingleton());
            container.Register(Component.For<IAvlConfiguration>().ImplementedBy<ConfigurationObject>()
                .DynamicParameters((k, parameters) =>
                {
                    parameters["getDictionary"] = (Func<string, Dictionary<string, string>>)k.Resolve<IAvlRepository>().GetConfigurationDictionary;
                    parameters["componentName"] = "AVLAggregator";
                }
                        ).LifestyleSingleton());
            container.Register(Component.For<IAvlLogger>().ImplementedBy<AvlLogger>().DependsOn(Dependency.OnValue("loggerName", "HSC.RTD.AvlAggregator")).LifestyleSingleton());
            container.Register(Component.For<IncomingMessageInspector>().ImplementedBy<IncomingMessageInspector>().DynamicParameters((k, parameters) =>
            {
                parameters["logger"] = k.Resolve<IAvlLogger>();
            }
            ).LifestyleSingleton());

            container.AddFacility<WcfFacility>().Register(
                Component.For<IAvlAggregatorServiceBL>().ImplementedBy<AvlAggregatorServiceBL>().DynamicParameters((k, parameters) => {
                    parameters["serviceName"] = serviceName;
                }),
                Component.For<IAvlAggregatorService, AvlAggregatorService>()
                );
            return container;
        }
    }
}