using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Models;

public class Expense
{
    public int Id { get; set; }

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }

    [StringLength(200)]
    public string? Description { get; set; }

    [Required]
    public DateTime Date { get; set; } = DateTime.UtcNow;

    [Required]
    public int ProjectId { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty; // Who added the expense

    // Navigation
    [ForeignKey("ProjectId")]
    public virtual Project Project { get; set; } = null!;
}