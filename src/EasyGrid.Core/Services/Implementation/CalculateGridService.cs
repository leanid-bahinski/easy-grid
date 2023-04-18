using EasyGrid.Common;
using EasyGrid.Common.Enums;
using EasyGrid.Common.Models;

namespace EasyGrid.Core.Services.Implementation
{
    public class CalculateGridService : ICalculateGridService
    {
        public GeoPoint[,] CreateGrid(double minLat, double minLon, double maxLat, double maxLon, int squareSize)
        {
            if (squareSize < 1)
                throw new ArgumentException("Argument cannot be less than 1", nameof(squareSize));

            var startPoint = new GeoPoint(maxLat, minLon);
            var endPoint = new GeoPoint(minLat, maxLon);
            var numberOfPointsByLat = GetNumberOfSquaresByLat(startPoint, endPoint, squareSize) + 1;
            var numberOfPointsByLon = GetNumberOfSquaresByLon(startPoint, endPoint, squareSize) + 1;

            if (numberOfPointsByLon < 2 || numberOfPointsByLat < 2)
            {
                return new GeoPoint[0, 0];
            }

            var grid = new GeoPoint[numberOfPointsByLon, numberOfPointsByLat];

            for (var j = 0; j < numberOfPointsByLat; j++)
            {
                if (j == 0)
                {
                    grid[0, 0] = startPoint;
                }
                else
                {
                    var step = CalcStep(grid[0, j - 1], Direction.Down, squareSize);
                    grid[0, j] = CalcNextPoint(grid[0, j - 1], step);
                }

                for (var i = 1; i < numberOfPointsByLon; i++)
                {
                    var step = CalcStep(grid[i - 1, j], Direction.Right, squareSize);
                    grid[i, j] = CalcNextPoint(grid[i - 1, j], step);
                }
            }

            return grid;
        }

        private static GeoPoint CalcStep(GeoPoint startPoint, Direction directory, int distance)
        {
            var angle = Constants.RadAngle[directory];
            var stepLat = Constants.CircleAngle * Math.Sin(angle) * distance / Constants.Planet.Lat;
            var stepLon = Constants.CircleAngle * Math.Cos(angle) * distance / (Constants.Planet.Lon * Math.Cos(startPoint.Lat * Constants.Rad));
            return new GeoPoint(stepLat, stepLon);
        }

        private static GeoPoint CalcNextPoint(GeoPoint startPoint, GeoPoint step)
        {
            return new GeoPoint(startPoint.Lat + step.Lat, startPoint.Lon + step.Lon);
        }

        private static int GetNumberOfSquaresByLon(GeoPoint startPoint, GeoPoint endPoint, int squareSize)
        {
            const Direction direction = Direction.Right;
            var currentPoint = startPoint;
            var step = CalcStep(currentPoint, direction, squareSize);
            var width = 0;

            while (currentPoint.Lon + (step.Lon / 2) < endPoint.Lon)
            {
                currentPoint = CalcNextPoint(currentPoint, step);
                step = CalcStep(currentPoint, direction, squareSize);
                width++;
            }

            return width;
        }

        private static int GetNumberOfSquaresByLat(GeoPoint startPoint, GeoPoint endPoint, int squareSize)
        {
            const Direction direction = Direction.Down;
            var currentPoint = startPoint;
            var step = CalcStep(currentPoint, direction, squareSize);
            var height = 0;

            while (currentPoint.Lat + (step.Lat / 2) > endPoint.Lat)
            {
                currentPoint = CalcNextPoint(currentPoint, step);
                step = CalcStep(currentPoint, direction, squareSize);
                height++;
            }

            return height;
        }
    }
}