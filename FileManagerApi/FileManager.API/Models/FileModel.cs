using System.ComponentModel.DataAnnotations;

namespace FileManagerApi.Models
{
    public class FileModel
    {
        [Key]
        public Guid Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public byte[] Data { get; set; }
        public DateTime TimeUpload { get; set; }
        public string? ShortLink { get; set; }

    }
}
