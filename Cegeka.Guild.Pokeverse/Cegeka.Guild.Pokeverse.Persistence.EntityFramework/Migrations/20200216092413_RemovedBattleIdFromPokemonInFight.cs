using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    public partial class RemovedBattleIdFromPokemonInFight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BattleId",
                table: "PokemonInFight");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BattleId",
                table: "PokemonInFight",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
