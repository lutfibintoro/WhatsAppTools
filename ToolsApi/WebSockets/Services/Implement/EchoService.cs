using System;
using System.Net.WebSockets;
using System.Text;
using Microsoft.Extensions.Logging;
using ToolsApi.WebSockets.Services.Interface;

namespace ToolsApi.WebSockets.Services.Implement
{
    public partial class EchoService : IEchoService
    {
        private readonly ILogger<EchoService> _logger;
        private WebSocket? _websocket;
        private WebSocketReceiveResult? _receiveResult;

        public EchoService(ILogger<EchoService> logger)
        {
            _logger = logger;
        }


        public async Task HandshakeAsync(WebSocket webSocket)
        {
            try
            {
                byte[] buffer = new byte[1024];
                _websocket = webSocket;

                using var cancelationToken = new CancellationTokenSource(TimeSpan.FromSeconds(3));

                _receiveResult = await _websocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer),
                    cancelationToken.Token);

                if (!_receiveResult.EndOfMessage)
                    throw new ArgumentOutOfRangeException(nameof(webSocket), "The handshake message is too large.");

                await _websocket.SendAsync(
                    new ArraySegment<byte>(buffer, 0, _receiveResult.Count),
                    _receiveResult.MessageType,
                    false,
                    CancellationToken.None);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogWarning("[{timestamp}] [{message} because client do not send handshake message]", DateTime.UtcNow, ex.Message);
                await CloseWebsocketAsync();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                _logger.LogWarning("[{timestamp}] [{message}]", DateTime.UtcNow, ex.Message);
                await CloseWebsocketAsync();
            }
            catch (WebSocketException ex)
            {
                _logger.LogWarning("[{timestamp}] [{message}]", DateTime.UtcNow, ex.Message);
                await CloseWebsocketAsync();
            }
        }


        public async Task EchoAsync()
        {
            if (_receiveResult is null || _websocket is null)
                return;

            byte[] buffer = new byte[1024 * 4];


            do
            {
                await _websocket.SendAsync(
                    new ArraySegment<byte>(buffer, 0, _receiveResult.Count),
                    _receiveResult.MessageType,
                    _receiveResult.EndOfMessage,
                    CancellationToken.None);

                _receiveResult = await _websocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                // tutup koneksi websocket jika client menutup secara baik baik
                if (_receiveResult.CloseStatus.HasValue)
                {
                    await CloseWebsocketAsync();
                    break;
                }


            } while (!_receiveResult.CloseStatus.HasValue);
        }


        public async Task CloseSuggestAsync()
        {
            if (_receiveResult is null)
                return;

            if (_websocket.State != WebSocketState.Aborted)
                return;
        }
    }





    public partial class EchoService
    {
        private async Task CloseWebsocketAsync()
        {
            if (_websocket is null || _receiveResult is null)
                return;

            if (_websocket.State != WebSocketState.Closed)
                await _websocket.CloseAsync(
                    WebSocketCloseStatus.NormalClosure,
                    _receiveResult.CloseStatusDescription,
                    CancellationToken.None);
        }


        private void MemoryTempStore(MemoryStream memoryStream, byte[] bytes)
        {
        }

        private void DiskTempStore(FileStream fileStream, byte[] bytes)
        {
        }
    }
}
