namespace Courses_API.Dtos
{
	public class CourseFilterDto
	{
		public int Page { get; set; } = 1;
		public int RecordsForPage { get; set; } = 10;
		public PaginationDto PaginationDto
		{
			get
			{
				return new PaginationDto(Page, RecordsForPage);
			}
		}
		public string? Name { get; set; }
		public string? Description { get; set; }
		public bool? IsLessons { get; set; }
		public bool? IsFoto { get; set; }
		public string? Lessons { get; set; }
		public bool IncludeLessons { get; set; }
		public string? SortFile { get; set; }
		public bool AscendingOrder { get; set; } = true;
	}
}
