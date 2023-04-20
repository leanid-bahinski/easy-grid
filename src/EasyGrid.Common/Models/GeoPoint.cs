namespace EasyGrid.Common.Models
{
    public class GeoPoint
    {
        public double Lat { get; private set; } 
        public double Lon { get; private set; }
        public string Name { get; private set; }

        public GeoPoint(double lat, double lon)
        {
            Lat = lat;
            Lon = lon;
            Name = string.Empty;
        }

        public void SetName(string name)
        {
            Name = name;
        }
    }
}
