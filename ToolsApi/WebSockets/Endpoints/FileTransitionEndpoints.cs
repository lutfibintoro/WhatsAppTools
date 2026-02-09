using System.Net.WebSockets;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ToolsApi.Models.Dto.Response;
using ToolsApi.WebSockets.Services.Interface;

namespace ToolsApi.WebSockets.Endpoints
{
    public static class FileTransitionEndpoints
    {
        public static WebApplication MapFileTransitionEndpoints(this WebApplication app)
        {
            app.MapGet("/ws/file/transition", async Task
                (HttpContext context,
                [FromQuery] int? phoneNumber,
                [FromServices] ILogger<Program> logger,
                [FromServices] ITransmisiManagerService transmisiManagerService) =>
            {
                if (!context.WebSockets.IsWebSocketRequest)
                    throw new WebSocketException("upgrade protokol ke websocket");

                using WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                await transmisiManagerService.HandshakeAsync(webSocket);
                await transmisiManagerService.IdleAsync();
            });


            return app;
        }
    }
}
