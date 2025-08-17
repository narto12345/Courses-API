using Microsoft.EntityFrameworkCore;

namespace Courses_API.Utilities
{
	public static class HttpContextExtensions
	{
		public async static Task InsertPaginationParams<T>(this HttpContext httpContext, IQueryable<T> queruable)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException(nameof(httpContext));
			}

			double count = await queruable.CountAsync();
			httpContext.Response.Headers.Append("total-records", count.ToString());
		}
	}
}
