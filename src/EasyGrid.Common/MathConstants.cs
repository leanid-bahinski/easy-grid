using EasyGrid.Common.Enums;

namespace EasyGrid.Common
{
    public static class MathConstants
    {
        public const double CircleAngle = 360.0;
        public const double HalfCircleAngle = 180.0;
        public const double Rad = Math.PI / HalfCircleAngle;

        public static class Planet
        {
            public const double Lat = 40035000;
            public const double Lon = 40075000;
        }

        public static Dictionary<Direction, double> RadAngle = new()
        {
            { Direction.Up, 90.0 * Rad },
            { Direction.Right, 0 },
            { Direction.Down, 270.0 * Rad},
            { Direction.Left, 180.0 * Rad }
        };
    }
}