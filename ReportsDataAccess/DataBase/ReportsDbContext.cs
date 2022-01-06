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
            Database.EnsureCreated();
        }

        public DbSet<WorkTask> Tasks { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReportsDomain.Models.WorkTask>().OwnsMany(
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