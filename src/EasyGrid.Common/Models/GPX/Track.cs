using System.Xml.Serialization;

namespace EasyGrid.Common.Models.GPX
{
    [Serializable]
    public class Track
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlArray("trkseg")]
        [XmlArrayItem("trkpt")]
        public TrackPoint[] Points { get; set; }
    }
}
