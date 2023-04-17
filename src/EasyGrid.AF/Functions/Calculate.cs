using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EasyGrid.AF.Functions
{
    public static class Calculate
    {
        [FunctionName("Calculate")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];
            int x = int.Parse(req.Query["x"]);
            int y = int.Parse(req.Query["y"]);
            int result = x + y;

            return new OkObjectResult(result);
        }
    }
}
