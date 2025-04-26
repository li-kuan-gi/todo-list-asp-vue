using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Server.Todo;

[ApiController]
[Route("api/[controller]")]
public class TodoController(TodoContext context) : ControllerBase
{
    private readonly TodoContext _context = context;

    /// <summary>
    /// Retrieves all todo items.
    /// </summary>
    /// <response code="200">All todo items were listed.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TodoItemDTO>), 200)]
    public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
    {
        IEnumerable<TodoItemDTO> todos = await _context.TodoItems
                .Select(item => TodoItemToDTO(item))
                .ToListAsync();

        return Ok(todos);
    }

    /// <summary>
    /// Retrieves a todo item by its ID.
    /// </summary>
    /// <param name="id">The ID of the todo item to be retrieved.</param>
    /// <response code="200">The todo item was found and returned successfully.</response>
    /// <response code="404">No todo item was found with the specified ID.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TodoItemDTO), 200)]
    [ProducesResponseType(typeof(void), 404)]
    public async Task<ActionResult<TodoItemDTO>> GetTodoItemById(int id)
    {
        TodoItem? item = await _context.TodoItems.FindAsync(id);

        if (item is null)
        {
            return NotFound();
        }
        else
        {
            TodoItemDTO dto = TodoItemToDTO(item);
            return Ok(dto);
        }
    }

    /// <summary>
    /// Create a new todo item with the provided data.
    /// </summary>
    /// <param name="data">The data used to create the new todo item.</param>
    /// <response code="201">The todo item was created successfully.</response>
    [HttpPost("create")]
    [ProducesResponseType(typeof(TodoItemDTO), 201)]
    public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemCreateData data)
    {
        TodoItem item = new()
        {
            Name = data.Name,
            IsComplete = data.IsComplete,
        };

        _context.TodoItems.Add(item);

        await _context.SaveChangesAsync();

        TodoItemDTO dto = TodoItemToDTO(item);

        return CreatedAtAction(nameof(GetTodoItemById), new { id = dto.Id }, dto);
    }

    /// <summary>
    /// Update the todo item with the specified ID using the provided data.
    /// </summary>
    /// <param name="id">The ID of the todo item to be updated.</param>
    /// <param name="data">The data used to update the specified todo item.</param>
    /// <response code="204">The todo item was updated successfully.</response>
    /// <response code="404">The todo item with the specified ID was not found.</response>
    [HttpPost("update/{id}")]
    [ProducesResponseType(typeof(void), 204)]
    [ProducesResponseType(typeof(void), 404)]
    public async Task<ActionResult> UpdateTodoItem(int id, TodoItemUpdateData data)
    {
        int maxAttempts = 3;
        Random rnd = new();

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            TodoItem? item = await _context.TodoItems.FindAsync(id);

            if (item is null)
            {
                return NotFound();
            }

            if (data.Name is not null)
            {
                item.Name = data.Name;
            }

            if (data.IsComplete is not null)
            {
                item.IsComplete = (bool)data.IsComplete;
            }

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (ex.Entries.Single().State == EntityState.Modified)
                {
                    int interval = rnd.Next(100);
                    await Task.Delay(interval);
                }
            }
        }

        throw new Exception();
    }

    /// <summary>
    /// Delete the todo item with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the todo item to be deleted.</param>
    /// <response code="204">The todo item was deleted successfully.</response>
    /// <response code="404">The todo item with the specified ID was not found.</response>
    [HttpPost("delete/{id}")]
    [ProducesResponseType(typeof(void), 204)]
    [ProducesResponseType(typeof(void), 404)]
    public async Task<ActionResult> DeleteTodoItem(int id)
    {
        TodoItem? item = await _context.TodoItems.FindAsync(id);

        if (item is null)
        {
            return NotFound();
        }

        _context.TodoItems.Remove(item);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) { }

        return NoContent();
    }

    private static TodoItemDTO TodoItemToDTO(TodoItem item)
    {
        return new TodoItemDTO
        {
            Id = item.Id,
            Name = item.Name,
            IsComplete = item.IsComplete,
        };
    }
}
