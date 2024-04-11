using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Animals
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Category { get; set; }
    [Required]
    public float Mass { get; set; }
    [Required]
    public string? FurColor { get; set; }
}