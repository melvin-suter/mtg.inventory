using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mtg_inventory_backend.Models;

public class Folder
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    [ForeignKey("Collection")]
    public int CollectionId { get; set; }

    public virtual ICollection<FolderCard> Cards { get; set; } = null!;
}