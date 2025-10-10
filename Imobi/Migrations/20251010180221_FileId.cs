using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Imobi.Migrations
{
    /// <inheritdoc />
    public partial class FileId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArquivoId",
                table: "Empreendimentos",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArquivoId",
                table: "Empreendimentos");
        }
    }
}
