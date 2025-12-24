using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Imobi.MVC.Migrations
{
    /// <inheritdoc />
    public partial class EmailSecundario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailSecundario",
                schema: "auth",
                table: "Usuarios",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailSecundario",
                schema: "auth",
                table: "Usuarios");
        }
    }
}
