using System.Drawing;
using EasyGrid.Common.Models;
using EasyGrid.Common.Models.GPX;

namespace EasyGrid.Utils.Converters
{
    public interface IGeoPointsToGpxConverter
    {
        Gpx ConvertToGpx(GeoPoint[,] grid, IEnumerable<Point> path, string creatorName, string trackName);
    }
}
