using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bach_bash.Migrations
{
    /// <inheritdoc />
    public partial class ModificationToCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                schema: "app",
                table: "Bashes",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "Bashers",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bashers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Submissions",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChallengeId = table.Column<Guid>(type: "uuid", nullable: false),
                    BasherId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Submissions_Bashers_BasherId",
                        column: x => x.BasherId,
                        principalSchema: "app",
                        principalTable: "Bashers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Submissions_Challenges_ChallengeId",
                        column: x => x.ChallengeId,
                        principalSchema: "app",
                        principalTable: "Challenges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bashers_Id",
                schema: "app",
                table: "Bashers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_BasherId",
                schema: "app",
                table: "Submissions",
                column: "BasherId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_ChallengeId",
                schema: "app",
                table: "Submissions",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_Id",
                schema: "app",
                table: "Submissions",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Submissions",
                schema: "app");

            migrationBuilder.DropTable(
                name: "Bashers",
                schema: "app");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                schema: "app",
                table: "Bashes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }
    }
}
