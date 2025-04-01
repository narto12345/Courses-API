using System.ComponentModel.DataAnnotations;

namespace Courses_API.Models
{
	public class User
	{
		public int Id { get; set; }
		[Required]
		public string? UserName { get; set; }
		[Required]
		public string? Name { get; set; }
		
		public string? Lastname { get; set; }
		public Detail? Detail { get; set; }
	}
}
