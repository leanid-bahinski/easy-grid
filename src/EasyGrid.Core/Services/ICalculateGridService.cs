using EasyGrid.Common.Models;

namespace EasyGrid.Core.Services
{
    public interface ICalculateGridService
    {
        GeoPoint[,] CreateGrid(double minLat, double minLon, double maxLat, double maxLon, int squareSize);
    }
}
