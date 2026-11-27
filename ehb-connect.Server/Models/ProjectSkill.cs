using System.ComponentModel.DataAnnotations;

namespace ehb_connect.Server.Models
{
    public class ProjectSkill
    {
        public int ProjectId { get; set; }

        [Required]
        public Project Project { get; set; } = null!;


        public int SkillId { get; set; }

        [Required]
        public Skill Skill { get; set; } = null!;
    }
}