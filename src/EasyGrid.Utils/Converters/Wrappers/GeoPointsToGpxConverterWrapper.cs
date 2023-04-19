using EasyGrid.Common.Models;
using EasyGrid.Common.Models.GPX;
using EasyGrid.Utils.Converters.Implementation;
using System.Drawing;

namespace EasyGrid.Utils.Converters.Wrappers
{
    public class GeoPointsToGpxConverterWrapper : IGeoPointsToGpxConverter
    {
        public Gpx ConvertToGpx(GeoPoint[,] grid, IEnumerable<Point> path, string creatorName, string trackName) 
            => GeoPointsToGpxConverter.ConvertToGpx(grid, path, creatorName, trackName);
    }
}