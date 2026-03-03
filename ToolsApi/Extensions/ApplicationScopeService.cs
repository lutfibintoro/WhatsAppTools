using ToolsApi.Services.Implement;
using ToolsApi.Services.Interface;
using ToolsApi.WebSockets.Models;
using ToolsApi.WebSockets.Services.Implement;
using ToolsApi.WebSockets.Services.Interface;

namespace ToolsApi.Extensions
{
    public static class ApplicationScopeService
    {
        public static IServiceCollection AddApplicationScopeService(this IServiceCollection service)
        {
            // transient
            

            // scope
            service.AddScoped<DataStreamHeader>();
            service.AddScoped<IEchoService, EchoService>();
            service.AddScoped<ITransmisiManagerService, TransmisiManagerService>();

            // singelton
            service.AddSingleton<HeaderSize>();
            service.AddSingleton<IDataStreamValidationService, DataStreamValidationService>();

            return service;
        }
    }
}
