using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeddingPhotoServer.Infrastructure.Interface;

namespace UnitTest.ServiceTests
{
    [TestClass]
    public class PathProviderTests
    {
        private Mock<IConfiguration> _mockConfiguration;
        private PathProvider _pathProvider;

        [TestInitialize]
        public void Setup()
        {
            _mockConfiguration = new Mock<IConfiguration>();
        }

        [TestMethod]
        public void GetPath_ShouldReturnFullyQualifiedPath_WhenRelativePathIsGiven()
        {
            // Arrange
            var relativePath = "StoredFiles";
            var expectedPath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);

            var mockConfigurationSection = new Mock<IConfigurationSection>();
            mockConfigurationSection.Setup(x => x.Value).Returns(relativePath);

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x.GetSection("StoredFilesPath")).Returns(mockConfigurationSection.Object);

            _pathProvider = new PathProvider(mockConfiguration.Object);

            // Act
            var result = _pathProvider.GetPath();

            // Assert
            Assert.AreEqual(expectedPath, result);
        }

        [TestMethod]
        public void GetPath_ShouldReturnFullyQualifiedPath_WhenAlreadyAbsolute()
        {
            // Arrange
            var absolutePath = @"C:\StoredFiles";

            var mockConfigurationSection = new Mock<IConfigurationSection>();
            mockConfigurationSection.Setup(x => x.Value).Returns(absolutePath);

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x.GetSection("StoredFilesPath")).Returns(mockConfigurationSection.Object);

            _pathProvider = new PathProvider(mockConfiguration.Object);

            // Act
            var result = _pathProvider.GetPath();

            // Assert
            Assert.AreEqual(absolutePath, result);
        }

        [TestMethod]
        public void GetPath_ShouldCreateDirectoryIfNotExists()
        {
            // Arrange
            var relativePath = "NewStoredFiles";
            var expectedPath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);

            var mockConfigurationSection = new Mock<IConfigurationSection>();
            mockConfigurationSection.Setup(x => x.Value).Returns(relativePath);

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x.GetSection("StoredFilesPath")).Returns(mockConfigurationSection.Object);

            _pathProvider = new PathProvider(mockConfiguration.Object);

            if (Directory.Exists(expectedPath))
            {
                Directory.Delete(expectedPath, true);
            }

            // Act
            var result = _pathProvider.GetPath();

            // Assert
            Assert.AreEqual(expectedPath, result);
            Assert.IsTrue(Directory.Exists(expectedPath), "Directory should have been created.");
        }

        [TestMethod]
        public void GetDirectory_ShouldReturnDirectoryPath()
        {
            // Arrange
            var relativePath = "StoredFiles";
            var expectedPath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);

            var mockConfigurationSection = new Mock<IConfigurationSection>();
            mockConfigurationSection.Setup(x => x.Value).Returns(relativePath);

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x.GetSection("StoredFilesPath")).Returns(mockConfigurationSection.Object);

            _pathProvider = new PathProvider(mockConfiguration.Object);

            // Act
            var result = _pathProvider.GetDirectory();

            // Assert
            Assert.AreEqual(expectedPath, result);
        }

        [TestMethod]
        public void GetDirectory_ShouldCreateDirectoryIfNotExists()
        {
            // Arrange
            var relativePath = "NewStoredFiles";
            var expectedPath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);

            var mockConfigurationSection = new Mock<IConfigurationSection>();
            mockConfigurationSection.Setup(x => x.Value).Returns(relativePath);

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(x => x.GetSection("StoredFilesPath")).Returns(mockConfigurationSection.Object);

            _pathProvider = new PathProvider(mockConfiguration.Object);

            if (Directory.Exists(expectedPath))
            {
                Directory.Delete(expectedPath, true);
            }

            // Act
            var result = _pathProvider.GetDirectory();

            // Assert
            Assert.AreEqual(expectedPath, result);
            Assert.IsTrue(Directory.Exists(expectedPath), "Directory should have been created.");
        }
    }
}
