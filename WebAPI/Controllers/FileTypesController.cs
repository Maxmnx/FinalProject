using BuisnessLogicLayer.Exceptions;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("mystorage/[controller]")]
    [ApiController]
    public class FileTypesController : ControllerBase
    {

        private readonly IFileTypeService _fileTypeService;
        private UserManager<AppUser> _userManager;
        public FileTypesController(IFileTypeService fileTypeService, UserManager<AppUser> userManager)
        {
            _fileTypeService = fileTypeService;
            _userManager = userManager;
        }

        // GET: api/<FileTypesController>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<FileTypeModel>>> GetAll()
        {

            if (!User.IsInRole("Admin"))
            {
                return BadRequest();
            }

            var fileTypes = await _fileTypeService.GetAllAsync();

            return Ok(fileTypes);
        }

        // GET api/<FileTypesController>/5
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<FileTypeModel>> Get(int id)
        {

            if (!User.IsInRole("Admin"))
            {
                return BadRequest();
            }

            return Ok();
        }

        // POST api/<FileTypesController>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> AddAsync([FromBody] FileTypeModel value)
        {

            if (!User.IsInRole("Admin"))
            {
                return BadRequest();
            }

            try
            {
                await _fileTypeService.AddAsync(value);
            }
            catch (BLLException e)
            {

                return BadRequest(e.Message);
            }

            return Ok(StatusCodes.Status201Created);
        }

        // DELETE api/<FileTypesController>/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(Guid id)
        {

            if (!User.IsInRole("Admin"))
            {
                return BadRequest();
            }
            try
            {
                await _fileTypeService.DeleteAsync(id);
            }
            catch(BLLException e)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
