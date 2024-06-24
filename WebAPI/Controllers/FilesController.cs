using BuisnessLogicLayer.Exceptions;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("mystorage/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;
        private UserManager<AppUser> _userManager;
        public FilesController(IFileService fileService, UserManager<AppUser> userManager)
        {
            _fileService = fileService;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<FileModel>>> GetFileModels()
        {

            IEnumerable<FileModel> fileModels;

            try
            {
                fileModels = await _fileService.GetAllAsync();
            }
            catch(BLLException e) {
                return BadRequest();
            }

            return Ok(fileModels);
        }

        [HttpGet("public")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<FileModel>>> GetPublicFileModels()
        {
            IEnumerable<FileModel> fileModels;

            try
            {
                fileModels = await _fileService.GetAllPublicAsync();
            }
            catch (BLLException e)
            {
                return BadRequest();
            }

            return Ok(fileModels);
        }


        [HttpGet("my-files")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<FileModel>>> GetUsersFileModels()
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;

            IEnumerable<FileModel> fileModels = _fileService.GetFilesByUserId(userId);

            if (fileModels == null)
            {
                return NotFound();
            }

            return Ok(fileModels);
        }

        [HttpPost("upload/{fileName}/{accessLevel}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddFile(string fileName, int accessLevel,[FromQuery] string description, IFormFile file)
        {
            try
            {
                AddFileModel addFile = new AddFileModel()
                {
                    Name = fileName,
                    AccessLevel = accessLevel,
                    Description = description,
                    File = file,
                    CreatorId = User.Claims.First(c => c.Type == "UserId").Value
                };


                await _fileService.AddAsync(addFile);

            }
            catch(BLLException be)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpGet("filter")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<FileModel>>> GetFileModelsByFilter([FromQuery] FileFilterModel fileFilter)
        {
            IEnumerable<FileModel> fileModels = await _fileService.GetPublicByFileterAsync(fileFilter);

            if (fileModels == null)
            {
                return NotFound();
            }

            return Ok(fileModels);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<FileModel>> GetFileModelById(Guid id)
        {
            FileModel fileModel = await _fileService.GetByIdAsync(id);

            if (fileModel == null)
            {
                return NotFound();
            }

            return Ok(fileModel);
        }

        [HttpGet("link/{userName}/{fileName}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<FileModel>> GetFileModelByUserAndName(string userName, string fileName)
        {

            if(userName == null || fileName == null)
            {
                return BadRequest();
            }

            FileModel fileModel = await _fileService.GetFileModelByUserAndNameAsync(userName, fileName);

            if (fileModel == null)
            {
                return NotFound();
            }

            return Ok(fileModel);
        }

        [HttpGet("export/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<FileStreamResult> ExportFile(Guid id)
        {
            FileModel fileModel = await _fileService.GetByIdAsync(id);

            FileStream file = await _fileService.GetFileAsync(id);

            return new FileStreamResult(file, fileModel.ContentType) {
                FileDownloadName = fileModel.Name + fileModel.FileTypeExtension
            };
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateFileInformation([FromBody] FileModel file)
        {
            if(file == null)
            {
                return BadRequest();
            }

            await _fileService.UpdateAsync(file);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteFile(Guid id)
        {
            await _fileService.DeleteAsync(id);
            return Ok();
        }


    }
}
