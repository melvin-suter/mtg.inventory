namespace mtg_inventory_backend.Models;

public class FolderCard
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string CardMetadataId { get; set; } = null!;

    public int Quantity { get; set; }

    public int FolderId { get; set; }

    public virtual ICollection<Deck> Decks { get; set; } = null!;

}