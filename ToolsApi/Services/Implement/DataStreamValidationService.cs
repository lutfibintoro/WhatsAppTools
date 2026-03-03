using System.Net.WebSockets;
using ToolsApi.Services.Interface;
using ToolsApi.WebSockets.Models;

namespace ToolsApi.Services.Implement
{
    public class DataStreamValidationService : IDataStreamValidationService
    {
        private readonly HeaderSize _headerSize;

        public DataStreamValidationService(HeaderSize headerSize)
        {
            _headerSize = headerSize;
        }

        public bool IsFileOperationMagicNumber(Span<byte> userStream, DataStreamHeader streamHeader)
        {
            if (userStream.IsEmpty || userStream.Length < _headerSize.MagicCountPosition)
                return false;

            Span<byte> magicNumber = userStream.Slice(_headerSize.MagicIndex, _headerSize.Magic);

            return streamHeader.Magic.SequenceEqual(magicNumber);
        }

        public bool IsValidTransferId(Span<byte> userStream, DataStreamHeader streamHeader)
        {
            if (userStream.IsEmpty || userStream.Length < _headerSize.TransferIdCountPosition)
                return false;

            Span<byte> transferId = userStream.Slice(_headerSize.TransferIdIndex, _headerSize.TransferId);

            return streamHeader.TransferId.SequenceEqual(transferId);
        }

        public bool StreamIsToBig(WebSocketReceiveResult receiveResult)
            => !receiveResult.EndOfMessage;
    }
}
