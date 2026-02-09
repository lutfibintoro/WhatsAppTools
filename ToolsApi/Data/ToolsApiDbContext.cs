using Microsoft.EntityFrameworkCore;
using ToolsApi.Data.Models;

namespace ToolsApi.Data
{
    public class ToolsApiDbContext : DbContext
    {
        public ToolsApiDbContext(DbContextOptions<ToolsApiDbContext> options) : base(options) { }

        public DbSet<UserNumber> UserNumbers { get; set; }
        public DbSet<ConnectionEvent> ConnectionEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserNumber>(buildAction =>
            {
                buildAction.HasKey(e => e.Id);
                buildAction.Property(e => e.Id)
                .IsRequired(true)
                .ValueGeneratedOnAdd();

                buildAction.Property(e => e.WhatsAppNumber)
                .IsRequired(true);

                buildAction
                .HasIndex(e => e.WhatsAppNumber)
                .IsUnique(true);
            });

            modelBuilder.Entity<ConnectionEvent>(buildAction =>
            {
                buildAction.HasKey(e => e.Id);
                buildAction.Property(e => e.Id)
                .IsRequired(true)
                .ValueGeneratedOnAdd();

                buildAction.Property(e => e.TransferId)
                .IsRequired(true)
                .HasMaxLength(18);

                buildAction
                .HasIndex(e => e.TransferId)
                .IsUnique(true);

                buildAction.Property(e => e.Offset)
                .IsRequired(true);

                buildAction.Property(e => e.Objective)
                .IsRequired(true);
                
                buildAction.Property(e => e.MimeType)
                .IsRequired(true);
            });
        }
    }
}
