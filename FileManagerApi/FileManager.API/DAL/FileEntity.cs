using System.ComponentModel.DataAnnotations;

namespace FileManagerAPI.DAL
{
    public class FileEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public byte[] Data { get; set; }
        public DateTime TimeUpload { get; set; }
        public string? ShortLink { get; set; }

    }
}
