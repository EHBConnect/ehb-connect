using System.ComponentModel.DataAnnotations;

namespace ehb_connect.Server.Models
{
    public class UserSkill
    {
        public string UserId { get; set; } = string.Empty;

        //Navigation property to User
        [Required]
        public User User { get; set; } = null!;


        public int SkillId { get; set; }

        // Navigation property to skill
        [Required]
        public Skill Skill { get; set; } = null!;


        // Best to make it an enumerator in ERD, this due to readability
        public SkillType Type { get; set; }

        // Best to make it an enumerator in ERD, this due to readability
        public SkillLevel Level { get; set; }



    }
}
