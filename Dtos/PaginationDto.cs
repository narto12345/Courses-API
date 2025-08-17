namespace Courses_API.Dtos
{
	public record PaginationDto(int Page = 1, int RecordsForPage = 5)
	{
		private const int maxRecordForPage = 100;
		public int Page { get; set; } = Math.Max(1, Page);
		public int RecordsForPage { get; set; } = Math.Clamp(RecordsForPage, 1, maxRecordForPage);
	}
}
