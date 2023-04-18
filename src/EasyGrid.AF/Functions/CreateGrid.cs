using EasyGrid.Core.Services;
using EasyGrid.Core.Services.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EasyGrid.AF.Functions
{
    public static class CreateGrid
    {
        private static readonly ICalculateGridService CalculateGridService = new CalculateGridService();

        [FunctionName("CreateGrid")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("CreateGrid function processed a request.");

            var minLat = double.Parse(req.Query["minLat"]);
            var minLon = double.Parse(req.Query["minLon"]);
            var maxLat = double.Parse(req.Query["maxLat"]);
            var maxLon = double.Parse(req.Query["maxLon"]);
            var squareSize = int.Parse(req.Query["squareSize"]);

            log.LogInformation($"Received request with parameters: minLat={minLat}, minLon={minLon}, maxLat={maxLat}, maxLon={maxLon}, squareSize={squareSize}");

            var grid = CalculateGridService.CreateGrid(minLat, minLon, maxLat, maxLon, squareSize);

            log.LogInformation("CreateGrid function completed successfully.");

            return new OkObjectResult(grid);
        }
    }
}
