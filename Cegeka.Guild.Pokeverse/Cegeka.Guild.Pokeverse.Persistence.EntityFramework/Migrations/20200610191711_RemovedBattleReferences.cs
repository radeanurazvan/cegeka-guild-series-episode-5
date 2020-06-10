using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    public partial class RemovedBattleReferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonBattle");

            migrationBuilder.DropTable(
                name: "Battles");

            migrationBuilder.DropTable(
                name: "PokemonInFight");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PokemonInFight",
                columns: table => new
                {
                    PokemonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Health = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonInFight", x => x.PokemonId);
                    table.ForeignKey(
                        name: "FK_PokemonInFight_Pokemon_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemon",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Battles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivePlayer = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttackerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DefenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinishedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LoserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WinnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Battles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Battles_PokemonInFight_AttackerId",
                        column: x => x.AttackerId,
                        principalTable: "PokemonInFight",
                        principalColumn: "PokemonId");
                    table.ForeignKey(
                        name: "FK_Battles_PokemonInFight_DefenderId",
                        column: x => x.DefenderId,
                        principalTable: "PokemonInFight",
                        principalColumn: "PokemonId");
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
                name: "PokemonBattle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BattleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    PokemonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonBattle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonBattle_Battles_BattleId",
                        column: x => x.BattleId,
                        principalTable: "Battles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonBattle_Pokemon_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Battles_AttackerId",
                table: "Battles",
                column: "AttackerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Battles_DefenderId",
                table: "Battles",
                column: "DefenderId",
                unique: true);

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
                name: "IX_PokemonBattle_BattleId",
                table: "PokemonBattle",
                column: "BattleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonBattle_PokemonId",
                table: "PokemonBattle",
                column: "PokemonId");
        }
    }
}
