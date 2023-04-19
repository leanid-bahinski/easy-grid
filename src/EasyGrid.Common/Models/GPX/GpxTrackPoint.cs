using System.Xml.Serialization;

namespace EasyGrid.Common.Models.GPX
{
    [Serializable]
    public class GpxTrackPoint
    {
        [XmlAttribute("lat")]
        public double Lat { get; set; }

        [XmlAttribute("lon")]
        public double Lon { get; set; }
    }
}
