﻿// <auto-generated />
using System;
using Cegeka.Guild.Pokeverse.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    [DbContext(typeof(PokemonsContext))]
    [Migration("20200610191711_RemovedBattleReferences")]
    partial class RemovedBattleReferences
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Cegeka.Guild.Pokeverse.Domain.Ability", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Damage")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PokemonDefinitionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RequiredLevel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PokemonDefinitionId");

                    b.ToTable("Ability");
                });

            modelBuilder.Entity("Cegeka.Guild.Pokeverse.Domain.Pokemon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DefinitionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("TrainerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DefinitionId");

                    b.HasIndex("TrainerId");

                    b.ToTable("Pokemon");
                });

            modelBuilder.Entity("Cegeka.Guild.Pokeverse.Domain.PokemonDefinition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PokemonDefinitions");
                });

            modelBuilder.Entity("Cegeka.Guild.Pokeverse.Domain.Trainer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Trainers");
                });

            modelBuilder.Entity("Cegeka.Guild.Pokeverse.Domain.Ability", b =>
                {
                    b.HasOne("Cegeka.Guild.Pokeverse.Domain.PokemonDefinition", null)
                        .WithMany("Abilities")
                        .HasForeignKey("PokemonDefinitionId");
                });

            modelBuilder.Entity("Cegeka.Guild.Pokeverse.Domain.Pokemon", b =>
                {
                    b.HasOne("Cegeka.Guild.Pokeverse.Domain.PokemonDefinition", "Definition")
                        .WithMany()
                        .HasForeignKey("DefinitionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Cegeka.Guild.Pokeverse.Domain.Trainer", null)
                        .WithMany("Pokemons")
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Cegeka.Guild.Pokeverse.Domain.PokemonLevel", "Level", b1 =>
                        {
                            b1.Property<Guid>("PokemonId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Current")
                                .HasColumnType("int");

                            b1.Property<int>("Experience")
                                .HasColumnType("int");

                            b1.HasKey("PokemonId");

                            b1.ToTable("Pokemon");

                            b1.WithOwner()
                                .HasForeignKey("PokemonId");
                        });

                    b.OwnsOne("Cegeka.Guild.Pokeverse.Domain.PokemonStats", "Stats", b1 =>
                        {
                            b1.Property<Guid>("PokemonId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("CriticalStrikeChancePoints")
                                .HasColumnType("int");

                            b1.Property<int>("DamagePoints")
                                .HasColumnType("int");

                            b1.Property<int>("HealthPoints")
                                .HasColumnType("int");

                            b1.HasKey("PokemonId");

                            b1.ToTable("Pokemon");

                            b1.WithOwner()
                                .HasForeignKey("PokemonId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
