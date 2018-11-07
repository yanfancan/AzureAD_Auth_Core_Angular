using System.Xml.Schema;

namespace HSC.RTD.AVLAggregatorCore
{
    public interface IUtils
    {
        XmlSchemaSet GetSchemas();
        string GetServiceName(bool fullName = true);
    }
}