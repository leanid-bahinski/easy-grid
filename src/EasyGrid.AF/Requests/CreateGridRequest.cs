using Newtonsoft.Json;

namespace EasyGrid.AF.Requests
{
    public class CreateGridRequest
    {
        [JsonProperty("minLat")] 
        public double? MinLat { get; set; }

        [JsonProperty("minLon")]
        public double? MinLon { get; set; }

        [JsonProperty("maxLat")]
        public double? MaxLat { get; set; }

        [JsonProperty("maxLon")]
        public double? MaxLon { get; set; }

        [JsonProperty("squareSize")]
        public int? SquareSize { get; set; }

        public double GetMinLat() => MinLat!.Value;
        public double GetMinLon() => MinLon!.Value;
        public double GetMaxLat() => MaxLat!.Value;
        public double GetMaxLon() => MaxLon!.Value;
        public int GetSquareSize() => SquareSize!.Value;
    }
}