using System.Net.WebSockets;
using ToolsApi.WebSockets.Models;
using ToolsApi.WebSockets.Services.Interface;

namespace ToolsApi.WebSockets.Services.Implement
{
    // | 4B magic | 16B transferId | 1B flags | 2B objective | 2B mimeType | 8B offset | 4B length | (1024 * 4)B payload |
    public class TransmisiManagerService : ITransmisiManagerService
    {
        private readonly ILogger<TransmisiManagerService> _logger;
        private WebSocket? _websocket;
        private WebSocketReceiveResult? _receiveResult;
        private readonly DataStreamHeader _dataStreamHeader;

        public TransmisiManagerService(ILogger<TransmisiManagerService> logger, DataStreamHeader dataStreamHeader)
        {
            _logger = logger;
            _dataStreamHeader = dataStreamHeader;
        }

        public async Task HandshakeAsync(WebSocket webSocket)
        {
            throw new NotImplementedException();
        }

        public async Task IdleAsync()
        {
            throw new NotImplementedException();
        }
    }
}
