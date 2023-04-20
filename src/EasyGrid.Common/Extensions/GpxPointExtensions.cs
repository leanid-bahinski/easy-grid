using EasyGrid.Common.Models;

namespace EasyGrid.Common.Extensions
{
    public static class GpxPointExtensions
    {
        public static bool IsEmpty(this GeoPoint[,] grid)
        {
            return grid.GetLength(0) == 0 || 
                   grid.GetLength(1) == 0;
        }
    }
}
