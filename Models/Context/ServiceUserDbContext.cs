using Microsoft.EntityFrameworkCore;
using System.Security;

namespace ServiceUser.Models.Context
{
    public partial class ServiceUserDbContext(DbContextOptions<ServiceUserDbContext> dbc) : DbContext(dbc)
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<RoleModel> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(u => u.Id).HasName("UserId");
                entity.HasIndex(u => u.Email).IsUnique();

                entity.Property(e => e.Email)
                .HasColumnName("UserEmail")
                .HasMaxLength(255);

                entity.Property(e => e.UserName)
                .HasColumnName("UserName")
                .HasMaxLength(255);

                entity.Property(e => e.Password).HasColumnName("UserPassword");

                entity.Property(e => e.RoleId).HasConversion<int>();
            });

            modelBuilder.Entity<RoleModel>(entity =>
            {
                entity.ToTable("Role");

                entity.HasKey(u => u.RoleId).HasName("RoleId");

                entity.Property(e => e.RoleId)
                .HasConversion<int>();

                entity.HasData(
                    Enum.GetValues(typeof(RoleId))
                    .Cast<RoleId>()
                    .Select(e => new RoleModel()
                    {
                        RoleId = e,
                        RoleName = e.ToString()
                    }));
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
