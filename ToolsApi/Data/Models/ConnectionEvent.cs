namespace ToolsApi.Data.Models
{
    public class ConnectionEvent
    {
        public int Id { get; set; }
        public byte[] TransferId { get; set; } = null!;
        public ulong Offset { get; set; }
        public uint Objective { get; set; }
        public ushort MimeType { get; set; }
    }
}
