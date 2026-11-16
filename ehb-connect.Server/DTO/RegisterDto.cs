using System.ComponentModel.DataAnnotations;

namespace ehb_connect.Server.DTO
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

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
    }
}
