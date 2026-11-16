using System.ComponentModel.DataAnnotations;

namespace ehb_connect.Server.DTOs
{
	public class UpdateUserDto
	{
		[Required]
		[StringLength(50)]
		public string FirstName { get; set; } = string.Empty;

		[Required]
		[StringLength(50)]
		public string LastName { get; set; } = string.Empty;

		[Required]
		[StringLength(50)]
		public string Campus { get; set; } = string.Empty;

		[Required]
		[StringLength(100)]
		public string Programme { get; set; } = string.Empty;

		[StringLength(100)]
		public string? Availability { get; set; }

		public bool IsVisible { get; set; }
	}
}
