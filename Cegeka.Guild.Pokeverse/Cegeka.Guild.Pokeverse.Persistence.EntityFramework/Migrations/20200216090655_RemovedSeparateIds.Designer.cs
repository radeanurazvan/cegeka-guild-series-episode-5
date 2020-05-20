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
    [Migration("20200216090655_RemovedSeparateIds")]
    partial class RemovedSeparateIds
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Cegeka.Guild.Pokeverse.Domain.Entities.Ability", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Damage")
                        .HasColumnType("int");

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

            modelBuilder.Entity("Cegeka.Guild.Pokeverse.Domain.Entities.Battle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ActivePlayer")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AttackerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DefenderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("FinishedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("LoserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("WinnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AttackerId")
                        .IsUnique();

                    b.HasIndex("DefenderId")
                        .IsUnique();

                    b.HasIndex("LoserId")
                        .IsUnique()
                        .HasFilter("[LoserId] IS NOT NULL");

                    b.HasIndex("WinnerId")
                        .IsUnique()
                        .HasFilter("[WinnerId] IS NOT NULL");

                    b.ToTable("Battles");
                });

            modelBuilder.Entity("Cegeka.Guild.Pokeverse.Domain.Entities.Pokemon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CriticalStrikeChancePoints")
                        .HasColumnType("int");

                    b.Property<int>("CurrentLevel")
                        .HasColumnType("int");

                    b.Property<int>("DamagePoints")
                        .HasColumnType("int");

                    b.Property<Guid>("DefinitionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Experience")
                        .HasColumnType("int");

                    b.Property<int>("HealthPoints")
                        .HasColumnType("int");

                    b.Property<Guid>("TrainerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DefinitionId");

                    b.HasIndex("TrainerId");

                    b.ToTable("Pokemon");
                });

            modelBuilder.Entity("Cegeka.Guild.Pokeverse.Domain.Entities.PokemonDefinition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PokemonDefinitions");
                });

            modelBuilder.Entity("Cegeka.Guild.Pokeverse.Domain.Entities.PokemonInFight", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BattleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Health")
                        .HasColumnType("int");

                    b.Property<Guid>("PokemonId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PokemonId")
                        .IsUnique();

                    b.ToTable("PokemonInFight");
                });

            modelBuilder.Entity("Cegeka.Guild.Pokeverse.Domain.Entities.Trainer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Trainers");
                });

            modelBuilder.Entity("Cegeka.Guild.Pokeverse.Domain.Entities.Ability", b =>
                {
                    b.HasOne("Cegeka.Guild.Pokeverse.Domain.Entities.PokemonDefinition", null)
                        .WithMany("Abilities")
                        .HasForeignKey("PokemonDefinitionId");
                });

            modelBuilder.Entity("Cegeka.Guild.Pokeverse.Domain.Entities.Battle", b =>
                {
                    b.HasOne("Cegeka.Guild.Pokeverse.Domain.Entities.PokemonInFight", "Attacker")
                        .WithOne()
                        .HasForeignKey("Cegeka.Guild.Pokeverse.Domain.Entities.Battle", "AttackerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Cegeka.Guild.Pokeverse.Domain.Entities.PokemonInFight", "Defender")
                        .WithOne()
                        .HasForeignKey("Cegeka.Guild.Pokeverse.Domain.Entities.Battle", "DefenderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Cegeka.Guild.Pokeverse.Domain.Entities.Pokemon", "Loser")
                        .WithOne()
                        .HasForeignKey("Cegeka.Guild.Pokeverse.Domain.Entities.Battle", "LoserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Cegeka.Guild.Pokeverse.Domain.Entities.Pokemon", "Winner")
                        .WithOne()
                        .HasForeignKey("Cegeka.Guild.Pokeverse.Domain.Entities.Battle", "WinnerId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("Cegeka.Guild.Pokeverse.Domain.Entities.Pokemon", b =>
                {
                    b.HasOne("Cegeka.Guild.Pokeverse.Domain.Entities.PokemonDefinition", "Definition")
                        .WithMany()
                        .HasForeignKey("DefinitionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Cegeka.Guild.Pokeverse.Domain.Entities.Trainer", null)
                        .WithMany("Pokemons")
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Cegeka.Guild.Pokeverse.Domain.Entities.PokemonInFight", b =>
                {
                    b.HasOne("Cegeka.Guild.Pokeverse.Domain.Entities.Pokemon", "Pokemon")
                        .WithOne()
                        .HasForeignKey("Cegeka.Guild.Pokeverse.Domain.Entities.PokemonInFight", "PokemonId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
