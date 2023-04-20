using EasyGrid.Common.Extensions;
using EasyGrid.Common.Models;
using EasyGrid.Common.Models.GPX;
using System.Drawing;

namespace EasyGrid.Utils.Converters.Implementation
{
    public static class GeoPointsToGpxConverter
    {
        private const string DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss'Z'";

        public static Gpx ConvertToGpx(GeoPoint[,] grid, IEnumerable<Point> path, string creatorName, string trackName)
        {
            var metadata = CreateMetadata(grid, DateTimeFormat);
            var track = CreateTrack(grid, path, trackName);
            var points = CreatePoints(grid);
            return CreateGpx(creatorName, metadata, track, points);
        }

        private static GpxMetadata CreateMetadata(GeoPoint[,] grid, string dateTimeFormat)
        {
            return new GpxMetadata
            {
                Time = DateTime.Now.ToString(dateTimeFormat),
                Bounds = CreateBounds(grid)
            };
        }

        private static GpxBounds? CreateBounds(GeoPoint[,] grid)
        {
            if (grid.IsEmpty())
            {
                return null;
            }

            var gridArray = grid.Cast<GeoPoint>().ToArray();

            return new GpxBounds
            {
                MinLat = gridArray.Min(m => m.Lat),
                MinLon = gridArray.Min(m => m.Lon),
                MaxLat = gridArray.Max(m => m.Lat),
                MaxLon = gridArray.Max(m => m.Lon)
            };
        }

        private static GpxTrack? CreateTrack(GeoPoint[,] grid, IEnumerable<Point> path, string trackName)
        {
            if (grid.IsEmpty())
            {
                return null;
            }

            return new GpxTrack
            {
                Name = trackName,
                Points = GenerateTrack(grid, path).ToArray()
            };
        }

        private static IEnumerable<GpxTrackPoint> GenerateTrack(GeoPoint[,] grid, IEnumerable<Point> path)
        {
            return path.Select(point => new GpxTrackPoint()
            {
                Lat = grid[point.X, point.Y].Lat,
                Lon = grid[point.X, point.Y].Lon
            });
        }

        private static GpxPoint[]? CreatePoints(GeoPoint[,] grid)
        {
            if (grid.IsEmpty())
            {
                return null;
            }

            return grid.Cast<GeoPoint>()
                       .Select(geo => new GpxPoint { Name = geo.Name, Lat = geo.Lat, Lon = geo.Lon })
                       .ToArray();
        }

        private static Gpx CreateGpx(string creatorName, GpxMetadata metadata, GpxTrack? track, GpxPoint[]? points)
        {
            return new Gpx
            {
                Creator = creatorName,
                MetaData = metadata,
                TrackCollection = track,
                Points = points
            };
        }
    }
}