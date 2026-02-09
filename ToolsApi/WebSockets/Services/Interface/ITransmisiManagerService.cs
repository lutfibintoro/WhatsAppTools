using System.Net.WebSockets;

namespace ToolsApi.WebSockets.Services.Interface
{
    public interface ITransmisiManagerService
    {
        public Task HandshakeAsync(WebSocket webSocket);
        public Task IdleAsync();
    }
}
