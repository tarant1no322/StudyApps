using FileManagerAPI.Models;

namespace FileManagerApi.Service
{
    public interface IFileService
    {
        Task<FileDTO?> GetFileByLink(string link);
        Task<string?> GenerateLink(Guid id);
        Task<List<FileInfoDTO>> GetAllInfo();
        Task<bool> UploadFile(IEnumerable<IFormFile> files);
        Task<bool> DeleteFile(Guid id);
    }
}
