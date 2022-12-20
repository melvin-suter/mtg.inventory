using System.ComponentModel.DataAnnotations;

namespace mtg_inventory_backend.Models;

public class Deck
{
    public int id { get; set; }
    [Required]
    public string name { get; set; }
    public string? description { get; set; }
    /*public List <Card> cards {get;set;}*/
    public virtual ICollection<Card> cards { get; set; }

    public Deck(){
        this.cards = new HashSet<Card>();
    }
}