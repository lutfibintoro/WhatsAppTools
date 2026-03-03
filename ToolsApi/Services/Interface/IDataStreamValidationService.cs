using System.Net.WebSockets;
using ToolsApi.WebSockets.Models;

namespace ToolsApi.Services.Interface
{
    public interface IDataStreamValidationService
    {
        /// <summary>
        /// validate user magic number
        /// </summary>
        /// <param name="userMagic"></param>
        /// <param name="magicNumber"></param>
        /// <returns>true if equals otherwise false</returns>
        public bool IsFileOperationMagicNumber(Span<byte> userStream, DataStreamHeader streamHeader);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userStream"></param>
        /// <param name="streamHeader"></param>
        /// <returns></returns>
        public bool IsValidTransferId(Span<byte> userStream, DataStreamHeader streamHeader);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="receiveResult"></param>
        /// <returns>true if the received data is too large, otherwise false</returns>
        public bool StreamIsToBig(WebSocketReceiveResult receiveResult);
    }
}
