namespace FileManagerAPI.Models
{
    public class FileDTO
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public DateTime TimeUpload { get; set; }
        public string? ShortLink { get; set; }
    }
}
