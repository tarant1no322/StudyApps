namespace FileManagerAPI.Models
{
    public class FileValue
    {
        public MemoryStream Value { get; set; } = new MemoryStream();
        public string Name { get; set; } = string.Empty;
    }
}
