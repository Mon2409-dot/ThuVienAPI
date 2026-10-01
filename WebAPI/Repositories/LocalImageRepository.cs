using WebAPI.Data;
using WebAPI.Models.Domain;

namespace WebAPI.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnviroment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _dbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor, AppDbContext dbContext)
        {
            _webHostEnviroment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        public Image Upload(Image image)
        {
            var localFilePath = Path.Combine(_webHostEnviroment.ContentRootPath, "Images",
                $"{image.FileName}{image.FileExtension}");

            using var stream = new FileStream(localFilePath, FileMode.Create);
            image.File.CopyTo(stream);

            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://" +
                $"{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/" +
                $"{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;

            _dbContext.Images.Add(image);
            _dbContext.SaveChanges();

            return image;
        }

        public List<Image> GetAllInfoImages()
        {
            return _dbContext.Images.ToList();
        }

        public (byte[], string, string) DownloadFile(int Id)
        {
            try
            {
                var fileById = _dbContext.Images.Where(x => x.Id == Id).FirstOrDefault();
                var path = Path.Combine(_webHostEnviroment.ContentRootPath, "Images",
                    $"{fileById.FileName}{fileById.FileExtension}");
                var stream = File.ReadAllBytes(path);
                var fileName = fileById.FileName + fileById.FileExtension;
                return (stream, "application/octet-stream", fileName);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}