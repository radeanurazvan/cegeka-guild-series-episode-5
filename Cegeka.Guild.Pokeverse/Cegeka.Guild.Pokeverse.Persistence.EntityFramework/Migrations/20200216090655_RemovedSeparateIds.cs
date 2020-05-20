using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    public partial class RemovedSeparateIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonInFight_Battles_AttackBattleId",
                table: "PokemonInFight");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonInFight_Battles_DefendBattleId",
                table: "PokemonInFight");

            migrationBuilder.DropIndex(
                name: "IX_PokemonInFight_AttackBattleId",
                table: "PokemonInFight");

            migrationBuilder.DropIndex(
                name: "IX_PokemonInFight_DefendBattleId",
                table: "PokemonInFight");

            migrationBuilder.DropColumn(
                name: "AttackBattleId",
                table: "PokemonInFight");

            migrationBuilder.DropColumn(
                name: "DefendBattleId",
                table: "PokemonInFight");

            migrationBuilder.AddColumn<Guid>(
                name: "BattleId",
                table: "PokemonInFight",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Battles_PokemonInFight_AttackerId",
                table: "Battles");

            migrationBuilder.DropForeignKey(
                name: "FK_Battles_PokemonInFight_DefenderId",
                table: "Battles");

            migrationBuilder.DropIndex(
                name: "IX_Battles_AttackerId",
                table: "Battles");

            migrationBuilder.DropIndex(
                name: "IX_Battles_DefenderId",
                table: "Battles");

            migrationBuilder.DropColumn(
                name: "BattleId",
                table: "PokemonInFight");

            migrationBuilder.AddColumn<Guid>(
                name: "AttackBattleId",
                table: "PokemonInFight",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DefendBattleId",
                table: "PokemonInFight",
                type: "uniqueidentifier",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonInFight_Battles_AttackBattleId",
                table: "PokemonInFight",
                column: "AttackBattleId",
                principalTable: "Battles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonInFight_Battles_DefendBattleId",
                table: "PokemonInFight",
                column: "DefendBattleId",
                principalTable: "Battles",
                principalColumn: "Id");
        }
    }
}
