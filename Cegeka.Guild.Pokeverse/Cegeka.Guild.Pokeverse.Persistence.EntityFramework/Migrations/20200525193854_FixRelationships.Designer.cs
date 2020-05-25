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
    [Migration("20200525193854_FixRelationships")]
    partial class FixRelationships
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
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

            modelBuilder.Entity("Cegeka.Guild.Pokeverse.Domain.Battle", b =>
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

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

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

            modelBuilder.Entity("Cegeka.Guild.Pokeverse.Domain.Pokemon", b =>
                {
                    b.Property<Guid>("Id")
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

            modelBuilder.Entity("Cegeka.Guild.Pokeverse.Domain.PokemonBattle", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BattleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("PokemonId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BattleId");

                    b.HasIndex("PokemonId");

                    b.ToTable("PokemonBattle");
                });

            modelBuilder.Entity("Cegeka.Guild.Pokeverse.Domain.PokemonDefinition", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PokemonDefinitions");
                });

            modelBuilder.Entity("Cegeka.Guild.Pokeverse.Domain.PokemonInFight", b =>
                {
                    b.Property<Guid>("PokemonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Health")
                        .HasColumnType("int");

                    b.HasKey("PokemonId");

                    b.ToTable("PokemonInFight");
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

            modelBuilder.Entity("Cegeka.Guild.Pokeverse.Domain.Battle", b =>
                {
                    b.HasOne("Cegeka.Guild.Pokeverse.Domain.PokemonInFight", "Attacker")
                        .WithOne()
                        .HasForeignKey("Cegeka.Guild.Pokeverse.Domain.Battle", "AttackerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Cegeka.Guild.Pokeverse.Domain.PokemonInFight", "Defender")
                        .WithOne()
                        .HasForeignKey("Cegeka.Guild.Pokeverse.Domain.Battle", "DefenderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Cegeka.Guild.Pokeverse.Domain.Pokemon", "Loser")
                        .WithOne()
                        .HasForeignKey("Cegeka.Guild.Pokeverse.Domain.Battle", "LoserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Cegeka.Guild.Pokeverse.Domain.Pokemon", "Winner")
                        .WithOne()
                        .HasForeignKey("Cegeka.Guild.Pokeverse.Domain.Battle", "WinnerId")
                        .OnDelete(DeleteBehavior.NoAction);
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

            modelBuilder.Entity("Cegeka.Guild.Pokeverse.Domain.PokemonBattle", b =>
                {
                    b.HasOne("Cegeka.Guild.Pokeverse.Domain.Battle", "Battle")
                        .WithMany()
                        .HasForeignKey("BattleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cegeka.Guild.Pokeverse.Domain.Pokemon", null)
                        .WithMany("battles")
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Cegeka.Guild.Pokeverse.Domain.PokemonInFight", b =>
                {
                    b.HasOne("Cegeka.Guild.Pokeverse.Domain.Pokemon", "Pokemon")
                        .WithOne()
                        .HasForeignKey("Cegeka.Guild.Pokeverse.Domain.PokemonInFight", "PokemonId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
