using Microsoft.AspNetCore.Mvc;

namespace Server.Todo;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    /// <summary>
    /// Retrieves all todo items.
    /// </summary>
    /// <response code="200">All todo items were listed.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TodoItemDTO>), 200)]
    public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
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
        throw new NotImplementedException();
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
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }
}
