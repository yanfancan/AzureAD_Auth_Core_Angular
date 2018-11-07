using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Xml.Schema;
using System.Xml.Linq;


namespace HSC.RTD.AVLAggregatorCore.Extensions
{
    public static class SerializeExtensions
    {
        public static string SerializeToXmlString<T>(this T instance)
        {
            XmlWriterSettings ws = new XmlWriterSettings();
            ws.NewLineHandling = NewLineHandling.Entitize;
            ws.NamespaceHandling = NamespaceHandling.OmitDuplicates;
            ws.NewLineHandling = NewLineHandling.Replace;
            ws.OmitXmlDeclaration = true;

            var sb = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(sb, ws))
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, instance, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty } ));
            }

            return sb.ToString();
        }


        public static T DeserializeFromXmlString<T>(this string xmlString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(new StringReader(xmlString));

        }

        public static bool XDocValidate(this string xmlString, XmlSchemaSet schemaSet)
        {
            var xDoc = XDocument.Parse(xmlString);
            xDoc.Validate(schemaSet, null);
            return true;
        }
    }
}
