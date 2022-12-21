namespace mtg_inventory_backend.Models;

public class AuthRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}