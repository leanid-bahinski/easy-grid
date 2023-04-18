using EasyGrid.Common.Models.GPX;
using System.Xml;
using System.Xml.Serialization;

namespace EasyGrid.Utils.Converters
{
    public static class GpxToStringConverter
    {
        public static string ConvertToXml(Gpx gpx)
        {
            var serializer = new XmlSerializer(typeof(Gpx));
            var settings = new XmlWriterSettings { Indent = true };

            using var stringWriter = new StringWriter();
            using var xmlWriter = XmlWriter.Create(stringWriter, settings);

            serializer.Serialize(xmlWriter, gpx);
            return stringWriter.ToString();
        }
    }
}
