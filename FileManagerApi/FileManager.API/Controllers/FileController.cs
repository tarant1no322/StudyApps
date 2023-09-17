using FileManagerApi.Service;
using FileManagerAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileManagerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// Downloading a file from the server by the client
        /// </summary>
        /// <param name="link">Short link to download the file</param>
        /// <returns>File as a data stream</returns>
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

        /// <summary>
        /// Get a short link to download a file 
        /// </summary>
        /// <param name="id">File guid</param>
        /// <returns>Short reference to a file as a string</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLink(Guid id)
        {
            string? result = await _fileService.GenerateLink(id);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }

        /// <summary>
        /// Get list of all files on the server
        /// </summary>
        /// <returns>List of files as FileDTO</returns>
        [HttpGet]
        public async Task<ActionResult<List<FileInfoDTO>>> GetAllInfo()
        {
            return await _fileService.GetAllInfo();
        }

        /// <summary>
        /// Getting the guid file and passing it to services for deletion
        /// </summary>
        /// <param name="id"> File guid for deletion</param>
        /// <returns>Client receives a response in the form of an http response with the string</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Guid id)
        {
            if (await _fileService.DeleteFile(id))
                return Ok("Delete successfully!");
            return BadRequest("File not found or delete error!");
        }

        /// <summary>
        /// Receiving the file and transferring it to the services for uploading
        /// </summary>
        /// <param name="formFiles">Ccollection of one or more files to upload</param>
        /// <returns>Client receives a response in the form of an http response with the string</returns>
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFileCollection formFiles)
        {
            if (formFiles == null || formFiles.Count == 0)
                return BadRequest("Controller has not received any files to upload!");
            if (await _fileService.UploadFile(formFiles))
                return Ok("Upload successfully completed!");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}