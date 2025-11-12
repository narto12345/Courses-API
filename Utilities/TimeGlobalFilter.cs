using Microsoft.AspNetCore.Mvc.Filters;

namespace Courses_API.Utilities;

public class TimeGlobalFilter : IAsyncActionFilter
{
    private readonly ILogger<TimeGlobalFilter> logger;

    public TimeGlobalFilter(ILogger<TimeGlobalFilter> logger)
    {
        this.logger = logger;
    }
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var stopWatch = System.Diagnostics.Stopwatch.StartNew();
        logger.LogInformation("Starting the action {ActionName}", context.ActionDescriptor.DisplayName);

        await next();

        stopWatch.Stop();
        logger.LogInformation("Finished the action {ActionName} in {ElapsedMilliseconds} ms", context.ActionDescriptor.DisplayName, stopWatch.ElapsedMilliseconds);
    }
}