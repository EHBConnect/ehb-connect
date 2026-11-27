using ehb_connect.Server.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ehb_connect.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<ProjectSkill> ProjectSkills { get; set; }
        public DbSet<ContactRequest> ContactRequests { get; set; }
        public DbSet<CollaborationAgreement> CollaborationAgreements { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("Users");
            builder.Entity<Project>().ToTable("Projects");
            builder.Entity<ProjectMember>().ToTable("ProjectMembers");
            builder.Entity<Skill>().ToTable("Skills");
            builder.Entity<UserSkill>().ToTable("UserSkills");
            builder.Entity<ProjectSkill>().ToTable("ProjectSkills");
            builder.Entity<ContactRequest>().ToTable("ContactRequests");
            builder.Entity<CollaborationAgreement>().ToTable("CollaborationAgreements");


            builder.Entity<Project>()
                .HasOne(p => p.Owner)
                .WithMany()
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ProjectMember>()
                .HasKey(pm => new { pm.ProjectId, pm.UserId });

            builder.Entity<ProjectMember>()
                .HasOne(pm => pm.Project)
                .WithMany(p => p.ProjectMembers)
                .HasForeignKey(pm => pm.ProjectId);

            builder.Entity<ProjectMember>()
                .HasOne(pm => pm.User)
                .WithMany(u => u.ProjectMembers)
                .HasForeignKey(pm => pm.UserId);

            
            builder.Entity<UserSkill>()
                .HasKey(us => new { us.UserId, us.SkillId });

            builder.Entity<UserSkill>()
                .HasOne(us => us.User)
                .WithMany(u => u.UserSkills)
                .HasForeignKey(us => us.UserId);

            builder.Entity<UserSkill>()
                .HasOne(us => us.Skill)
                .WithMany(s => s.UserSkills)
                .HasForeignKey(us => us.SkillId);

           
            builder.Entity<ProjectSkill>()
                .HasKey(ps => new { ps.ProjectId, ps.SkillId });

            builder.Entity<ProjectSkill>()
                .HasOne(ps => ps.Project)
                .WithMany(p => p.ProjectSkills)
                .HasForeignKey(ps => ps.ProjectId);

            builder.Entity<ProjectSkill>()
                .HasOne(ps => ps.Skill)
                .WithMany(s => s.ProjectSkills)
                .HasForeignKey(ps => ps.SkillId);
            builder.Entity<ContactRequest>()
              .HasOne(cr => cr.Sender)
              .WithMany(u => u.SentContactRequests)
              .HasForeignKey(cr => cr.SenderId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ContactRequest>()
                .HasOne(cr => cr.Receiver)
                .WithMany(u => u.ReceivedContactRequests)
                .HasForeignKey(cr => cr.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
