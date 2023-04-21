using EasyGrid.AF.Requests;
using EasyGrid.Common.Constants;
using EasyGrid.Core.Services;
using EasyGrid.Utils.Converters;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace EasyGrid.AF.Functions
{
    public class CreateGridFunction
    {
        private readonly ICalculateGridService _calculateGridService;
        private readonly IFindPathService _findMeshTraversalPathService;
        private readonly IGeoPointsToGpxConverter _geoPointsToGpxConverter;
        private readonly IGpxToStringConverter _gpxToStringConverter;
        private readonly IValidator<CreateGridRequest> _validator;

        public CreateGridFunction(
            ICalculateGridService calculateGridService,
            IFindPathService findMeshTraversalPathService,
            IGeoPointsToGpxConverter geoPointsToGpxConverter,
            IGpxToStringConverter gpxToStringConverter,
            IValidator<CreateGridRequest> validator)
        {
            _calculateGridService = calculateGridService;
            _findMeshTraversalPathService = findMeshTraversalPathService;
            _geoPointsToGpxConverter = geoPointsToGpxConverter;
            _gpxToStringConverter = gpxToStringConverter;
            _validator = validator;
        }

        [FunctionName(nameof(CreateGrid))]
        public async Task<IActionResult> CreateGrid(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "create-grid")]
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation("CreateGrid function processed a request. {@CreateGridRequest}", req);

            // Deserialize object
            var json = await req.ReadAsStringAsync();
            var param = JsonConvert.DeserializeObject<CreateGridRequest>(json);
            log.LogDebug($"Received request with parameters: {param}.");

            // Validating
            var validationResult = await _validator.ValidateAsync(param);
            if (!validationResult.IsValid)
            {
                return GenerateBadRequestObjectResult(validationResult);
            }
            log.LogDebug("Received request valid.");

            //Calculation
            var grid = _calculateGridService.CreateGrid(param.GetMinLat(), param.GetMinLon(), param.GetMaxLat(), param.GetMaxLon(), param.GetSquareSize());
            var path = _findMeshTraversalPathService.FindPathForGpx(grid.GetLength(0), grid.GetLength(1));
            var gridName = $"grid-{param.SquareSize} {DateTime.Now}";
            var gpx = _geoPointsToGpxConverter.ConvertToGpx(grid, path, "EasyGrid", gridName);
            var xml = _gpxToStringConverter.ConvertToXml(gpx);
            
            //Result
            log.LogDebug("CreateGrid function prepares results.");
            var result = GenerateFileResult(xml, gridName, ExtensionConstants.Gpx);
            log.LogInformation("CreateGrid function completed successfully.");
            return result;
        }

        private static BadRequestObjectResult GenerateBadRequestObjectResult(ValidationResult validationResult)
        {
            var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }).ToList();
            return new BadRequestObjectResult(errors);
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
