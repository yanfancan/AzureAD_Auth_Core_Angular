using System;
using System.Reflection;
using System.Xml.Schema;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace HSC.RTD.AVLAggregatorCore
{
    public class Utils : IUtils
    {
        private static XmlSchemaSet cip_exchange_schemaset;
        private readonly IHostingEnvironment env;

        public Utils(IHostingEnvironment env)
        {
            this.env = env;
        }


        public XmlSchemaSet GetSchemas()
        {
            if (cip_exchange_schemaset == null)
            {
                try
                {
                    string dir;
                    if ( System.ServiceModel.OperationContext.Current == null)
                    {
                        dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    }
                    else
                    {
                        dir = $"{this.env.ContentRootPath}bin";
                    }
                    cip_exchange_schemaset = new XmlSchemaSet();
                    cip_exchange_schemaset.Add("", dir + @"\Schemas\AVLDataTypes.xsd");
                    cip_exchange_schemaset.Compile();
                }
                catch
                {
                    cip_exchange_schemaset = null;
                    throw;
                }
            }
            return cip_exchange_schemaset;
        }

        public string GetServiceName(bool fullName = true)
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        }

    }
}