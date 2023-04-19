using EasyGrid.Common.Enums;
using System.Drawing;

namespace EasyGrid.Core.Services.Implementation
{
    public class FindMeshTraversalPathService : IFindMeshTraversalPathService
    {
        public IEnumerable<Point> FindPathForGpx(int width, int height)
        {
            var points = new List<Point>();
            var currentPoint = new Point(0, 0);

            var horizontalDirection = Direction.Right;
            var verticalDirection = Direction.Down;
            var iteration = 0;

            while (true)
            {
                points.Add(currentPoint);

                if (iteration == 0)
                {
                    if (!TryMovePoint(width, height, horizontalDirection, ref currentPoint))
                    {
                        horizontalDirection = RevertDirection(horizontalDirection);

                        if (!TryMovePoint(width, height, verticalDirection, ref currentPoint))
                        {
                            iteration++;
                            verticalDirection = RevertDirection(verticalDirection);
                        }
                    }
                }

                if (iteration == 1)
                {
                    if (!TryMovePoint(width, height, verticalDirection, ref currentPoint))
                    {
                        verticalDirection = RevertDirection(verticalDirection);

                        if (!TryMovePoint(width, height, horizontalDirection, ref currentPoint))
                        {
                            iteration++;
                            horizontalDirection = RevertDirection(horizontalDirection);
                        }
                    }
                }

                if (iteration == 2)
                {
                    break;
                }
            }

            return points;
        }

        public IEnumerable<Point> FindPathForGpx2(int width, int height)
        {
            var points = new List<Point>();

            var currentPoint = new Point(0, 0);
            var horizontalDirection = Direction.Right;
            var verticalDirection = Direction.Down;
            var iteration = 0;

            while (iteration < 3)
            {
                points.Add(currentPoint);

                var direction = iteration switch
                {
                    0 => horizontalDirection,
                    1 => verticalDirection,
                    _ => throw new InvalidOperationException("Unexpected iteration value."),
                };

                if (TryMovePoint(width, height, direction, ref currentPoint))
                {
                    continue;
                }

                switch (iteration)
                {
                    case 0:
                        horizontalDirection = RevertDirection(horizontalDirection);
                        break;
                    case 1:
                        verticalDirection = RevertDirection(verticalDirection);
                        break;
                }

                iteration++;
            }

            return points;
        }

        private static bool TryMovePoint(int width, int height, Direction direction, ref Point point)
        {
            switch (direction)
            {
                case Direction.Up when point.Y > 0:
                    point.Y--;
                    return true;
                case Direction.Right when point.X < width - 1:
                    point.X++;
                    return true;
                case Direction.Down when point.Y < height - 1:
                    point.Y++;
                    return true;
                case Direction.Left when point.X > 0:
                    point.X--;
                    return true;
                default:
                    return false;
            }
        }

        private static Direction RevertDirection(Direction direction)
        {
            return direction switch
            {
                Direction.Up => Direction.Down,
                Direction.Right => Direction.Left,
                Direction.Down => Direction.Up,
                Direction.Left => Direction.Right,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
    }
}
