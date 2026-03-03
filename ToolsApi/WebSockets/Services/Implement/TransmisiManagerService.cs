using System.Net.WebSockets;
using ToolsApi.Services.Interface;
using ToolsApi.WebSockets.Models;
using ToolsApi.WebSockets.Services.Interface;

namespace ToolsApi.WebSockets.Services.Implement
{
    // | 4B magic | 16B transferId | 2B flags | 4B objective | 2B mimeType | (1024 * 4)B payload |
    public partial class TransmisiManagerService : ITransmisiManagerService
    {
        private readonly ILogger<TransmisiManagerService> _logger;
        private readonly IDataStreamValidationService _dataStreamValidationService;

        private WebSocket? _websocket;
        private WebSocketReceiveResult? _receiveResult;
        private readonly DataStreamHeader _dataStreamHeader;
        private readonly HeaderSize _headerSize;

        public TransmisiManagerService
            (ILogger<TransmisiManagerService> logger,
            DataStreamHeader dataStreamHeader,
            IDataStreamValidationService dataStreamValidationService,
            HeaderSize headerSize)
        {
            _logger = logger;
            _dataStreamHeader = dataStreamHeader;
            _dataStreamValidationService = dataStreamValidationService;
            _headerSize = headerSize;
        }

        public async Task HandshakeAsync(WebSocket webSocket)
        {
            byte[] buffer = new byte[_headerSize.Size];
            _websocket = webSocket;
            using CancellationTokenSource cancellation = new(TimeSpan.FromSeconds(30));

            _receiveResult = await _websocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellation.Token);
            
            throw new NotImplementedException();
        }
        
        public async Task IdleAsync()
        {
            throw new NotImplementedException();
        }
    }
}