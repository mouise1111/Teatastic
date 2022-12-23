using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teatastic.Migrations
{
    public partial class newMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Function_Tea_TeaId",
                table: "Function");

            migrationBuilder.DropIndex(
                name: "IX_Function_TeaId",
                table: "Function");

            migrationBuilder.DropColumn(
                name: "TeaId",
                table: "Function");

            migrationBuilder.CreateTable(
                name: "FunctionTea",
                columns: table => new
                {
                    FunctionsId = table.Column<int>(type: "int", nullable: false),
                    TeasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionTea", x => new { x.FunctionsId, x.TeasId });
                    table.ForeignKey(
                        name: "FK_FunctionTea_Function_FunctionsId",
                        column: x => x.FunctionsId,
                        principalTable: "Function",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FunctionTea_Tea_TeasId",
                        column: x => x.TeasId,
                        principalTable: "Tea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FunctionTea_TeasId",
                table: "FunctionTea",
                column: "TeasId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FunctionTea");

            migrationBuilder.AddColumn<int>(
                name: "TeaId",
                table: "Function",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Function_TeaId",
                table: "Function",
                column: "TeaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Function_Tea_TeaId",
                table: "Function",
                column: "TeaId",
                principalTable: "Tea",
                principalColumn: "Id");
        }
    }
}
