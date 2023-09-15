using FileManagerApi.Models;
using FileManagerAPI.Models;

namespace FileManagerApi.Service
{
    public interface IFileService
    {
        Task<FileValue> GetFileByLink(string link);
        Task<string> GenerateLink(Guid id);
        Task<List<FileDTO>> GetAllInfo();
        Task<bool> UploadFile(IEnumerable<IFormFile> files);
        Task<bool> DeleteFile(Guid id);
    }
}
