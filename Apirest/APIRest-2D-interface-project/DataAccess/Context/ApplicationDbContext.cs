using APIRest_2D_interface_project.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIRest_2D_interface_project.DataAccess.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets definition
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User
            modelBuilder.Entity<User>(entity =>
            {
                // Primary key
                entity.HasKey(e => e.Id);

                // Constraints configuration
                entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(20);

                entity.Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(80);

                entity.Property(e => e.Email)
                .IsRequired();

                entity.Property(e => e.IsVerified)
                .HasDefaultValue(false);

                // Checks configuration
                entity.ToTable("Users", tb =>
                {
                    // Username
                    tb.HasCheckConstraint("CK_Users_UsernameLength",
                        "LEN([Username]) >= 4");

                    // Checks password rule
                    tb.HasCheckConstraint("CK_Users_PasswordComplexity",
                        @"[Password] LIKE '%[A-Z]%'
                        AND [Password] LIKE '%[a-z]%'
                        AND [Password] LIKE '%[0-9]%'
                        AND [Password] LIKE '%[!@#$%^&*()]%'
                        AND LEN([Password]) >= 8");
                });

            });
        }
    }
}
