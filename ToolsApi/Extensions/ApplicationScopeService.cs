using ToolsApi.WebSockets.Services.Implement;
using ToolsApi.WebSockets.Services.Interface;

namespace ToolsApi.Extensions
{
    public static class ApplicationScopeService
    {
        public static IServiceCollection AddApplicationScopeService(this IServiceCollection service)
        {
            service.AddScoped<IEchoService, EchoService>();

            return service;
        }
    }
}
