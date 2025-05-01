namespace Server.Todo;

public class TodoItemCreateData
{
    /// <summary>
    /// The name for the todo item.
    /// Required.
    /// </summary>
    /// <example>Just a COOL~~ name</example>
    public required string Name { get; set; }

    /// <summary>
    /// To specify the todo item is completed or not.
    /// Optional.
    /// </summary>
    public bool IsComplete { get; set; } = false;
}