using Microsoft.EntityFrameworkCore;

namespace Server.Todo;

public class TodoContext(DbContextOptions<TodoContext> options) : DbContext(options)
{
    public DbSet<TodoItem> TodoItems { get; set; }
}