using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using WeddingPhotoServer.DTO;
using WeddingPhotoServer.Infrastructure.Interface;
using WeddingPhotoServer.Infrastrucure.Data;
using WeddingPhotoServer.Model;

namespace WeddingPhotoServer.Controllers
{
    [ApiController]
    [Route("api/photos")]
    public class PhotoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPathProvider _pathProvider;

        public PhotoController(ApplicationDbContext context, IPathProvider pathProvider)
        {
            _context = context;
            _pathProvider = pathProvider;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");
            string guid = Guid.NewGuid().ToString();
            string fileName = guid + "_" + file.FileName;
            var path = Path.Combine(Directory.GetCurrentDirectory(), _pathProvider.GetDirectory());
            var fullPath = Path.Combine(path, fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var photo = new Photo
            {
                FileName = file.FileName,
                Guid = guid,
                FileDirectory = _pathProvider.GetDirectory(),
                AddToGallery = true //TODO 
            };
            _context.Photos.Add(photo);
            await _context.SaveChangesAsync();

            return Ok(new { photo.Id, fileName });
        }

        [HttpGet("{fileName}")]
        public IActionResult Get(string fileName)
        {
            var path = Path.Combine(_pathProvider.GetPath(), fileName);
            if (!System.IO.File.Exists(path))
                return NotFound();

            var imageFileStream = System.IO.File.OpenRead(path);
            return File(imageFileStream, "image/jpeg");
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<PhotoDTO>>> GetPhotos(int pageNumber = 1, int pageSize = 10)
        {
            var photos = await _context.Photos
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var photoDtos = photos.Select(photo => new PhotoDTO
            {
                Id = photo.Id,
                FileName = photo.FileName,
                FullFilePath = $"{this.Request.Scheme}://{this.Request.Host}/api/photos/{photo.Guid}_{photo.FileName}",
                CreatedAt = photo.CreatedAt,
                AddToGallery = photo.AddToGallery
            }).ToList();

            return Ok(photoDtos);
        }

    }
}
