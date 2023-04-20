namespace EasyGrid.AF.Parameters
{
    public class CreateGridParameters
    {
        public double MinLat { get; set; }
        public double MinLon { get; set; }
        public double MaxLat { get; set; }
        public double MaxLon { get; set; }
        public int SquareSize { get; set; } = 500;

        public override string ToString()
        {
            return $"minLat={MinLat}, minLon={MinLon}, maxLat={MaxLat}, maxLon={MaxLon}, squareSize={SquareSize}";
        }
    }
}
