using System.Xml.Serialization;

namespace EasyGrid.Common.Models.GPX
{
    [Serializable]
    [XmlRoot("gpx", Namespace = "http://www.topografix.com/GPX/1/0")]
    public class Gpx
    {
        [XmlAttribute("creator")]
        public string Creator { get; set; }

        [XmlElement("metadata")]
        public Metadata MetaData { get; set; }

        [XmlElement("trk")]
        public Track TrackCollection { get; set; }

        [XmlElement("wpt")]
        public Point[] Points { get; set; }
    }
}
