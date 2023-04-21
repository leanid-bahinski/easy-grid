using System.Drawing;

namespace EasyGrid.Core.Services
{
    public interface IFindPathService
    {
        IEnumerable<Point> FindPathForGpx(int width, int height);
    }
}
