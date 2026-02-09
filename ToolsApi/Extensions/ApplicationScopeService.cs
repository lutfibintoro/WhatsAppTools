using ToolsApi.WebSockets.Models;
using ToolsApi.WebSockets.Services.Implement;
using ToolsApi.WebSockets.Services.Interface;

namespace ToolsApi.Extensions
{
    public static class ApplicationScopeService
    {
        public static IServiceCollection AddApplicationScopeService(this IServiceCollection service)
        {
            //
            service.AddScoped<DataStreamHeader>();

            // scope
            service.AddScoped<IEchoService, EchoService>();
            service.AddScoped<ITransmisiManagerService, TransmisiManagerService>();

            // singelton

            return service;
        }
    }
}
