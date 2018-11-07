using System.Xml;
using System.Reflection;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SoapCore;
using HSC.RTD.AVLAggregatorCore.Models;
using HSC.RTD.AVLAggregatorCore.Data;
using HSC.RTD.AVLAggregatorCore.Data.POCO;
using HSC.RTD.AVLAggregatorCore.BusinessLogic;
using HSC.RTD.AVLAggregatorCore.Middleware;
using HSC.RTD.AVLAggregatorCore.Logging;

namespace HSC.RTD.AVLAggregatorCore
{
    public class Startup
    {
        private IServiceProvider ServiceProvider;
        public Startup(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            Configuration = configuration;
            ServiceProvider = serviceProvider;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string serviceName = Configuration.GetValue<string>("ServiceName", "HSC.RTD.AVLAggregatorCore");

            services.AddMemoryCache();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton<IAvlRepository, AvlRepository>((x) => { return new AvlRepository(Configuration.GetConnectionString("avl"), serviceName); });
            services.AddSingleton<IUtils, Utils>();


            //Cached services
            services.AddSingleton<NamedService>((x) => { return new NamedService("Configuration", new Func<string, Dictionary<string, string>>((a) => x.GetRequiredService<IAvlRepository>().GetConfigurationDictionary("AVLAggregator")));});
            services.AddSingleton<NamedService>((x) => { return new NamedService("ActiveSessions", new Func<string, Dictionary<string, Session>>((a) => x.GetRequiredService<IAvlRepository>().GetSessionsByStatus(Enums.SessionStatus.Active).ToDictionary(b => b.Id.ToString(), b => b))); });

            //Configuration
            services.AddSingleton<IAvlConfiguration, ConfigurationObject>((x) => { return new ConfigurationObject((Func<string, Dictionary<string, string>>)x.GetServices<NamedService>().First(s=>s.Name == "Configuration").Service, "AVLAggregator", x.GetRequiredService<IMemoryCache>()); });

            //ActiveSessions
            services.AddSingleton<ICachedDictionary<string, Session>>((x) => { return new CachedDictionary<string, Session>("ActiveSessions", (Func<string, Dictionary<string, Session>>)x.GetServices<NamedService>().First(s => s.Name == "ActiveSessions").Service, 600, serviceName, x.GetRequiredService<IMemoryCache>()); });

            services.AddSingleton<IAvlAggregatorServiceBL, AvlAggregatorServiceBL>((x) => { return new AvlAggregatorServiceBL(x.GetRequiredService<IAvlRepository>(), serviceName, x.GetService<IAvlConfiguration>(), x.GetRequiredService<ICachedDictionary<string,Data.POCO.Session>>()); });

            services.TryAddSingleton<IAvlAggregatorService, AvlAggregatorService>();

            services.AddSingleton(typeof(IAvlLogger<>), typeof(AvlLogger<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //DI

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseMiddleware<RequestInterceptor>();
            app.UseSoapEndpoint<IAvlAggregatorService>("/AvlAggregatorService.svc", new BasicHttpBinding(), SoapSerializer.DataContractSerializer);
            app.UseMvc();
        }    
    }

    public class NamedService
    {
        public string Name { get; private set; }
        public object Service { get; private set; }
        public NamedService(string name, object service)
        {
            this.Name = name;
            this.Service = service;
        }
    }
}
