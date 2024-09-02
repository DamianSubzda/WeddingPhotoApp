using Microsoft.AspNetCore.Mvc;
using WeddingPhotoServer.DTO;
using WeddingPhotoServer.Repositories;

namespace WeddingPhotoServer.Controllers
{
    [ApiController]
    [Route("api/photos")]
    public class PhotoController : Controller
    {
        private readonly IPhotoRepo _photoRepo;
        public PhotoController(IPhotoRepo photoRepo)
        {
            _photoRepo = photoRepo;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file, [FromForm] bool addToGallery, [FromForm] int rotation)
        {
            try
            {
                var result = await _photoRepo.UploadPhoto(file, addToGallery, rotation, this.Request);
                return Ok(result);
            }
            catch (Exception ex) when (ex is ArgumentException ||ex is ArgumentNullException) 
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }

            
        }

        [HttpGet("{fileName}")]
        public IActionResult Get(string fileName)
        {
            try
            {
                var result = _photoRepo.GetPhoto(fileName);
                return File(result, "image/jpeg");
            }
            catch (FileNotFoundException)
            {
                return NotFound($"File: {fileName} not found!");
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<PhotoDTO>>> GetPhotosDTO(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var result = await _photoRepo.GetPhotosDTO(pageNumber, pageSize, this.Request);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
