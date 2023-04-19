using EasyGrid.Core.Services;
using EasyGrid.Utils.Converters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EasyGrid.AF.Functions
{
    public class CreateGridFunction
    {
        private readonly ICalculateGridService _calculateGridService;
        private readonly IFindMeshTraversalPathService _findMeshTraversalPathService;
        private readonly IGeoPointsToGpxConverter _geoPointsToGpxConverter;
        private readonly IGpxToStringConverter _gpxToStringConverter;

        public CreateGridFunction(
            ICalculateGridService calculateGridService,
            IFindMeshTraversalPathService findMeshTraversalPathService,
            IGeoPointsToGpxConverter geoPointsToGpxConverter,
            IGpxToStringConverter gpxToStringConverter)
        {
            _calculateGridService = calculateGridService;
            _findMeshTraversalPathService = findMeshTraversalPathService;
            _geoPointsToGpxConverter = geoPointsToGpxConverter;
            _gpxToStringConverter = gpxToStringConverter;
        }

        [FunctionName("CreateGrid")]
        public async Task<IActionResult> CreateGrid(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest request,
            ILogger log)
        {
            log.LogInformation("CreateGrid function processed a request.");

            var minLat = double.Parse(request.Query["minLat"]);
            var minLon = double.Parse(request.Query["minLon"]);
            var maxLat = double.Parse(request.Query["maxLat"]);
            var maxLon = double.Parse(request.Query["maxLon"]);
            var squareSize = int.Parse(request.Query["squareSize"]);

            log.LogInformation($"Received request with parameters: minLat={minLat}, minLon={minLon}, maxLat={maxLat}, maxLon={maxLon}, squareSize={squareSize}.");

            var grid = _calculateGridService.CreateGrid(minLat, minLon, maxLat, maxLon, squareSize);
            var path = _findMeshTraversalPathService.FindPathForGpx(grid.GetLength(0), grid.GetLength(1));
            var gridName = $"grid-{squareSize}-{DateTime.Now}";
            var gpx = _geoPointsToGpxConverter.ConvertToGpx(grid, path, "EasyGrid", gridName);

            log.LogInformation("CreateGrid function prepares results.");

            var xml = _gpxToStringConverter.ConvertToXml(gpx);
            var result = GenerateResult(xml, gridName);

            log.LogInformation("CreateGrid function completed successfully.");

            return result;
        }

        private static FileStreamResult GenerateResult(string content, string name)
        {
            var byteArray = Encoding.UTF8.GetBytes(content);
            var stream = new MemoryStream(byteArray);

            return new FileStreamResult(stream, "application/octet-stream")
            {
                FileDownloadName = $"{name}.gpx"
            };
        }
    }
}
