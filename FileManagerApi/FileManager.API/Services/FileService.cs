using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManagerApi.Models;
using FileManagerAPI.DAL;
using FileManagerAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FileManagerApi.Service
{
    class FileService : IFileService
    {
        private readonly ILogger<FileService> _logger;
        private readonly ApplicationDbContext _db;
        public FileService(ApplicationDbContext db, ILogger<FileService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<bool> DeleteFile(Guid id)
        {
            try
            {
                var file =  await _db.Files.FirstOrDefaultAsync(x => x.Id == id);
                if (file == null)
                    return false;   
                
                _db.Files.Remove(file);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }


        public async Task<string> GenerateLink(Guid id)
        {
            try
            {
                var file = await _db.Files.FirstOrDefaultAsync(x => x.Id == id);
                if (file == null)
                    return "File not found!";
                
                file.ShortLink = RandomString(7);

                await _db.SaveChangesAsync();
                return file.ShortLink;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return "Error";
            }
        }

        public async Task<List<FileDTO>> GetAllInfo()
        {
            return await _db.Files.Select(x => new FileDTO()
                {
                    Id = x.Id,
                    FileName = x.FileName,
                    TimeUpload = x.TimeUpload,
                    ShortLink = x.ShortLink
                })
                .ToListAsync();
        }

        public async Task<FileValue?> GetFileByLink(string link)
        {
            try
            {
                var file = await _db.Files.FirstOrDefaultAsync(x => x.ShortLink == link);
                if (file == null)
                    return null;
                file.ShortLink = null;

                await _db.SaveChangesAsync();
                return new FileValue()
                {
                    Value = new MemoryStream(file.Data),
                    Name = file.FileName
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> UploadFile(IEnumerable<IFormFile> files)
        {
            try
            {
                foreach (var file in files)
                {
                    using MemoryStream ms = new MemoryStream();
                    await file.CopyToAsync(ms);
                    FileModel fileModel = new FileModel()
                    {
                        Id = Guid.NewGuid(),
                        FileName = file.FileName,
                        Data = ms.ToArray(),
                        TimeUpload = DateTime.UtcNow,
                        ShortLink = null
                    };
                    await _db.Files.AddAsync(fileModel);
                }
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
        private string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
