using System.ComponentModel.DataAnnotations;

namespace mtg_inventory_backend.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    public void SetPasswordHash(string plainPassword) 
    {
        Password = BCrypt.Net.BCrypt.HashPassword(plainPassword);
    }

    public bool CheckPassword(string plainPassword)
    {
        return BCrypt.Net.BCrypt.Verify(plainPassword, Password);
    }

}