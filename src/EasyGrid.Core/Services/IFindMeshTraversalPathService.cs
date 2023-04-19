using System.Drawing;

namespace EasyGrid.Core.Services
{
    public interface IFindMeshTraversalPathService
    {
        IEnumerable<Point> FindPathForGpx(int width, int height);
    }
}
