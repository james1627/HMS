using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HMS.Data;
using HMS.Models;
using System.Threading.Tasks;
using System.Linq;

namespace HMS.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public ExpensesController(AppDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetExpenses(int? projectId = null)
    {
        var userId = _userManager.GetUserId(User);
        var query = _context.Expenses.Include(e => e.Project).Where(e => e.UserId == userId);
        if (projectId.HasValue)
            query = query.Where(e => e.ProjectId == projectId.Value);

        var expenses = await query.ToListAsync();
        return Ok(expenses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetExpense(int id)
    {
        var userId = _userManager.GetUserId(User);
        var expense = await _context.Expenses.Include(e => e.Project).FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
        if (expense == null) return NotFound();
        return Ok(expense);
    }

    [HttpPost]
    public async Task<IActionResult> CreateExpense([FromBody] Expense expense)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var userId = _userManager.GetUserId(User);
        // Check if project belongs to user
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == expense.ProjectId && p.UserId == userId);
        if (project == null) return BadRequest("Invalid project");

        expense.UserId = userId!;
        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, expense);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExpense(int id, [FromBody] Expense updatedExpense)
    {
        var userId = _userManager.GetUserId(User);
        var expense = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
        if (expense == null) return NotFound();

        // If changing project, check ownership
        if (expense.ProjectId != updatedExpense.ProjectId)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == updatedExpense.ProjectId && p.UserId == userId);
            if (project == null) return BadRequest("Invalid project");
        }

        expense.Amount = updatedExpense.Amount;
        expense.Description = updatedExpense.Description;
        expense.Date = updatedExpense.Date;
        expense.ProjectId = updatedExpense.ProjectId;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(int id)
    {
        var userId = _userManager.GetUserId(User);
        var expense = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
        if (expense == null) return NotFound();

        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}