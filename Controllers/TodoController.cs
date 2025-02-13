using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController(ApplicationDbContext _context) : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public TodoController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("getAllTodoItems")]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
    {
        var result = await _context.TodoItems.ToListAsync();
        return Ok(result);
    }

    [HttpPost("createTodoItem")]
    public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
    {
        _context.TodoItems.Add(todoItem);
        await _context.SaveChangesAsync();
        return  ("GetTodoItem", new { id = todoItem.Id }, todoItem);
    }

}