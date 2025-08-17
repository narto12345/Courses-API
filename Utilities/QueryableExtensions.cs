using Courses_API.Dtos;

namespace Courses_API.Utilities
{
	public static class QueryableExtensions
	{
		public static IQueryable<T> Page<T>(this IQueryable<T> queryable, PaginationDto paginationDto)
		{
			return 
				queryable.Skip((paginationDto.Page - 1) * paginationDto.RecordsForPage)
				.Take(paginationDto.RecordsForPage);
		}
	}
}
