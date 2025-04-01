using System.ComponentModel.DataAnnotations;

namespace Courses_API.Models
{
	public class User
	{
		public int Id { get; set; }
		[Required]
		public required string UserName { get; set; }
		[Required]
		public required string Name { get; set; }
		[Required]
		public required string Lastname { get; set; }
		public Detail? Detail { get; set; }
	}
}
