namespace TodoApp.Models;

public class TodoItem
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public bool? IsCompleted { get; set; }
    public DateTime? DueDate { get; set; }
    public Priority? Priority { get; set; }
    public string? Category { get; set; }
    public string? Description { get; set; }
}
