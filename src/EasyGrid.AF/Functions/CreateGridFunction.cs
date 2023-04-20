using EasyGrid.AF.Parameters;
using EasyGrid.Common;
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
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "create-grid")] CreateGridParameters parameters,
            ILogger log)
        {
            log.LogInformation("CreateGrid function processed a request.");
            log.LogInformation($"Received request with param: {parameters}.");

            var grid = _calculateGridService.CreateGrid(parameters.MinLat, parameters.MinLon, parameters.MaxLat, parameters.MaxLon, parameters.SquareSize);
            var path = _findMeshTraversalPathService.FindPathForGpx(grid.GetLength(0), grid.GetLength(1));
            var gridName = $"grid-{parameters.SquareSize} {DateTime.Now}";
            var gpx = _geoPointsToGpxConverter.ConvertToGpx(grid, path, "EasyGrid", gridName);
            var xml = _gpxToStringConverter.ConvertToXml(gpx);

            log.LogInformation("CreateGrid function prepares results.");

            var result = GenerateFileResult(xml, gridName, ExtensionNames.Gpx);

            log.LogInformation("CreateGrid function completed successfully.");

            return result;
        }

        private static ActionResult GenerateFileResult(string content, string name, string type)
        {
            var byteArray = Encoding.UTF8.GetBytes(content);
            var stream = new MemoryStream(byteArray);

            return new FileStreamResult(stream, "application/octet-stream")
            {
                FileDownloadName = $"{name}.{type}"
            };
        }
    }
}
