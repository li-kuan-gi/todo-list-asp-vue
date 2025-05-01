namespace Server.Todo;

public class TodoItemUpdateData
{

    /// <summary>
    /// The name for the todo item.
    /// Optional.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// To specify the todo item is completed or not.
    /// Optional.
    /// </summary>
    public bool? IsComplete { get; set; }
}