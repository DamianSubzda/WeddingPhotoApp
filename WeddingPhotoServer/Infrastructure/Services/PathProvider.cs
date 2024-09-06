namespace WeddingPhotoServer.Infrastructure.Interface
{
    public class PathProvider : IPathProvider
    {
        private readonly IConfiguration _config;

        public PathProvider(IConfiguration config)
        {
            _config = config;
        }

        public string GetPath()
        {
            var path = _config.GetValue<string>("StoredFilesPath");

            if (!Path.IsPathFullyQualified(path))
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), path);
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        public string GetDirectory()
        {
            var path = _config.GetValue<string>("StoredFilesPath");

            if (!Path.IsPathFullyQualified(path))
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), path);
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), path));
            }

            return path;
        }
    }
    public interface IPathProvider
    {
        string GetPath();
        string GetDirectory();
    }

}
