namespace EasyGrid.Common.Models
{
    public class GeoPoint
    {
        public string Name { get; private set; }
        public double Lat { get; private set; } 
        public double Lon { get; private set; }

        public GeoPoint(double lat, double lon)
        {
            Name = string.Empty;
            Lat = lat;
            Lon = lon;
        }

        public void SetName(string name)
        {
            Name = name;
        }
    }
}
