using FileManager.Domain.Models;

namespace FileManager.Service.Interfaces
{
    interface IFileService
    {
        Task<string> GenerateLink(int id);
        Task<List<FileModel>> GetAllInfo();
        Task<bool> UploadFile(MemoryStream ms);
        Task<bool> DeleteFile(int id);
    }
}
