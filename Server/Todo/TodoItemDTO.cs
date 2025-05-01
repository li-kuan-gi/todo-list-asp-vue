namespace Server.Todo;

public class TodoItemDTO
{
    /// <summary>
    /// The ID of the todo item.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name for the todo item.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// To specify the todo item is completed or not.
    /// </summary>
    public bool IsComplete { get; set; }
}
