using Microsoft.AspNetCore.Mvc.Filters;

namespace Courses_API.Utilities;

public class ActionFilter : IActionFilter
{
    private readonly ILogger<ActionFilter> _logger;
    public ActionFilter(ILogger<ActionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation("Action Filter Executing");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation("Action Filter Executed");
    }
}