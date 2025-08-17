using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Courses_API.Services
{
	public class FileStorageAzure : IFileStorage
	{

		private readonly string _connectionString;
		public FileStorageAzure(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("AzureStorageConnection")!; 
		}
		public async Task<string> Store(string container, IFormFile formFile)
		{
			BlobContainerClient client = new BlobContainerClient(_connectionString, container);
			await client.CreateIfNotExistsAsync();
			client.SetAccessPolicy(PublicAccessType.Blob);

			string extension = Path.GetExtension(formFile.FileName);
			string fileName = $"{Guid.NewGuid()}{extension}";

			BlobClient blobClient = client.GetBlobClient(fileName);

			BlobHttpHeaders blobHttpHeaders = new BlobHttpHeaders();
			blobHttpHeaders.ContentType = formFile.ContentType;
			await blobClient.UploadAsync(formFile.OpenReadStream(), blobHttpHeaders);

			return blobClient.Uri.ToString();
		}

		public async Task Delete(string? path, string container)
		{
			if (string.IsNullOrEmpty(path))
			{
				return;
			}

			BlobContainerClient client = new BlobContainerClient(_connectionString, container);
			await client.CreateIfNotExistsAsync();
			string fileName = Path.GetFileName(path);
			BlobClient blob = client.GetBlobClient(fileName);
			await blob.DeleteIfExistsAsync();
		}
	}
}
