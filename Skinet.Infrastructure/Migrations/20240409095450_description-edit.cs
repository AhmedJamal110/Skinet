using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skinet.Infrastructure.Migrations
{
    public partial class descriptionedit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descriprion",
                table: "Products",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "Descriprion");
        }
    }
}
