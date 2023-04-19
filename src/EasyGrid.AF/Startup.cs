using EasyGrid.Core.Services;
using EasyGrid.Core.Services.Implementation;
using EasyGrid.Utils.Converters;
using EasyGrid.Utils.Converters.Wrappers;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(EasyGrid.AF.Startup))]

namespace EasyGrid.AF
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<ICalculateGridService, CalculateGridService>();
            builder.Services.AddSingleton<IFindMeshTraversalPathService, FindMeshTraversalPathService>();
            builder.Services.AddTransient<IGeoPointsToGpxConverter, GeoPointsToGpxConverterWrapper>();
            builder.Services.AddTransient<IGpxToStringConverter, GpxToStringConverterWrapper>();
        }
    }
}
