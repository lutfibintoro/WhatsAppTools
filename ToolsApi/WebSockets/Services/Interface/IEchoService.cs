using System.Net.WebSockets;

namespace ToolsApi.WebSockets.Services.Interface
{
    public interface IEchoService
    {
        public Task HandshakeAsync(WebSocket webSocket);

        public Task EchoAsync();
    }
}
