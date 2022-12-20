using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace mtg_inventory_backend.Models;

public class Card
{
    public int id { get; set; }
    [Required]
    public string name { get; set; }
    [Required]
    public string scryfallID { get; set; }

    [ForeignKey("ScryfallCard")]
    public string? scryfallCardId {get;set;}
    public int quantity { get; set; }

    [ForeignKey("Folder")]
    public int folderId {get;set;}
    public virtual ICollection<Deck> decks { get; set; }

    public Card(){
        this.decks = new HashSet<Deck>();
    }

}