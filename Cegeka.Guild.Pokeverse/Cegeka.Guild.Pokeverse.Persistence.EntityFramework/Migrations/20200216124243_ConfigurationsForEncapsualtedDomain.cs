using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    public partial class ConfigurationsForEncapsualtedDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentLevel",
                table: "Pokemon");

            migrationBuilder.RenameColumn(
                name: "HealthPoints",
                table: "Pokemon",
                newName: "Stats_HealthPoints");

            migrationBuilder.RenameColumn(
                name: "Experience",
                table: "Pokemon",
                newName: "Level_Experience");

            migrationBuilder.RenameColumn(
                name: "DamagePoints",
                table: "Pokemon",
                newName: "Stats_DamagePoints");

            migrationBuilder.RenameColumn(
                name: "CriticalStrikeChancePoints",
                table: "Pokemon",
                newName: "Stats_CriticalStrikeChancePoints");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Trainers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PokemonDefinitions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "Stats_HealthPoints",
                table: "Pokemon",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Level_Experience",
                table: "Pokemon",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Stats_DamagePoints",
                table: "Pokemon",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Stats_CriticalStrikeChancePoints",
                table: "Pokemon",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Pokemon",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Level_Current",
                table: "Pokemon",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Battles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Ability",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PokemonBattle",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BattleId = table.Column<Guid>(nullable: false),
                    PokemonId = table.Column<Guid>(nullable: false)
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
                name: "IX_PokemonBattle_BattleId",
                table: "PokemonBattle",
                column: "BattleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonBattle_PokemonId",
                table: "PokemonBattle",
                column: "PokemonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonBattle");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PokemonDefinitions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Pokemon");

            migrationBuilder.DropColumn(
                name: "Level_Current",
                table: "Pokemon");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Battles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Ability");

            migrationBuilder.RenameColumn(
                name: "Stats_HealthPoints",
                table: "Pokemon",
                newName: "HealthPoints");

            migrationBuilder.RenameColumn(
                name: "Stats_DamagePoints",
                table: "Pokemon",
                newName: "DamagePoints");

            migrationBuilder.RenameColumn(
                name: "Stats_CriticalStrikeChancePoints",
                table: "Pokemon",
                newName: "CriticalStrikeChancePoints");

            migrationBuilder.RenameColumn(
                name: "Level_Experience",
                table: "Pokemon",
                newName: "Experience");

            migrationBuilder.AlterColumn<int>(
                name: "HealthPoints",
                table: "Pokemon",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DamagePoints",
                table: "Pokemon",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CriticalStrikeChancePoints",
                table: "Pokemon",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Experience",
                table: "Pokemon",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentLevel",
                table: "Pokemon",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
