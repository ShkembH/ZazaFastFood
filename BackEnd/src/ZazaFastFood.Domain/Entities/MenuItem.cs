using System.ComponentModel.DataAnnotations.Schema;

// Make sure the namespace reflects your project name
namespace ZazaFastFood.Domain.Entities;

public class MenuItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public int CategoryId { get; set; }
    public virtual Category? Category { get; set; }
}