using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bach_bash.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bashes_Bashers_OwnerId",
                schema: "app",
                table: "Bashes");

            migrationBuilder.DropIndex(
                name: "IX_Bashes_OwnerId",
                schema: "app",
                table: "Bashes");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                schema: "app",
                table: "Bashers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Bashers_Bashes_Id",
                schema: "app",
                table: "Bashers",
                column: "Id",
                principalSchema: "app",
                principalTable: "Bashes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bashers_Bashes_Id",
                schema: "app",
                table: "Bashers");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                schema: "app",
                table: "Bashers");

            migrationBuilder.CreateIndex(
                name: "IX_Bashes_OwnerId",
                schema: "app",
                table: "Bashes",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bashes_Bashers_OwnerId",
                schema: "app",
                table: "Bashes",
                column: "OwnerId",
                principalSchema: "app",
                principalTable: "Bashers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
