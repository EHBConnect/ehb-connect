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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("Users");
            builder.Entity<Project>().ToTable("Projects");

            builder.Entity<Project>()
                .HasOne(p => p.Owner) // One Project has one Owner (User)
                .WithMany() // One User can own many Projects
                .HasForeignKey(p => p.OwnerId) // Foreign key in Project table: OwnerId
                .HasPrincipalKey(u => u.UserId) // Principal key in User table: UserId
                .OnDelete(DeleteBehavior.Cascade); // When a User is deleted, all their Projects are also deleted

            // Composed PK
            builder.Entity<UserSkill>()
                .HasKey(us => new
                {
                    us.UserId,
                    us.SkillId,
                    us.Type
                });

            // Composed PK
            builder.Entity<ProjectSkill>()
                .HasKey(ps => new
                {
                    ps.ProjectId,
                    ps.SkillId
                });
        }
    }
}
