using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bach_bash.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "app");

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
                name: "Bashes",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bashes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bashes_Bashers_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "app",
                        principalTable: "Bashers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BashMembers",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BasherId1 = table.Column<Guid>(type: "uuid", nullable: false),
                    BashId = table.Column<Guid>(type: "uuid", nullable: false),
                    BasherId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BashMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BashMembers_Bashers_BasherId1",
                        column: x => x.BasherId1,
                        principalSchema: "app",
                        principalTable: "Bashers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BashMembers_Bashes_BasherId",
                        column: x => x.BasherId,
                        principalSchema: "app",
                        principalTable: "Bashes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Challenges",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Points = table.Column<int>(type: "integer", nullable: false),
                    BashId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Challenges_Bashes_BashId",
                        column: x => x.BashId,
                        principalSchema: "app",
                        principalTable: "Bashes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Submissions",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChallengeId = table.Column<Guid>(type: "uuid", nullable: false),
                    BasherId = table.Column<Guid>(type: "uuid", nullable: false),
                    Place = table.Column<short>(type: "smallint", nullable: false),
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
                name: "IX_Bashes_Id",
                schema: "app",
                table: "Bashes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Bashes_OwnerId",
                schema: "app",
                table: "Bashes",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_BashMembers_BasherId",
                schema: "app",
                table: "BashMembers",
                column: "BasherId");

            migrationBuilder.CreateIndex(
                name: "IX_BashMembers_BasherId1",
                schema: "app",
                table: "BashMembers",
                column: "BasherId1");

            migrationBuilder.CreateIndex(
                name: "IX_BashMembers_BashId",
                schema: "app",
                table: "BashMembers",
                column: "BashId");

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_BashId",
                schema: "app",
                table: "Challenges",
                column: "BashId");

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
                name: "BashMembers",
                schema: "app");

            migrationBuilder.DropTable(
                name: "Submissions",
                schema: "app");

            migrationBuilder.DropTable(
                name: "Challenges",
                schema: "app");

            migrationBuilder.DropTable(
                name: "Bashes",
                schema: "app");

            migrationBuilder.DropTable(
                name: "Bashers",
                schema: "app");
        }
    }
}
