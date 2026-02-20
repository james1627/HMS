using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HMS.Models;

public class Project
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty; // Owner

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public virtual ICollection<Expense> Expenses { get; set; } = [];
}