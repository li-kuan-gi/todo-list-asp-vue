using Microsoft.EntityFrameworkCore;

namespace Server.Todo;

public class TodoContext(DbContextOptions<TodoContext> options) : DbContext(options)
{
    public DbSet<TodoItem> TodoItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TodoItem>()
            .ToTable("TodoItems");

        modelBuilder.Entity<TodoItem>()
            .Property(t => t.Id)
            .HasColumnName("id");

        modelBuilder.Entity<TodoItem>()
            .Property(t => t.Name)
            .HasColumnName("name");

        modelBuilder.Entity<TodoItem>()
            .Property(t => t.IsComplete)
            .HasColumnName("isComplete");
    }
}
