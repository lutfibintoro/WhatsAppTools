using ToolsApi.WebSockets.Endpoints;

namespace ToolsApi.Extensions
{
    public static class Endpoints
    {
        public static WebApplication MapEndpoints(this WebApplication app)
        {
            // restfull endpoints


            // websocket endpoint
            app.MapEchoEndpoints();

            return app;
        }
    }
}
