using System.Buffers.Binary;

namespace ToolsApi.WebSockets.Models
{
    // | 4B magic | 16B transferId | 2B flags | 4B objective | 2B mimeType | (1024 * 4)B payload |
    public partial class DataStreamHeader
    {
        private readonly HeaderSize _headerSize;

        private readonly byte[] _magic;
        private readonly byte[] _transferId = new byte[16];
        private readonly byte[] _payload = new byte[1024 * 4];


        public ReadOnlySpan<byte> Magic { get => _magic; }
        public ReadOnlySpan<byte> TransferId { get => _transferId; }
        public Flags Flags { get; set; }
        public Objective Objective { get; set; }
        public MimeType MimeType { get; set; }
        public ulong Offset { get; set; }
        public int Length { get; set; }
        public Span<byte> Payload { get => _payload; }


        public DataStreamHeader(IConfiguration configuration, HeaderSize headerSize)
        {
            _magic = configuration.GetValue<byte[]>("WebSocketRuRu:HeaderFileOperationMagicNumber") ?? throw new ArgumentNullException(nameof(configuration), "magic number harus memiliki nilai");
            _headerSize = headerSize;
        }
    }
}




namespace ToolsApi.WebSockets.Models
{
    public partial class DataStreamHeader
    {
        public Span<byte> NewTransferId()
        {
            Span<byte> newSpanId = new Guid().ToByteArray().AsSpan();
            newSpanId.CopyTo(_transferId);

            return newSpanId;
        }


        public void MappingStreamHeader(Span<byte> bufferHeader, int bufferCount)
        {
            bufferHeader = bufferHeader.Slice(_headerSize.StartIndex, bufferCount);

            Span<byte> objectiveSpan = bufferHeader.Slice(_headerSize.ObjectiveIndex, _headerSize.Objective);
            Span<byte> mimeTypeSpan = bufferHeader.Slice(_headerSize.MimeTypeIndex, _headerSize.MimeType);
            Span<byte> payloadSpan = bufferHeader.Slice(_headerSize.PayloadIndex, bufferCount - _headerSize.PayloadIndex);
            Span<byte> flagsSpan = bufferHeader.Slice(_headerSize.FlagsIndex, _headerSize.Flags);

            payloadSpan.CopyTo(_payload);
            this.Length = payloadSpan.Length;
            this.Offset += (ulong)payloadSpan.Length;
            this.Flags = (Flags)BinaryPrimitives.ReadInt16LittleEndian(flagsSpan);
            this.Objective = (Objective)BinaryPrimitives.ReadInt32LittleEndian(objectiveSpan);
            this.MimeType = (MimeType)BinaryPrimitives.ReadInt16LittleEndian(mimeTypeSpan);
        }
    }
}
