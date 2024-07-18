using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DBContext
{
    public class EmployeeDirectoryDbContext : DbContext
    {

        public EmployeeDirectoryDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<User> users { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Manager> Managers { get; set; }

        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpId);

                entity.Property(e => e.EmpId)
                    .HasColumnType("varchar(6)");

                entity.Property(e => e.FirstName)
                    .IsRequired();

                entity.Property(e => e.LastName)
                    .IsRequired();

                entity.Property(e => e.Email)
                    .IsRequired();

                entity.HasIndex(e => e.Email)
                    .IsUnique();

                entity.Property(e => e.DateofBirth)
                    .HasColumnType("Date");

                entity.Property(e => e.JoiningDate)
                    .HasColumnType("Date")
                    .IsRequired();

                entity.Property(e => e.PhoneNumber)
                    .HasColumnType("varchar(10)")
                    .IsRequired();

                entity.HasIndex(e => e.PhoneNumber)
                    .IsUnique();

                entity.Property(e => e.RoleId)
                    .IsRequired();

                entity.Property(e => e.LocationId)
                    .IsRequired();

                entity.Property(e => e.ProjectId)
                    .IsRequired();

                entity.Property(e => e.ManagerId)
                    .IsRequired();

                entity.HasOne(e => e.Role)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(e => e.RoleId);

                entity.HasOne(e => e.Location)
                    .WithMany(L => L.Employees)
                    .HasForeignKey(e => e.LocationId);

                entity.HasOne(e => e.Manager)
                    .WithMany(m => m.Employees)
                    .HasForeignKey(e => e.ManagerId);

                entity.HasOne(e => e.Project)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(e => e.ProjectId);
            });


            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity.Property(entity => entity.Id)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn(seed: 1, increment: 1);

                entity.Property(entity => entity.DepartmentId)
                    .IsRequired();

                entity.Property(entity => entity.RoleName)
                    .IsRequired();

                entity.HasOne(d => d.Dept)
                    .WithMany(r => r.Roles)
                    .HasForeignKey(d => d.DepartmentId);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity.Property(entity => entity.Id)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn(seed: 1, increment: 1);

                entity.Property(entity => entity.DepartmentName)
                    .IsRequired();

                entity.HasIndex(entity => entity.DepartmentName)
                    .IsUnique();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity.Property(entity => entity.Id)
                        .ValueGeneratedOnAdd()
                        .UseIdentityColumn(seed: 1, increment: 1);

                entity.Property(entity => entity.Name).IsRequired();

                entity.Property(entity => entity.emailAddress).IsRequired();

                entity.HasIndex(entity => entity.emailAddress)
                        .IsUnique();

                entity.Property(entity => entity.Password).IsRequired();

                entity.Property(entity => entity.Role).IsRequired();

            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity.Property(entity => entity.Id)
                      .ValueGeneratedOnAdd()
                      .UseIdentityColumn(seed: 1, increment: 1);

                entity.Property(entity => entity.Name).IsRequired();

            });

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity.Property(entity => entity.Id)
                      .ValueGeneratedOnAdd()
                      .UseIdentityColumn(seed: 1, increment: 1);

                entity.Property(entity => entity.Name).IsRequired();

            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity.Property(entity => entity.Id)
                      .ValueGeneratedOnAdd()
                      .UseIdentityColumn(seed: 1, increment: 1);

                entity.Property(entity => entity.Name).IsRequired();

            });

        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added || e.State == EntityState.Modified
            ));

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedBy = Environment.UserName;
                    ((BaseEntity)entityEntry.Entity).CreatedOn = DateOnly.FromDateTime(DateTime.Now);
                }

                if (entityEntry.State == EntityState.Modified)
                {
                    ((BaseEntity)entityEntry.Entity).ModifiedBy = Environment.UserName;
                    ((BaseEntity)entityEntry.Entity).ModifiedOn = DateOnly.FromDateTime(DateTime.Now);
                }
            }

            return base.SaveChanges();
        }
    }
}
