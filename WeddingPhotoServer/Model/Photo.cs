namespace WeddingPhotoServer.Model
{
    public class Photo
    {
        public int Id { get; set; }
        public required string FileName { get; set; }
        public string? Guid { get; set; }
        public required string FileDirectory { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? SendBy { get ; set; }
        public required bool AddToGallery { get; set; } = true;

        //Opcja public - czy zdjęcia mają się wyświetlać na "ścianie"
    }
}
