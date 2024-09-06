using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Text;
using WeddingPhotoServer.Infrastructure.Interface;
using WeddingPhotoServer.Infrastrucure.Data;
using WeddingPhotoServer.Model;
using WeddingPhotoServer.Repositories;

namespace UnitTest.RepositoryTests
{
    [TestClass]
    public class PhotoRepoTests
    {
        private Mock<IPathProvider> _mockPathProvider;
        private ApplicationDbContext _dbContext;
        private PhotoRepo _photoRepo;

        [TestInitialize]
        public void Setup()
        {
            // Setup in-memory database
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "WeddingPhotoDb")
                .Options;
            _dbContext = new ApplicationDbContext(options);

            // Initialize Mock for IPathProvider
            _mockPathProvider = new Mock<IPathProvider>();
            _photoRepo = new PhotoRepo(_mockPathProvider.Object, _dbContext);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task UploadPhoto_ShouldThrowException_WhenFileIsNull()
        {
            // Arrange
            var requestMock = new Mock<HttpRequest>();

            // Act
            await _photoRepo.UploadPhoto(null, true, 0, null, requestMock.Object);

            // Assert handled by ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task UploadPhoto_ShouldThrowException_WhenFileIsNotImage()
        {
            // Arrange
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.FileName).Returns("test.txt");
            fileMock.Setup(f => f.ContentType).Returns("text/plain");

            // Ensure the file has content to avoid ArgumentNullException
            var fileContent = new MemoryStream(Encoding.UTF8.GetBytes("This is a text file."));
            fileMock.Setup(f => f.OpenReadStream()).Returns(fileContent);
            fileMock.Setup(f => f.Length).Returns(fileContent.Length);

            var requestMock = new Mock<HttpRequest>();

            // Act
            await _photoRepo.UploadPhoto(fileMock.Object, true, 0, null, requestMock.Object);

            // Assert handled by ExpectedException
        }


        [TestMethod]
        public void GetPhoto_ShouldReturnFileStream_WhenFileExists()
        {
            // Arrange
            var fileName = "test.jpg";
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "test_directory");
            var filePath = Path.Combine(directoryPath, fileName);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            File.WriteAllBytes(filePath, new byte[] { 0x20, 0x20 });

            _mockPathProvider.Setup(p => p.GetPath()).Returns(directoryPath);

            // Act
            var result = _photoRepo.GetPhoto(fileName);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileStream));

            // Clean up
            result.Dispose();
            File.Delete(filePath);
            Directory.Delete(directoryPath);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void GetPhoto_ShouldThrowFileNotFoundException_WhenFileDoesNotExist()
        {
            // Arrange
            var fileName = "nonexistent.jpg";
            _mockPathProvider.Setup(p => p.GetPath()).Returns("test_directory");

            // Act
            _photoRepo.GetPhoto(fileName);

            // Assert handled by ExpectedException
        }

        [TestMethod]
        public async Task GetPhotosDTO_ShouldReturnPhotos_WithPagination()
        {
            // Arrange
            _dbContext.Photos.Add(new Photo
            {
                FileName = "photo1.jpg",
                AddToGallery = true,
                Description = "Test photo",
                CreatedAt = DateTime.UtcNow,
                FileDirectory = ""
            }); ;

            _dbContext.Photos.Add(new Photo
            {
                FileName = "photo2.jpg",
                AddToGallery = false,
                Description = "Another photo",
                CreatedAt = DateTime.UtcNow.AddMinutes(-10),
                FileDirectory = ""
            });

            await _dbContext.SaveChangesAsync();

            var requestMock = new Mock<HttpRequest>();
            requestMock.Setup(r => r.Scheme).Returns("https");
            requestMock.Setup(r => r.Host).Returns(new HostString("localhost"));

            // Act
            var result = await _photoRepo.GetPhotosDTO(1, 2, requestMock.Object);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("photo1.jpg", result[0].FileName);
            Assert.AreEqual("photo2.jpg", result[1].FileName);
        }
    }
}
