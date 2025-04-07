// Make sure the namespace reflects your project name
namespace ZazaFastFood.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
}