using EasyGrid.AF.Validators;
using EasyGrid.Core.Services;
using EasyGrid.Core.Services.Implementation;
using EasyGrid.Utils.Converters;
using EasyGrid.Utils.Converters.Wrappers;
using FluentValidation;
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

            //Services
            builder.Services.AddSingleton<ICalculateGridService, CalculateGridService>();
            builder.Services.AddSingleton<IFindPathService, FindPathService>();

            //Converters
            builder.Services.AddTransient<IGeoPointsToGpxConverter, GeoPointsToGpxConverterWrapper>();
            builder.Services.AddTransient<IGpxToStringConverter, GpxToStringConverterWrapper>();

            //Validators
            builder.Services.AddValidatorsFromAssemblyContaining<CreateGridRequestValidator>();
        }
    }
}
