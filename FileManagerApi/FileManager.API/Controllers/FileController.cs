using System.IO;
using System.Web;
using FileManagerApi.Service;
using FileManagerAPI.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace FileManagerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly ILogger<FileController> _logger;

        public FileController(IFileService fileService, ILogger<FileController> logger)
        {
            _fileService = fileService;
            _logger = logger;
        }
        [HttpGet]
        [Route("~/{link?}")]
        public async Task<IActionResult> GetFileByLink(string? link)
        {
            if (string.IsNullOrEmpty(link))
                return BadRequest("Incorrect link");
            var file = await _fileService.GetFileByLink(link);
            if (file == null)
                return NotFound("FileNotFound");
            return File(file.Value, "application/octet-stream", file.Name);
        }
        //get download link
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLink(Guid id)
        {
            string result = await _fileService.GenerateLink(id);
            return new JsonResult(result);
        }
        //get info about all files
        [HttpGet]
        public async Task<List<FileDTO>> GetAllInfo()
        {
            return await _fileService.GetAllInfo();
        }
        //delete file 
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]Guid id)
        {
            if (await _fileService.DeleteFile(id))
                return Ok("Delete successfully!");
            return BadRequest("File not found or delete error!");
        }
        
        //upload file
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFileCollection formFile) 
        {
            if(formFile == null || formFile.Count == 0)
                return BadRequest("Controller has not received any files to upload!");
            if (await _fileService.UploadFile(formFile))
                return Ok("Upload successfully completed!");
            return BadRequest("Error"); 
        }
    }
}