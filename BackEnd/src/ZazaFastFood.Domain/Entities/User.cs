﻿// Make sure the namespace reflects your project name
namespace ZazaFastFood.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;
    public string Role { get; set; } = "Customer"; // e.g., "Customer", "Admin"
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}