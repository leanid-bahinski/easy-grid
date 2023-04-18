using EasyGrid.Common.Models;
using System.Xml.Serialization;

namespace EasyGrid.Utils.Converters
{
    public class GeoPointArrayToXmlConverter
    {
        public static string ConvertToXml(GeoPoint[,] points)
        {
            var serializer = new XmlSerializer(typeof(GeoPoint[,]));
            using var writer = new StringWriter { NewLine = null };
            serializer.Serialize(writer, points);
            return writer.ToString();
        }
    }
}