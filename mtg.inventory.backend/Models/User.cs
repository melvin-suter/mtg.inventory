using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace mtg_inventory_backend.Models;

public class User
{
    public int id { get; set; }
    [Required]
    [EmailAddress]
    public string email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string password { get; set; }

    public void setPasswordHash(string plainPassword) {
        this.password = BCrypt.Net.BCrypt.HashPassword(plainPassword);
    }

    public bool checkPassword(string plainPassword){
        return BCrypt.Net.BCrypt.Verify(plainPassword, this.password);
    }

}