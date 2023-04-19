using EasyGrid.Common.Models.GPX;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace EasyGrid.Utils.Converters.Implementation
{
    public static class GpxToStringConverter
    {
        public static string ConvertToXml(Gpx gpx)
        {
            var serializer = new XmlSerializer(typeof(Gpx));
            var settings = new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 };

            using var memoryStream = new MemoryStream();
            using var xmlWriter = XmlWriter.Create(memoryStream, settings);

            serializer.Serialize(xmlWriter, gpx);
            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }
    }
}
