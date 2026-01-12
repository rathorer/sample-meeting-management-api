
using Microsoft.EntityFrameworkCore;
using MeetingTaskManagement.Models;

namespace MeetingTaskManagement.DataLayer
{
    public class InMemoryDbContext : DbContext
    {
        public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options)
            : base(options)
        {
        }

        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
    }
}