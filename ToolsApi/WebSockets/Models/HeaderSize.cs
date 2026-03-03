namespace ToolsApi.WebSockets.Models
{
    // | 4B magic | 16B transferId | 2B flags | 4B objective | 2B mimeType | (1024 * 4)B payload |
    public class HeaderSize
    {
        public int Size { get => Magic + TransferId + Flags + Objective + MimeType + Payload; }

        public byte Magic { get => 4; }
        public byte TransferId { get => 16; }
        public byte Flags { get => 2; }
        public byte Objective { get => 4; }
        public byte MimeType { get => 2; }
        public int Payload { get => 1024 * 4; }

        public byte StartIndex { get => 0; }
        public byte MagicIndex { get => StartIndex; }
        public byte TransferIdIndex { get => Magic; }
        public byte FlagsIndex { get => (byte)(Magic + TransferId); }
        public byte ObjectiveIndex { get => (byte)(Magic + TransferId + Flags); }
        public byte MimeTypeIndex { get => (byte)(Magic + TransferId + Flags + Objective); }
        public byte PayloadIndex { get => (byte)(Magic + TransferId + Flags + Objective + MimeType); }

        public byte MagicCountPosition { get => Magic; }
        public byte TransferIdCountPosition { get => (byte)(Magic + TransferId); }
        public byte FlagsCountPosition { get => (byte)(Magic + TransferId + Flags); }
        public byte ObjectiveCountPosition { get => (byte)(Magic + TransferId + Flags + Objective); }
        public byte MimeTypeCountPosition { get => (byte)(Magic + TransferId + Flags + Objective + MimeType); }
    }
}
