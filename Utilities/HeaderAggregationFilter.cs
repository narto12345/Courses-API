using Microsoft.AspNetCore.Mvc.Filters;

namespace Courses_API.Utilities;

public class HeaderAggregationFilter : ActionFilterAttribute
{
    private readonly string name;
    private readonly string value;

    public HeaderAggregationFilter(string name, string value)
    {
        this.name = name;
        this.value = value;
    }

    public override void OnResultExecuting(ResultExecutingContext context)
    {
        // Antes
        context.HttpContext.Response.Headers.Append(name, value);
        base.OnResultExecuting(context);
        // Después
    }
}