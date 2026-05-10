using Core.Entities;
using Core.Entities.Common;
using Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Personnel> Personnels { get; set; }
        public DbSet<Core.Entities.Task> Tasks { get; set; }
        public DbSet<TaskAnalysis> TaskAnalyses { get; set; }
        public DbSet<TaskDevelopment> TaskDevelopments { get; set; }
        public DbSet<AssignmentHistory> AssignmentHistories { get; set; }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                    entry.Entity.CreatedAt = DateTime.UtcNow;

                else if (entry.State == EntityState.Modified)
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Personnel>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Core.Entities.Task>().HasQueryFilter(x => !x.IsDeleted);

            modelBuilder.Entity<Role>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).IsRequired().HasMaxLength(100);
            });

            // USER
            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Email).IsRequired().HasMaxLength(150);
                e.HasIndex(x => x.Email).IsUnique();
                e.Property(x => x.PasswordHash).IsRequired().HasMaxLength(500);

                e.HasOne(x => x.Role)
                    .WithMany(r => r.Users)
                    .HasForeignKey(x => x.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // PERSONNEL
            modelBuilder.Entity<Personnel>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
                e.Property(x => x.LastName).IsRequired().HasMaxLength(100);
                e.Property(x => x.Status).IsRequired();
                e.Property(x => x.CreatedByUserId);

                e.HasOne(x => x.User)
                    .WithOne(u => u.Personnel)
                    .HasForeignKey<Personnel>(x => x.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(x => x.CreatedByUserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // TASK
            modelBuilder.Entity<Core.Entities.Task>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Title).IsRequired().HasMaxLength(200);
                e.Property(x => x.Description).HasMaxLength(2000);
                e.Property(x => x.ExpectationNotes).HasMaxLength(1000);
                e.Property(x => x.Stage).IsRequired();

                e.HasOne(x => x.CreatedByPersonnel)
                    .WithMany(p => p.CreatedTasks)
                    .HasForeignKey(x => x.CreatedByPersonnelId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // TASK ANALYSIS
            modelBuilder.Entity<TaskAnalysis>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.AnalystNotes).HasMaxLength(2000);
                e.Property(x => x.RequirementsDetail).HasMaxLength(4000);
                e.Property(x => x.RejectionReason).HasMaxLength(1000);
                e.Property(x => x.Priority).IsRequired();
                e.Property(x => x.Status).IsRequired();
                e.Property(x => x.DifficultyLevel).HasConversion<int>();

                e.HasOne(x => x.Task)
                    .WithOne(t => t.TaskAnalysis)
                    .HasForeignKey<TaskAnalysis>(x => x.TaskId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.AnalystPersonnel)
                    .WithMany(p => p.TaskAnalyses)
                    .HasForeignKey(x => x.AnalystPersonnelId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // TASK DEVELOPMENT
            modelBuilder.Entity<TaskDevelopment>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.DeveloperNotes).HasMaxLength(2000);
                e.Property(x => x.CancellationReason).HasMaxLength(1000);
                e.Property(x => x.Status).IsRequired();

                e.HasOne(x => x.TaskAnalysis)
                    .WithOne(a => a.TaskDevelopment)
                    .HasForeignKey<TaskDevelopment>(x => x.TaskAnalysisId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.DeveloperPersonnel)
                    .WithMany(p => p.TaskDevelopments)
                    .HasForeignKey(x => x.DeveloperPersonnelId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ASSIGNMENT HISTORY
            modelBuilder.Entity<AssignmentHistory>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.DifficultyLevel).HasConversion<int?>().IsRequired(false);
                e.Property(x => x.StageType).IsRequired().HasConversion<int>();
                e.Property(x => x.SelectionReason).HasMaxLength(500);
                e.Property(x => x.ReassignmentReason).HasMaxLength(500);
                e.Property(x => x.IsConsecutiveBlocked);
                e.Property(x => x.IsReassignment);

                e.HasOne(x => x.Task)
                    .WithMany(t => t.AssignmentHistories)
                    .HasForeignKey(x => x.TaskId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.Personnel)
                    .WithMany(p => p.AssignmentHistories)
                    .HasForeignKey(x => x.PersonnelId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // SEED DATA
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var now = new DateTime();

            // Tüm kullanıcıların başlangıç şifresi: Pass1234!
            // Gerçek BCrypt hash: BCrypt.Net.BCrypt.HashPassword("Pass1234!")
            const string defaultHash = "$2a$11$replacethiswithrealbcrypthashbeforedeployment.";

            // Roller
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", CreatedAt = now },
                new Role { Id = 2, Name = "Opener", CreatedAt = now },
                new Role { Id = 3, Name = "Analyst", CreatedAt = now },
                new Role { Id = 4, Name = "Developer", CreatedAt = now }
            );

            // Kullanıcılar
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, RoleId = 1, Email = "admin@system.com", PasswordHash = defaultHash, IsActive = true, IsDeleted = false, CreatedAt = now },
                new User { Id = 2, RoleId = 2, Email = "ahmet.yilmaz@system.com", PasswordHash = defaultHash, IsActive = true, IsDeleted = false, CreatedAt = now },
                new User { Id = 3, RoleId = 3, Email = "ayse.kaya@system.com", PasswordHash = defaultHash, IsActive = true, IsDeleted = false, CreatedAt = now },
                new User { Id = 4, RoleId = 4, Email = "mehmet.demir@system.com", PasswordHash = defaultHash, IsActive = true, IsDeleted = false, CreatedAt = now },
                new User { Id = 5, RoleId = 4, Email = "fatma.celik@system.com", PasswordHash = defaultHash, IsActive = true, IsDeleted = false, CreatedAt = now },
                new User { Id = 6, RoleId = 4, Email = "ali.sahin@system.com", PasswordHash = defaultHash, IsActive = true, IsDeleted = false, CreatedAt = now },
                new User { Id = 7, RoleId = 4, Email = "zeynep.arslan@system.com", PasswordHash = defaultHash, IsActive = true, IsDeleted = false, CreatedAt = now },
                new User { Id = 8, RoleId = 4, Email = "mustafa.koc@system.com", PasswordHash = defaultHash, IsActive = true, IsDeleted = false, CreatedAt = now },
                new User { Id = 9, RoleId = 4, Email = "elif.erdogan@system.com", PasswordHash = defaultHash, IsActive = true, IsDeleted = false, CreatedAt = now },
                new User { Id = 10, RoleId = 4, Email = "hasan.dogan@system.com", PasswordHash = defaultHash, IsActive = true, IsDeleted = false, CreatedAt = now },
                new User { Id = 11, RoleId = 4, Email = "merve.yildiz@system.com", PasswordHash = defaultHash, IsActive = true, IsDeleted = false, CreatedAt = now }
            );

            // Personel
            modelBuilder.Entity<Personnel>().HasData(
                new Personnel { Id = 1, UserId = 2, FirstName = "Ahmet", LastName = "Yılmaz", Status = PersonnelStatus.Active, IsDeleted = false, CreatedAt = now, CreatedByUserId = 1 },
                new Personnel { Id = 2, UserId = 3, FirstName = "Ayşe", LastName = "Kaya", Status = PersonnelStatus.Active, IsDeleted = false, CreatedAt = now, CreatedByUserId = 1 },
                new Personnel { Id = 3, UserId = 4, FirstName = "Mehmet", LastName = "Demir", Status = PersonnelStatus.Active, IsDeleted = false, CreatedAt = now, CreatedByUserId = 1 },
                new Personnel { Id = 4, UserId = 5, FirstName = "Fatma", LastName = "Çelik", Status = PersonnelStatus.Active, IsDeleted = false, CreatedAt = now, CreatedByUserId = 1 },
                new Personnel { Id = 5, UserId = 6, FirstName = "Ali", LastName = "Şahin", Status = PersonnelStatus.Active, IsDeleted = false, CreatedAt = now, CreatedByUserId = 1 },
                new Personnel { Id = 6, UserId = 7, FirstName = "Zeynep", LastName = "Arslan", Status = PersonnelStatus.Active, IsDeleted = false, CreatedAt = now, CreatedByUserId = 1 },
                new Personnel { Id = 7, UserId = 8, FirstName = "Mustafa", LastName = "Koç", Status = PersonnelStatus.Active, IsDeleted = false, CreatedAt = now, CreatedByUserId = 1 },
                new Personnel { Id = 8, UserId = 9, FirstName = "Elif", LastName = "Erdoğan", Status = PersonnelStatus.Active, IsDeleted = false, CreatedAt = now, CreatedByUserId = 1 },
                new Personnel { Id = 9, UserId = 10, FirstName = "Hasan", LastName = "Doğan", Status = PersonnelStatus.Active, IsDeleted = false, CreatedAt = now, CreatedByUserId = 1 },
                new Personnel { Id = 10, UserId = 11, FirstName = "Merve", LastName = "Yıldız", Status = PersonnelStatus.Active, IsDeleted = false, CreatedAt = now, CreatedByUserId = 1 }
            );
        }
    }
}
