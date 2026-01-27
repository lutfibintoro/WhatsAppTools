using Microsoft.EntityFrameworkCore;
using ToolsApi.Data.Models;

namespace ToolsApi.Data
{
    public class ToolsApiDbContext : DbContext
    {
        public ToolsApiDbContext(DbContextOptions<ToolsApiDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(buildAction =>
            {
                buildAction.HasKey(e => e.Id);
                buildAction.Property(e => e.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

                buildAction.Property(e => e.WhatsAppNumber).IsRequired(true);
            });
        }
    }
}
