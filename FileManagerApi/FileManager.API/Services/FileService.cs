using FileManagerAPI.DAL;
using FileManagerAPI.Models;
using Microsoft.EntityFrameworkCore;

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

        /// <summary>
        /// Deleting a file from the database
        /// </summary>
        /// <param name="id">File Guid</param>
        public async Task<bool> DeleteFile(Guid id)
        {
            try
            {
                var file = await _db.Files.FirstOrDefaultAsync(x => x.Id == id);
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

        /// <summary>
        /// Generating a link, assigning it to the selected file, and returning it to the client
        /// </summary>
        /// <param name="id">File Guid</param>
        /// <returns>String or null</returns>
        public async Task<string?> GenerateLink(Guid id)
        {
            try
            {
                var file = await _db.Files.FirstOrDefaultAsync(x => x.Id == id);
                if (file == null)
                    return null;

                file.ShortLink = RandomString(7);

                await _db.SaveChangesAsync();
                return file.ShortLink;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Function returns the list of all files on the server
        /// </summary>
        public async Task<List<FileInfoDTO>> GetAllInfo()
        {
            return await _db.Files.Select(x => new FileInfoDTO()
            {
                Id = x.Id,
                FileName = x.FileName,
                TimeUpload = x.TimeUpload,
                ShortLink = x.ShortLink
            })
                .ToListAsync();
        }

        /// <summary>
        /// Returning the file from server to the client by a short link
        /// </summary>
        /// <param name="link">Short link</param>
        public async Task<FileDTO?> GetFileByLink(string link)
        {
            try
            {
                var file = await _db.Files.FirstOrDefaultAsync(x => x.ShortLink == link);
                if (file == null)
                    return null;
                file.ShortLink = null;

                await _db.SaveChangesAsync();
                return new FileDTO()
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

        /// <summary>
        /// Saving all received files to the database
        /// </summary>
        /// <param name="files">Collection of one or more files to upload</param>
        public async Task<bool> UploadFile(IEnumerable<IFormFile> files)
        {
            try
            {
                foreach (var file in files)
                {
                    using MemoryStream ms = new MemoryStream();
                    await file.CopyToAsync(ms);
                    FileEntity fileModel = new FileEntity()
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

        /// <summary>
        /// Generating a random string of specified length
        /// </summary>
        /// <param name="length">Length of generated string</param>
        /// <returns>Generated string</returns>
        private string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
