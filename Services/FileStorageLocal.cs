
namespace Courses_API.Services
{
	public class FileStorageLocal : IFileStorage
	{
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IHttpContextAccessor _contextAccessor;
		public FileStorageLocal(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor contextAccessor)
		{
			_webHostEnvironment = webHostEnvironment;
			_contextAccessor = contextAccessor;
		}
		public Task Delete(string? path, string container)
		{
			if (string.IsNullOrEmpty(path))
			{
				return Task.CompletedTask;
			}

			var fileName = Path.GetFileName(path);
			var fileDirectory = Path.Combine(_webHostEnvironment.WebRootPath, container, fileName);

			if (File.Exists(fileDirectory))
			{
				File.Delete(fileDirectory);
			}

			return Task.CompletedTask;
		}

		public async Task<string> Store(string container, IFormFile formFile)
		{
			string extension = Path.GetExtension(formFile.FileName);
			string fileName = $"{Guid.NewGuid()}{extension}";
			string folder = Path.Combine(_webHostEnvironment.WebRootPath, container);

			if (!Directory.Exists(folder))
			{
				Directory.CreateDirectory(folder);
			}

			string path = Path.Combine(folder, fileName);
			using (MemoryStream ms = new MemoryStream())
			{
				await formFile.CopyToAsync(ms);
				byte[] content = ms.ToArray();
				await File.WriteAllBytesAsync(path, content);
			}

			string url = $"{_contextAccessor.HttpContext!.Request.Scheme}://{_contextAccessor.HttpContext!.Request.Host}";
			string fileUrl = Path.Combine(url, container, fileName).Replace("\\", "/");
			return fileUrl;
		}
	}
}
