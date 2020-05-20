using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    public partial class ChangedBattleForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Battles_PokemonInFight_AttackerId",
                table: "Battles");

            migrationBuilder.DropForeignKey(
                name: "FK_Battles_PokemonInFight_DefenderId",
                table: "Battles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PokemonInFight",
                table: "PokemonInFight");

            migrationBuilder.DropIndex(
                name: "IX_PokemonInFight_PokemonId",
                table: "PokemonInFight");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PokemonInFight");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PokemonInFight",
                table: "PokemonInFight",
                column: "PokemonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Battles_PokemonInFight_AttackerId",
                table: "Battles",
                column: "AttackerId",
                principalTable: "PokemonInFight",
                principalColumn: "PokemonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Battles_PokemonInFight_DefenderId",
                table: "Battles",
                column: "DefenderId",
                principalTable: "PokemonInFight",
                principalColumn: "PokemonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Battles_PokemonInFight_AttackerId",
                table: "Battles");

            migrationBuilder.DropForeignKey(
                name: "FK_Battles_PokemonInFight_DefenderId",
                table: "Battles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PokemonInFight",
                table: "PokemonInFight");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "PokemonInFight",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PokemonInFight",
                table: "PokemonInFight",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonInFight_PokemonId",
                table: "PokemonInFight",
                column: "PokemonId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Battles_PokemonInFight_AttackerId",
                table: "Battles",
                column: "AttackerId",
                principalTable: "PokemonInFight",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Battles_PokemonInFight_DefenderId",
                table: "Battles",
                column: "DefenderId",
                principalTable: "PokemonInFight",
                principalColumn: "Id");
        }
    }
}
