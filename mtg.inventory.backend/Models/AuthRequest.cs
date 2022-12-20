using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace mtg_inventory_backend.Models;

public class AuthRequest
{
   public string username {get;set;}
   public string password {get;set;}
}