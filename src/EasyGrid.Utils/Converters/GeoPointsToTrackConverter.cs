using EasyGrid.Common.Enums;
using EasyGrid.Common.Models;
using EasyGrid.Common.Models.GPX;

namespace EasyGrid.Utils.Converters
{
    internal static class GeoPointsToTrackConverter
    {
        public static TrackPoint[] ConvertToTrackPoints(GeoPoint[,] grid)
        {
            var points = new List<(int, int)>();

            var i = 0;
            var j = 0;
            var width = grid.GetLength(1);
            var height = grid.GetLength(0);
            var horizontalDirection = Direction.Right;
            var verticalDirection = Direction.Down;
            var iteration = 0;

            while (true)
            {
                points.Add((i, j));

                if (iteration == 0)
                {
                    if (!MovePoint(width, height, horizontalDirection, ref i, ref j))
                    {
                        horizontalDirection = RevertDirection(horizontalDirection);

                        if (!MovePoint(width, height, verticalDirection, ref i, ref j))
                        {
                            iteration++;
                            verticalDirection = RevertDirection(verticalDirection);
                        }
                    }
                }

                if (iteration == 1)
                {
                    if (!MovePoint(width, height, verticalDirection, ref i, ref j))
                    {
                        verticalDirection = RevertDirection(verticalDirection);

                        if (!MovePoint(width, height, horizontalDirection, ref i, ref j))
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

            return points.Select(s => new TrackPoint() { Lat = grid[s.Item2, s.Item1].Lat, Lon = grid[s.Item2, s.Item1].Lon }).ToArray();
        }

        private static bool MovePoint(int width, int height, Direction direction, ref int i, ref int j)
        {
            switch (direction)
            {
                case Direction.Up when j > 0:
                    j--; return true;
                case Direction.Right when i < width - 1:
                    i++; return true;
                case Direction.Down when j < height - 1:
                    j++; return true;
                case Direction.Left when i > 0:
                    i--; return true;
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
