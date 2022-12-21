using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace mtg_inventory_backend.Models;

public class DefaultDBContext : DbContext
{
    public DefaultDBContext(DbContextOptions<DefaultDBContext> options)
        : base(options)
    {
    }

    public DbSet<Collection> Collection { get; set; } = null!;
    public DbSet<Folder> Folder { get; set; } = null!;
    public DbSet<FolderCard> FolderCard { get; set; } = null!;
    public DbSet<Deck> Deck { get; set; } = null!;
    public DbSet<CardMetadata> CardMetadata { get; set; } = null!;
    public DbSet<User> User { get; set; } = null!;

    // Seeding
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //ToDo maybe abstract annotations to use different dbms

        modelBuilder.Entity<Collection>(builder =>
        {
            builder.HasKey(c => c.Id)
                   .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            builder.HasMany(x => x.Folders)
                   .WithOne()
                   .HasForeignKey(x => x.CollectionId)
                   .HasConstraintName("FK_Collection");

            builder.ToTable("Collection");
        });

        modelBuilder.Entity<Folder>(builder =>
        {
            builder.HasKey(f => f.Id)
                   .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            builder.HasMany(x => x.Cards)
                   .WithOne()
                   .HasForeignKey(x => x.FolderId)
                   .HasConstraintName("FK_Folder");

            builder.ToTable("Folder");
        });

        modelBuilder.Entity<FolderCard>(builder =>
        {
            builder.HasKey(c => c.Id)
                   .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            builder.HasMany(x => x.Decks)
                   .WithMany(x => x.Cards)
                   .UsingEntity(j => j.ToTable("DeckCards"));

            builder.ToTable("FolderCard");
        });

        modelBuilder.Entity<Deck>(builder =>
        {
            builder.HasKey(d => d.Id)
                   .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            builder.HasMany(x => x.Cards)
                   .WithMany(x => x.Decks)
                   .UsingEntity(j => j.ToTable("DeckCards"));

            builder.ToTable("Deck");
        });

        modelBuilder.Entity<CardMetadata>(builder =>
        {
            builder.HasKey(c => c.Id)
                   .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            builder.ToTable("CardMetadata");
        });

        //modelBuilder.Entity<Collection>().HasData(
        //    new Collection
        //    {
        //        Id = 1,
        //        Name = "Default Collection",
        //        Description = "This is an automatically created default collection",
        //        Folders = new List<Folder>()
        //    }
        //);
        //modelBuilder.Entity<Folder>().HasData(
        //    new Folder
        //    {
        //        Id = 1,
        //        Name = "Default Folder",
        //        CollectionId = 1,
        //        Description = "A default folder",
        //        Cards = new List<Card>()
        //    }
        //);
        //modelBuilder.Entity<Card>().HasData(
        //    new Card()
        //    {
        //        Id = 1,
        //        FolderId = 1,
        //        Name = "Backup Agent",
        //        MetadataID = "2a46af75-3880-4141-b26e-19834d67e7a8",
        //        MetadataCardId = null,
        //        Quantity = 2
        //    }
        //);
    }
}