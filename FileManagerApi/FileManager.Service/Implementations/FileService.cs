using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManager.Domain.Models;
using FileManager.Service.Interfaces;

namespace FileManager.Service.Implementations
{
    internal class FileService : IFileService
    {
        public Task<bool> DeleteFile(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GenerateLink(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<FileModel>> GetAllInfo()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UploadFile(MemoryStream ms)
        {
            throw new NotImplementedException();
        }
    }
}
