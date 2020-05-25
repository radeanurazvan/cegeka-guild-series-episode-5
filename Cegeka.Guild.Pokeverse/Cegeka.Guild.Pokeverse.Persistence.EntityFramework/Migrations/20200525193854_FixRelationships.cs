using Microsoft.EntityFrameworkCore.Migrations;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    public partial class FixRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PokemonBattle_BattleId",
                table: "PokemonBattle");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonBattle_BattleId",
                table: "PokemonBattle",
                column: "BattleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PokemonBattle_BattleId",
                table: "PokemonBattle");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonBattle_BattleId",
                table: "PokemonBattle",
                column: "BattleId",
                unique: true);
        }
    }
}
