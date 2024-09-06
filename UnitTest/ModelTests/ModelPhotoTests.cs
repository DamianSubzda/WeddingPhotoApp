using WeddingPhotoServer.Model;

namespace UnitTest.ModelTests
{
    [TestClass]
    public class ModelPhotoTests
    {
        [TestMethod]
        public void Photo_DefaultCreatedAt_ShouldBeCurrentUtcTime()
        {
            // Arrange & Act
            var photo = new Photo
            {
                Id = 1,
                FileName = "test.jpg",
                FileDirectory = "/photos",
                AddToGallery = true
            };

            // Assert
            Assert.IsTrue((DateTime.UtcNow - photo.CreatedAt).TotalSeconds < 1, "CreatedAt should be close to the current UTC time.");
        }

        [TestMethod]
        public void Photo_Guid_ShouldBeNullWhenNotSet()
        {
            // Arrange & Act
            var photo = new Photo
            {
                Id = 1,
                FileName = "test.jpg",
                FileDirectory = "/photos",
                AddToGallery = true
            };

            // Assert
            Assert.IsNull(photo.Guid, "Guid should be null by default.");
        }

        [TestMethod]
        public void Photo_Guid_ShouldNotBeNullWhenSet()
        {
            // Arrange
            var guidValue = Guid.NewGuid().ToString();

            // Act
            var photo = new Photo
            {
                Id = 1,
                FileName = "test.jpg",
                FileDirectory = "/photos",
                AddToGallery = true,
                Guid = guidValue
            };

            // Assert
            Assert.AreEqual(guidValue, photo.Guid, "Guid should match the provided value.");
        }

        [TestMethod]
        public void Photo_Description_ShouldBeNullWhenNotSet()
        {
            // Arrange & Act
            var photo = new Photo
            {
                Id = 1,
                FileName = "test.jpg",
                FileDirectory = "/photos",
                AddToGallery = true
            };

            // Assert
            Assert.IsNull(photo.Description, "Description should be null by default.");
        }

        [TestMethod]
        public void Photo_Description_ShouldNotBeNullWhenSet()
        {
            // Arrange
            var description = "A test photo description.";

            // Act
            var photo = new Photo
            {
                Id = 1,
                FileName = "test.jpg",
                FileDirectory = "/photos",
                AddToGallery = true,
                Description = description
            };

            // Assert
            Assert.AreEqual(description, photo.Description, "Description should match the provided value.");
        }

        [TestMethod]
        public void Photo_AddToGallery_ShouldBeSetCorrectly()
        {
            // Arrange & Act
            var photo = new Photo
            {
                Id = 1,
                FileName = "test.jpg",
                FileDirectory = "/photos",
                AddToGallery = true
            };

            // Assert
            Assert.IsTrue(photo.AddToGallery, "AddToGallery should be true.");
        }

        [TestMethod]
        public void Photo_FileName_ShouldBeSetCorrectly()
        {
            // Arrange
            var fileName = "test.jpg";

            // Act
            var photo = new Photo
            {
                Id = 1,
                FileName = fileName,
                FileDirectory = "/photos",
                AddToGallery = true
            };

            // Assert
            Assert.AreEqual(fileName, photo.FileName, "FileName should match the provided value.");
        }

        [TestMethod]
        public void Photo_FileDirectory_ShouldBeSetCorrectly()
        {
            // Arrange
            var fileDirectory = "/photos";

            // Act
            var photo = new Photo
            {
                Id = 1,
                FileName = "test.jpg",
                FileDirectory = fileDirectory,
                AddToGallery = true
            };

            // Assert
            Assert.AreEqual(fileDirectory, photo.FileDirectory, "FileDirectory should match the provided value.");
        }

        [TestMethod]
        public void Photo_Id_ShouldBeSetCorrectly()
        {
            // Arrange & Act
            var photo = new Photo
            {
                Id = 1,
                FileName = "test.jpg",
                FileDirectory = "/photos",
                AddToGallery = true
            };

            // Assert
            Assert.AreEqual(1, photo.Id, "Id should match the provided value.");
        }
    }
}