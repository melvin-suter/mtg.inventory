﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using mtg_inventory_backend.Models;

#nullable disable

namespace mtginventorybackend.Migrations
{
    [DbContext(typeof(DefaultDBContext))]
    partial class DefaultDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CardDeck", b =>
                {
                    b.Property<int>("cardsid")
                        .HasColumnType("integer");

                    b.Property<int>("decksid")
                        .HasColumnType("integer");

                    b.HasKey("cardsid", "decksid");

                    b.HasIndex("decksid");

                    b.ToTable("CardDeck");
                });

            modelBuilder.Entity("mtg_inventory_backend.Models.Card", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int>("folderId")
                        .HasColumnType("integer");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("quantity")
                        .HasColumnType("integer");

                    b.Property<string>("MetadataCardId")
                        .HasColumnType("text");

                    b.Property<string>("MetadataID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("folderId");

                    b.ToTable("Card");

                    b.HasData(
                        new
                        {
                            id = 1,
                            folderId = 1,
                            name = "Backup Agent",
                            quantity = 2,
                            MetadataID = "2a46af75-3880-4141-b26e-19834d67e7a8"
                        });
                });

            modelBuilder.Entity("mtg_inventory_backend.Models.Collection", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("description")
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Collection");

                    b.HasData(
                        new
                        {
                            id = 1,
                            description = "This is an automatically created default collection",
                            name = "Default Collection"
                        });
                });

            modelBuilder.Entity("mtg_inventory_backend.Models.Deck", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("description")
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Deck");
                });

            modelBuilder.Entity("mtg_inventory_backend.Models.Folder", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int>("collectionId")
                        .HasColumnType("integer");

                    b.Property<string>("description")
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("collectionId");

                    b.ToTable("Folder");

                    b.HasData(
                        new
                        {
                            id = 1,
                            collectionId = 1,
                            description = "A default folder",
                            name = "Default Folder"
                        });
                });

            modelBuilder.Entity("mtg_inventory_backend.Models.MetadataCard", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<string>("color_identity")
                        .HasColumnType("text");

                    b.Property<List<string>>("colors")
                        .HasColumnType("text[]");

                    b.Property<string>("imageUrl_Big")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("imageUrl_Small")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<List<string>>("keywords")
                        .HasColumnType("text[]");

                    b.Property<string>("lang")
                        .HasColumnType("text");

                    b.Property<string>("layout")
                        .HasColumnType("text");

                    b.Property<string>("mana_cost")
                        .HasColumnType("text");

                    b.Property<int?>("mana_cost_total")
                        .HasColumnType("integer");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("oracle_text")
                        .HasColumnType("text");

                    b.Property<int?>("power")
                        .HasColumnType("integer");

                    b.Property<int?>("toughness")
                        .HasColumnType("integer");

                    b.Property<string>("type")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("MetadataCard");
                });

            modelBuilder.Entity("mtg_inventory_backend.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("CardDeck", b =>
                {
                    b.HasOne("mtg_inventory_backend.Models.Card", null)
                        .WithMany()
                        .HasForeignKey("cardsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("mtg_inventory_backend.Models.Deck", null)
                        .WithMany()
                        .HasForeignKey("decksid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("mtg_inventory_backend.Models.Card", b =>
                {
                    b.HasOne("mtg_inventory_backend.Models.Folder", null)
                        .WithMany("cards")
                        .HasForeignKey("folderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("mtg_inventory_backend.Models.Folder", b =>
                {
                    b.HasOne("mtg_inventory_backend.Models.Collection", null)
                        .WithMany("folders")
                        .HasForeignKey("collectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("mtg_inventory_backend.Models.Collection", b =>
                {
                    b.Navigation("folders");
                });

            modelBuilder.Entity("mtg_inventory_backend.Models.Folder", b =>
                {
                    b.Navigation("cards");
                });
#pragma warning restore 612, 618
        }
    }
}
