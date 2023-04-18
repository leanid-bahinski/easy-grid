using EasyGrid.Common.Models;
using EasyGrid.Common.Models.GPX;

namespace EasyGrid.Utils.Converters
{
    public class GeoPointsToGpxConverter
    {
        private const string CreatorName = "EasyGrid";
        private const string TrackName = "grid";
        private const string DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss'Z'";

        public static Gpx ConvertToGpx(GeoPoint[,] grid)
        {
            var metadata = CreateMetadata(grid);
            var track = CreateTrack(grid, TrackName);
            var points = CreatePoints(grid);
            return CreateGpx(CreatorName, metadata, track, points);
        }

        private static Metadata CreateMetadata(GeoPoint[,] grid)
        {
            return new Metadata()
            {
                Time = DateTime.Now.ToString(DateTimeFormat),
                Bounds = CreateBounds(grid)
            };
        }

        private static Bounds CreateBounds(GeoPoint[,] grid)
        {
            var gridArray = grid.Cast<GeoPoint>().ToArray();

            return new Bounds()
            {
                MinLat = gridArray.Min(m => m.Lat),
                MinLon = gridArray.Min(m => m.Lon),
                MaxLat = gridArray.Max(m => m.Lat),
                MaxLon = gridArray.Max(m => m.Lon)
            };
        }

        private static Track CreateTrack(GeoPoint[,] grid, string trackName)
        {
            return new Track()
            {
                Name = trackName,
                Points = GenerateTrack(grid)
            };
        }

        private static TrackPoint[] GenerateTrack(GeoPoint[,] grid)
        {
            return GeoPointsToTrackConverter.ConvertToTrackPoints(grid);
        }

        private static Point[] CreatePoints(GeoPoint[,] grid)
        {
            return grid.Cast<GeoPoint>()
                       .Select(geo => new Point() { Name = geo.Name, Lat = geo.Lat, Lon = geo.Lon })
                       .ToArray();
        }

        private static Gpx CreateGpx(string creatorName, Metadata metadata, Track track, Point[] points)
        {
            return new Gpx()
            {
                Creator = creatorName,
                MetaData = metadata,
                TrackCollection = track,
                Points = points
            };
        }
    }
}