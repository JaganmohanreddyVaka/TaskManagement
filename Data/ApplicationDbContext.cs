using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Entities;
using Task = TaskManagementSystem.Entities.Task;

namespace TaskManagementSystem.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
                
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<TaskNote> TaskNotes { get; set; }

        public DbSet<TaskDocument> TaskDocuments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
    
}
