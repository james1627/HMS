using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HMS.Api.Data;
using HMS.Api.Models;
using System.Threading.Tasks;
using System.Linq;

namespace HMS.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public ProjectsController(AppDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetProjects()
    {
        var userId = _userManager.GetUserId(User);
        var projects = await _context.Projects.Where(p => p.UserId == userId).ToListAsync();
        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProject(int id)
    {
        var userId = _userManager.GetUserId(User);
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
        if (project == null) return NotFound();
        return Ok(project);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] Project project)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        project.UserId = _userManager.GetUserId(User)!;
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(int id, [FromBody] Project updatedProject)
    {
        var userId = _userManager.GetUserId(User);
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
        if (project == null) return NotFound();

        project.Name = updatedProject.Name;
        project.Description = updatedProject.Description;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var userId = _userManager.GetUserId(User);
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
        if (project == null) return NotFound();

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}