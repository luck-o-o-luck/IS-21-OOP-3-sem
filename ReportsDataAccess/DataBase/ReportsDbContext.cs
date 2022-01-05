using Microsoft.EntityFrameworkCore;
using ReportsDomain.Enums;
using ReportsDomain.Models;

namespace ReportsDataAccess.DataBase
{
    public sealed class ReportsDbContext : DbContext
    {
        public ReportsDbContext() { }

        public ReportsDbContext(DbContextOptions<ReportsDbContext> options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<ReportsDomain.Models.Task> Tasks { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReportsDomain.Models.Task>().HasData(
                new ReportsDomain.Models.Task("Pass OOP"),
                new ReportsDomain.Models.Task("Get 80 points..."));

            modelBuilder.Entity<Employee>().HasData(
                new Employee(new NameEmployee(
                        "Ksenia", "Vasyutinskaya", "Sergeevna"),
                    EmployeeStatus.OrdinaryEmployee));
            
            modelBuilder.Entity<ReportsDomain.Models.Task>().OwnsMany(
                task => task.Modifications,
                modification =>
                    modification.WithOwner());

            modelBuilder.Entity<Employee>()
                .HasMany(x => x.Tasks);

            modelBuilder.Entity<Report>()
                .HasMany(x => x.Tasks);

            base.OnModelCreating(modelBuilder);
        }
    }
}