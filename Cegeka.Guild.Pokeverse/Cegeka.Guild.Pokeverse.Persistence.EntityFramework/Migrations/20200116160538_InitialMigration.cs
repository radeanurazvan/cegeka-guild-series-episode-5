using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PokemonDefinitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonDefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ability",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Damage = table.Column<int>(nullable: false),
                    RequiredLevel = table.Column<int>(nullable: false),
                    PokemonDefinitionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ability", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ability_PokemonDefinitions_PokemonDefinitionId",
                        column: x => x.PokemonDefinitionId,
                        principalTable: "PokemonDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pokemon",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TrainerId = table.Column<Guid>(nullable: false),
                    DefinitionId = table.Column<Guid>(nullable: false),
                    HealthPoints = table.Column<int>(nullable: false),
                    CriticalStrikeChancePoints = table.Column<int>(nullable: false),
                    DamagePoints = table.Column<int>(nullable: false),
                    CurrentLevel = table.Column<int>(nullable: false),
                    Experience = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pokemon_PokemonDefinitions_DefinitionId",
                        column: x => x.DefinitionId,
                        principalTable: "PokemonDefinitions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pokemon_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Battles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AttackerId = table.Column<Guid>(nullable: false),
                    DefenderId = table.Column<Guid>(nullable: false),
                    ActivePlayer = table.Column<Guid>(nullable: false),
                    StartedAt = table.Column<DateTime>(nullable: false),
                    FinishedAt = table.Column<DateTime>(nullable: false),
                    WinnerId = table.Column<Guid>(nullable: true),
                    LoserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Battles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Battles_Pokemon_LoserId",
                        column: x => x.LoserId,
                        principalTable: "Pokemon",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Battles_Pokemon_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Pokemon",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PokemonInFight",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PokemonId = table.Column<Guid>(nullable: false),
                    AttackBattleId = table.Column<Guid>(nullable: true),
                    DefendBattleId = table.Column<Guid>(nullable: true),
                    Health = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonInFight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonInFight_Battles_AttackBattleId",
                        column: x => x.AttackBattleId,
                        principalTable: "Battles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PokemonInFight_Battles_DefendBattleId",
                        column: x => x.DefendBattleId,
                        principalTable: "Battles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PokemonInFight_Pokemon_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemon",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ability_PokemonDefinitionId",
                table: "Ability",
                column: "PokemonDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Battles_LoserId",
                table: "Battles",
                column: "LoserId",
                unique: true,
                filter: "[LoserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Battles_WinnerId",
                table: "Battles",
                column: "WinnerId",
                unique: true,
                filter: "[WinnerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_DefinitionId",
                table: "Pokemon",
                column: "DefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemon_TrainerId",
                table: "Pokemon",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonInFight_AttackBattleId",
                table: "PokemonInFight",
                column: "AttackBattleId",
                unique: true,
                filter: "[AttackBattleId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonInFight_DefendBattleId",
                table: "PokemonInFight",
                column: "DefendBattleId",
                unique: true,
                filter: "[DefendBattleId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonInFight_PokemonId",
                table: "PokemonInFight",
                column: "PokemonId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ability");

            migrationBuilder.DropTable(
                name: "PokemonInFight");

            migrationBuilder.DropTable(
                name: "Battles");

            migrationBuilder.DropTable(
                name: "Pokemon");

            migrationBuilder.DropTable(
                name: "PokemonDefinitions");

            migrationBuilder.DropTable(
                name: "Trainers");
        }
    }
}
