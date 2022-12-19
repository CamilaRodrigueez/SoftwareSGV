using Infraestructure.Entity.Model;
using Infraestructure.Entity.Model.GA;
using Infraestructure.Entity.Model.Master;
using Infraestructure.Entity.Model.Security;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Core.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<UserEntity> UserEntity { get; set; }
        public DbSet<RolEntity> RolEntity { get; set; }
        public DbSet<RolUserEntity> RolUserEntity { get; set; }
        public DbSet<PermissionEntity> PermissionEntity { get; set; }
        public DbSet<RolPermissionEntity> RolPermissionEntity { get; set; }

        public DbSet<TypePermissionEntity> TypePermissionEntity { get; set; }

        public DbSet<TypeUserEntity> TypeUserEntity { get; set; }
        public DbSet<UserTypeUserEntity> UserTypeUserEntity { get; set; }

        public DbSet<ClassEntity> ClassEntity { get; set; }
        public DbSet<ExcuseEntity> ExcuseEntity { get; set; }
        public DbSet<FichaClassEntity> FichaClassEntity { get; set; }
        public DbSet<FichaEntity> FichaEntity { get; set; }
        public DbSet<FichaUserEntity> FichaUserEntity { get; set; }
        public DbSet<PresenceEntity> PresenceEntity { get; set; }
        public DbSet<NotificationEntity> NotificationEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
               .HasIndex(b => b.Email)
               .IsUnique()
               .HasName("Index_Email");

            modelBuilder.Entity<UserEntity>()
               .HasIndex(b => b.Identification)
               .IsUnique()
               .HasName("Index_Identification");

            modelBuilder.Entity<FichaEntity>()
               .HasIndex(b => b.Num_Ficha)
               .IsUnique()
               .HasName("Index_NumFicha");

            modelBuilder.Entity<TypeUserEntity>().Property(t => t.IdTypeUser).ValueGeneratedNever();
            modelBuilder.Entity<TypePermissionEntity>().Property(t => t.IdTypePermission).ValueGeneratedNever();
            modelBuilder.Entity<RolEntity>().Property(t => t.IdRol).ValueGeneratedNever();
            modelBuilder.Entity<PermissionEntity>().Property(t => t.IdPermission).ValueGeneratedNever();
        }
    }
}
