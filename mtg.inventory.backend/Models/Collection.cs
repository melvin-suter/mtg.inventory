using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace mtg_inventory_backend.Models;

public class Collection
{
    public int id { get; set; }
    public string name { get; set; }
    public string? description { get; set; }

    public List<Folder> folders {get;set;}

}