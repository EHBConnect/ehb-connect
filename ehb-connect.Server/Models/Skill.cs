using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ehb_connect.Server.Models
{
    public class Skill
    {
        // Automatically a key
        public int SkillId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;

        // Skill could be coupled to many UserSkill-records or ProjectSkill-records
        public ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
        public ICollection<ProjectSkill> ProjectSkills { get; set; } = new List<ProjectSkill>();


    }
}
