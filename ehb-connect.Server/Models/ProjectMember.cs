namespace ehb_connect.Server.Models
{
    public class ProjectMember
    {
        // Combined key: ProjectId + UserId
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = null!;

        public MemberRole MemberRole { get; set; }
        public DateTime JoinedAt { get; set; }
    }
}
