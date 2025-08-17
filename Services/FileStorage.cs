

namespace Courses_API.Services
{
	public class FileStorage : IFileStorage
	{

		public Task Delete(string? path, string container)
		{
			throw new NotImplementedException();
		}

		public Task<string> Store(string container, IFormFile formFile)
		{
			throw new NotImplementedException();
		}
	}
}
