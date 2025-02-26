using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController(ApplicationDbContext _context) : ControllerBase
{

    [HttpGet("getAllTodoItems")]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
    {
        var result = await _context.TodoItems.ToListAsync();
        return Ok(result);
    }

    [HttpPost("createTodoItem")]
    public async Task<ActionResult<ResponseDto>> PostTodoItem(TodoItem todoItem)
    {
        _context.TodoItems.Add(todoItem);
        await _context.SaveChangesAsync();
        return new ResponseDto { Success = true, Message = "Todo item created successfully", Data = todoItem };
    }
    [HttpPut("updateTodoItem/{id}")]
    public async Task<ActionResult<ResponseDto>> PutTodoItem(int id, TodoItem todoItem)
    {
        if (id != todoItem.Id)
        {
            return BadRequest(new ResponseDto { Success = false, Message = "Invalid request, id mismatch" });
        }
        var existingTodoItem = await _context.TodoItems.FindAsync(id);
        if (existingTodoItem == null)
        {
            return NotFound(new ResponseDto { Success = false, Message = "Todo item not found" });
        }
        existingTodoItem.Title = todoItem.Title;
        existingTodoItem.IsCompleted = todoItem.IsCompleted;
        existingTodoItem.DueDate = todoItem.DueDate;
        existingTodoItem.Priority = todoItem.Priority;
        existingTodoItem.Category = todoItem.Category;
        existingTodoItem.Description = todoItem.Description;
        try
        {
            await _context.SaveChangesAsync();
            return new ResponseDto { Success = true, Message = "Todo item updated successfully", Data = todoItem };
        }
        catch (Exception ex)
        {
            return new ResponseDto { Success = false, Message = "An error occurred while updating the todo item", Data = ex.Message };
        }

    }

    [HttpDelete("deleteTodoItem/{id}")]
    public async Task<ActionResult<ResponseDto>> DeleteTodoItem(int id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);
        if (todoItem == null)
        {
            return NotFound(new ResponseDto { Success = false, Message = "Todo item not found" });
        }
        _context.TodoItems.Remove(todoItem);
        await _context.SaveChangesAsync();
        return new ResponseDto { Success = true, Message = "Todo item deleted successfully" };
    }

}