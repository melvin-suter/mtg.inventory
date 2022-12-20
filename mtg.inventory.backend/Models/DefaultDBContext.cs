using Microsoft.EntityFrameworkCore;
using mtg_inventory_backend.Models;

namespace mtg_inventory_backend.Models;

public class DefaultDBContext : DbContext
{
    public DefaultDBContext(DbContextOptions<DefaultDBContext> options)
        : base(options)
    {
    }

    public DbSet<Collection> Collection { get; set; } = null!;

    public DbSet<mtg_inventory_backend.Models.Folder> Folder { get; set; } = default!;

    public DbSet<mtg_inventory_backend.Models.Card> Card { get; set; } = default!;

    public DbSet<mtg_inventory_backend.Models.Deck> Deck { get; set; } = default!;

    public DbSet<mtg_inventory_backend.Models.ScryfallCard> ScryfallCard { get; set; } = default!;


    // Seeding
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Collection>().HasData(
            new Collection
            {
                id = 1,
                name = "Default Collection",
                description = "This is an automatically created default collection",
                folders = new List<Folder>(){}
            }
        );
        modelBuilder.Entity<Folder>().HasData(
            new Folder(){
                id = 1, name = "Default Folder", collectionId = 1, description = "A default folder", cards = new List<Card>(){}
            }
        );
        modelBuilder.Entity<Card>().HasData(
            new Card(){
                id = 1,
                folderId = 1,
                name = "Backup Agent",
                scryfallID = "2a46af75-3880-4141-b26e-19834d67e7a8",
                scryfallCardId = null,
                quantity = 2
            }
        );
    }


    // Seeding
    public DbSet<mtg_inventory_backend.Models.User> User { get; set; } = default!;
}