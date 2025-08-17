namespace Courses_API.Services
{
	public interface IFileStorage
	{
		Task Delete(string? path, string container);
		Task<string> Store(string container, IFormFile formFile);
		async Task<string> Update(string? path, string container, IFormFile formFile)
		{
			await Delete(path, container);
			return await Store(container, formFile);
		}
	}
}
