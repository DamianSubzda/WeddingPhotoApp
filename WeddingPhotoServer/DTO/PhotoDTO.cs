namespace WeddingPhotoServer.DTO
{
    public class PhotoDTO
    {
        public int Id { get; set; }
        public required string FileName { get; set; }
        public required string FullFilePath { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required bool AddToGallery { get; set; }
    }
}
