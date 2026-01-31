using System.Net;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Mvc;
using ToolsApi.WebSockets.Services.Interface;

namespace ToolsApi.WebSockets.Endpoints
{
    public static class EchoEndpoints
    {
        public static WebApplication MapEchoEndpoints(this WebApplication app)
        {
            app.MapGet("/ws/echo", async
                (HttpContext context,
                [FromServices] ILogger<Program> logger,
                [FromServices] IEchoService echoService) =>
            {
                logger.LogInformation("berak");

                if (!context.WebSockets.IsWebSocketRequest)
                    throw new WebSocketException("upgrade protokol ke websocket");


                using WebSocket websocket = await context.WebSockets.AcceptWebSocketAsync();
                await echoService.HandshakeAsync(websocket);
                await echoService.EchoAsync();
            });

            return app;
        }
    }
}
