using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ehb_connect.Server.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(2000)]
        public string Description { get; set; } = string.Empty;

        // Best to use an enum for the status to ensure type safety and avoid magic strings
        // Adjust in ERD
        public ProjectStatus Status { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //Foreign key for the User entity (Owner of the project)
        public int OwnerId { get; set; }

        // Navigation property for the User entity (Owner of the project)
        public User? Owner { get; set; }

        // Optional fields (possibly nullable)
        public string? Campus { get; set; }
        public int? MaxMembers { get; set; }

        public ICollection<ProjectSkill> ProjectSkills { get; set; } = new List<ProjectSkill>();
    }
}
