using DatabaseModels;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Note> Notes { get; set; } = null!;
    public DbSet<Reminder> Reminders { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Note>()
            .HasMany(n => n.Tags)
            .WithMany();
        modelBuilder.Entity<Reminder>()
            .HasMany(r => r.Tags)
            .WithMany();
    }
}