using System.Xml.Serialization;

namespace EasyGrid.Common.Models.GPX
{
    [Serializable]
    public class GpxMetadata
    {
        [XmlElement("time")]
        public string Time { get; set; }

        [XmlElement("bounds")]
        public GpxBounds Bounds { get; set; }
    }
}
