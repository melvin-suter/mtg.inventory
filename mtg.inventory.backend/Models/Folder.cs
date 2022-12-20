using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mtg_inventory_backend.Models;

public class Folder
{
    public int id { get; set; }
    [Required]
    public string name { get; set; }
    public string? description { get; set; }

    [ForeignKey("Collection")]
    public int collectionId {get;set;}

    public List<Card> cards {get;set;}
}