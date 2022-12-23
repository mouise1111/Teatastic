using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teatastic.Migrations
{
    public partial class addMigrationTeas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeaId",
                table: "Function",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tea",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tea", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Function_Tea_TeaId",
                table: "Function");

            migrationBuilder.DropTable(
                name: "Tea");

            migrationBuilder.DropIndex(
                name: "IX_Function_TeaId",
                table: "Function");

            migrationBuilder.DropColumn(
                name: "TeaId",
                table: "Function");
        }
    }
}
