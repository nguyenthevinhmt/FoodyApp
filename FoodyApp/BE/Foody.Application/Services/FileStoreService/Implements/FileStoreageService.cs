using Foody.Application.Services.FileStoreService.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace Foody.Application.Services.FileStoreService.Implements
{
    public class FileStorageService : IStorageService
    {
        private readonly string _imageUploadPath;
        private const string FILE_STORE_FOLDER = "ImageStorage";

        public FileStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _imageUploadPath = Path.Combine(Directory.GetCurrentDirectory(), FILE_STORE_FOLDER, "images");
            if (!Directory.Exists(_imageUploadPath))
            {
                Directory.CreateDirectory(_imageUploadPath);
            }
        }

        public string GetFileUrl(string fileName)
        {
            return $"/{FILE_STORE_FOLDER}/images/{fileName}";
        }

        public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(_imageUploadPath, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_imageUploadPath, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }
    }
}
