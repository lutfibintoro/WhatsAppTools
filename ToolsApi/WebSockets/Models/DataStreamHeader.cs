namespace ToolsApi.WebSockets.Models
{
    public class DataStreamHeader
    {
        private readonly byte[] _magic = new byte[4] { 0xE0, 0x8B, 0x33, 0xF7 };
        private readonly byte[] _transferId = new byte[16];
        private readonly byte[] _payload = new byte[1024 * 4];


        public Span<byte> Magic { get => _magic; }
        public Span<byte> TransferId { get => _transferId; }
        public Flags Flags { get; set; }
        public Objective Objective { get; set; }
        public MimeType MimeType { get; set; }
        public ulong Offset { get; set; }
        public uint Length { get; set; }
        public Span<byte> Payload { get => _payload; }
    }
}
