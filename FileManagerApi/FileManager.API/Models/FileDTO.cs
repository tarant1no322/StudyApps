namespace FileManagerAPI.Models
{
    public class FileDTO
    {
        public MemoryStream Value { get; set; } = new MemoryStream();
        public string Name { get; set; } = string.Empty;
    }
}
