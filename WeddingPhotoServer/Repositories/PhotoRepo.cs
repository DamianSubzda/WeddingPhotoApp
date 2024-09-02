using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingPhotoServer.Infrastructure.Interface;
using WeddingPhotoServer.Model;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using WeddingPhotoServer.Infrastrucure.Data;
using WeddingPhotoServer.DTO;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace WeddingPhotoServer.Repositories
{
    public interface IPhotoRepo
    {
        Task<PhotoDTO> UploadPhoto(IFormFile file, bool addToGallery, int rotation, HttpRequest request);
        FileStream GetPhoto(string fileName);
        Task<List<PhotoDTO>> GetPhotosDTO(int pageNumber, int pageSize, HttpRequest request);
    }
    public class PhotoRepo : IPhotoRepo
    {
        private readonly IPathProvider _pathProvider;
        private readonly ApplicationDbContext _context;
        public PhotoRepo(IPathProvider pathProvider, ApplicationDbContext context)
        {
            _pathProvider = pathProvider;
            _context = context;
        }

        public FileStream GetPhoto(string fileName)
        {
            var path = Path.Combine(_pathProvider.GetPath(), fileName);
            if (!File.Exists(path))
                throw new FileNotFoundException(fileName);

            var imageFileStream = File.OpenRead(path);
            return imageFileStream;
        }

        public async Task<List<PhotoDTO>> GetPhotosDTO(int pageNumber, int pageSize, HttpRequest request)
        {
            var photos = await _context.Photos
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(photo => new PhotoDTO
                {
                    Id = photo.Id,
                    FileName = photo.FileName,
                    FullFilePath = $"{request.Scheme}://{request.Host}/api/photos/{photo.Guid}_{photo.FileName}",
                    CreatedAt = photo.CreatedAt,
                    Description = photo.Description,
                    AddToGallery = photo.AddToGallery
                })
                .ToListAsync();

            return photos;
        }


        public async Task<PhotoDTO> UploadPhoto(IFormFile file, bool addToGallery, int rotation, HttpRequest request)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentNullException("No file uploaded");
            }

            if (!file.ContentType.StartsWith("image/"))
            {
                throw new ArgumentException("Uploaded file is not an image");
            }

            string guid = Guid.NewGuid().ToString();
            string fileName = guid + "_" + file.FileName;
            var path = Path.Combine(Directory.GetCurrentDirectory(), _pathProvider.GetDirectory());
            var fullPath = Path.Combine(path, fileName);

            using (var image = Image.Load(file.OpenReadStream()))
            {
                if (rotation % 360 != 0)
                {
                    image.Mutate(x => x.Rotate(rotation));
                }

                await image.SaveAsync(fullPath);
            }

            var photo = new Photo
            {
                FileName = file.FileName,
                Guid = guid,
                FileDirectory = _pathProvider.GetDirectory(),
                AddToGallery = addToGallery
            };

            _context.Photos.Add(photo);
            await _context.SaveChangesAsync();

            return new PhotoDTO
            {
                Id = photo.Id,
                FileName = photo.FileName,
                FullFilePath = $"{request.Scheme}://{request.Host}/api/photos/{photo.Guid}_{photo.FileName}",
                CreatedAt = photo.CreatedAt,
                Description = photo.Description,
                AddToGallery = photo.AddToGallery
            };

        }
    }
}
