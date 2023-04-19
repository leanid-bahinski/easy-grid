using System.Xml.Serialization;

namespace EasyGrid.Common.Models.GPX
{
    [Serializable]
    public class GpxTrack
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlArray("trkseg")]
        [XmlArrayItem("trkpt")]
        public GpxTrackPoint[] Points { get; set; }
    }
}
