using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using duholiya.communication.service.data.Contracts;
using duholiya.communication.service.data;

[assembly: FunctionsStartup(typeof(duholiya.communication.service.functions.Startup))]
namespace duholiya.communication.service.functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //builder.Services.AddHttpClient();

            builder.Services.AddSingleton<IOtpRepository, OtpRepository>();
           // builder.Services.AddSingleton<ILoggerProvider, MyLoggerProvider>();
        }
    }
}
