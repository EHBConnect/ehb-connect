using System.ComponentModel.DataAnnotations;
using Azure.Identity;
using Microsoft.AspNetCore.Identity;

namespace ehb_connect.Server.Models
{
    public class User : IdentityUser
    {
        // Primary key for the User entity
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Role { get; set; } = "Student";

        [Required]
        [StringLength(50)]
        public string Campus {  get; set; } = string.Empty;

        [Required]
        [StringLength (100)]
        public string Programme {  get; set; } = string.Empty;

        [StringLength(100)]
        public string? Availability { get; set; }
        public bool IsVisible { get; set; } = true;

        public DateTime ProfileUpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();

        public ICollection<ContactRequest> SentContactRequests { get; set; } = new List<ContactRequest>();

        public ICollection<ContactRequest> ReceivedContactRequests { get; set; } = new List<ContactRequest>();


    }
}
