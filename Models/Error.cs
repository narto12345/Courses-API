namespace Courses_API.Models;

public class Error
{
    public Guid Id { get; set; }
    public required string ErrorMessage { get; set; }
    public required string StackTrace { get; set; }
    public DateTime Date { get; set; }
}