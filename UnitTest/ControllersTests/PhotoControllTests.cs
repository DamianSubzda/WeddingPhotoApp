using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WeddingPhotoServer.Controllers;
using WeddingPhotoServer.DTO;
using WeddingPhotoServer.Repositories;

namespace UnitTest.ControllersTests
{
    [TestClass]
    public class PhotoControllerTests
    {
        private Mock<IPhotoRepo> _mockPhotoRepo;
        private PhotoController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockPhotoRepo = new Mock<IPhotoRepo>();
            _controller = new PhotoController(_mockPhotoRepo.Object);
        }

        [TestMethod]
        public async Task Upload_ShouldReturnOk_WhenUploadSucceeds()
        {
            // Arrange
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.FileName).Returns("test.jpg");

            var photoDTO = new PhotoDTO
            {
                Id = 1,
                FileName = "test.jpg",
                FullFilePath = "/photos/test.jpg",
                CreatedAt = DateTime.UtcNow,
                AddToGallery = true
            };

            _mockPhotoRepo
                .Setup(repo => repo.UploadPhoto(It.IsAny<IFormFile>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<HttpRequest>()))
                .ReturnsAsync(photoDTO);

            // Act
            var result = await _controller.Upload(fileMock.Object, true, 90, "Sample description") as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(photoDTO, result.Value);
        }


        [TestMethod]
        public async Task Upload_ShouldReturnBadRequest_WhenArgumentExceptionIsThrown()
        {
            // Arrange
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.FileName).Returns("test.jpg");
            _mockPhotoRepo
                .Setup(repo => repo.UploadPhoto(It.IsAny<IFormFile>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<HttpRequest>()))
                .ThrowsAsync(new ArgumentException("Invalid file"));

            // Act
            var result = await _controller.Upload(fileMock.Object, true, 90, "Sample description") as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("Invalid file", result.Value);
        }

        [TestMethod]
        public async Task Upload_ShouldReturnInternalServerError_WhenUnexpectedExceptionIsThrown()
        {
            // Arrange
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.FileName).Returns("test.jpg");
            _mockPhotoRepo
                .Setup(repo => repo.UploadPhoto(It.IsAny<IFormFile>(), It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<HttpRequest>()))
                .ThrowsAsync(new Exception());

            // Act
            var result = await _controller.Upload(fileMock.Object, true, 90, "Sample description") as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
            Assert.AreEqual("Internal server error", result.Value);
        }

        [TestMethod]
        public void Get_ShouldReturnFile_WhenFileExists()
        {
            // Arrange
            var fileName = "test.jpg";
            var fileContent = new byte[] { 0x20, 0x20 };

            var tempFilePath = Path.GetTempFileName();
            File.WriteAllBytes(tempFilePath, fileContent);

            var fileStream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read);

            _mockPhotoRepo.Setup(repo => repo.GetPhoto(fileName)).Returns(fileStream);

            // Act
            var result = _controller.Get(fileName) as FileStreamResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("image/jpeg", result.ContentType);

            using (var resultStream = new MemoryStream())
            {
                result.FileStream.CopyTo(resultStream);
                Assert.AreEqual(fileContent.Length, resultStream.Length);
                Assert.IsTrue(fileContent.SequenceEqual(resultStream.ToArray()));
            }

            // Cleanup
            fileStream.Dispose();
            File.Delete(tempFilePath);
        }


        [TestMethod]
        public void Get_ShouldReturnNotFound_WhenFileDoesNotExist()
        {
            // Arrange
            var fileName = "nonexistent.jpg";
            _mockPhotoRepo.Setup(repo => repo.GetPhoto(fileName)).Throws(new FileNotFoundException());

            // Act
            var result = _controller.Get(fileName) as NotFoundObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
            Assert.AreEqual($"File: {fileName} not found!", result.Value);
        }

        [TestMethod]
        public void Get_ShouldReturnInternalServerError_WhenUnexpectedExceptionIsThrown()
        {
            // Arrange
            var fileName = "test.jpg";
            _mockPhotoRepo.Setup(repo => repo.GetPhoto(fileName)).Throws(new Exception());

            // Act
            var result = _controller.Get(fileName) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
            Assert.AreEqual("Internal server error", result.Value);
        }

        [TestMethod]
        public async Task GetPhotosDTO_ShouldReturnOk_WithPhotosDTO()
        {
            // Arrange
            var photoDTOs = new List<PhotoDTO> { new PhotoDTO { Id = 1, FileName = "test.jpg", AddToGallery=true, CreatedAt=DateTime.Now, FullFilePath="" } };
            _mockPhotoRepo.Setup(repo => repo.GetPhotosDTO(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<HttpRequest>())).ReturnsAsync(photoDTOs);

            // Act
            var actionResult = await _controller.GetPhotosDTO();
            var okResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(photoDTOs, okResult.Value);
        }

        [TestMethod]
        public async Task GetPhotosDTO_ShouldReturnInternalServerError_WhenExceptionIsThrown()
        {
            // Arrange
            _mockPhotoRepo.Setup(repo => repo.GetPhotosDTO(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<HttpRequest>())).ThrowsAsync(new Exception());

            // Act
            var actionResult = await _controller.GetPhotosDTO();
            var result = actionResult.Result as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
            Assert.AreEqual("Internal server error", result.Value);
        }

    }
}
