using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Migrations
{
    /// <inheritdoc />
    public partial class VariosAjustes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tamanho",
                schema: "empreendimento",
                table: "Imagens");

            migrationBuilder.RenameColumn(
                name: "Nomedoarquivo",
                schema: "empreendimento",
                table: "Imagens",
                newName: "XLargeUrl");

            migrationBuilder.RenameColumn(
                name: "Extensao",
                schema: "empreendimento",
                table: "Imagens",
                newName: "ThumbUrl");

            migrationBuilder.RenameColumn(
                name: "Caminho",
                schema: "empreendimento",
                table: "Imagens",
                newName: "MediumUrl");

            migrationBuilder.AddColumn<string>(
                name: "LargeUrl",
                schema: "empreendimento",
                table: "Imagens",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Numero",
                schema: "empreendimento",
                table: "Endereos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "empreendimento",
                table: "Empreendimentos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SuitesMax",
                schema: "empreendimento",
                table: "Empreendimentos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SuitesMin",
                schema: "empreendimento",
                table: "Empreendimentos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VagasDeGaragemMax",
                schema: "empreendimento",
                table: "Empreendimentos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VagasDeGaragemMin",
                schema: "empreendimento",
                table: "Empreendimentos",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LargeUrl",
                schema: "empreendimento",
                table: "Imagens");

            migrationBuilder.DropColumn(
                name: "Numero",
                schema: "empreendimento",
                table: "Endereos");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "empreendimento",
                table: "Empreendimentos");

            migrationBuilder.DropColumn(
                name: "SuitesMax",
                schema: "empreendimento",
                table: "Empreendimentos");

            migrationBuilder.DropColumn(
                name: "SuitesMin",
                schema: "empreendimento",
                table: "Empreendimentos");

            migrationBuilder.DropColumn(
                name: "VagasDeGaragemMax",
                schema: "empreendimento",
                table: "Empreendimentos");

            migrationBuilder.DropColumn(
                name: "VagasDeGaragemMin",
                schema: "empreendimento",
                table: "Empreendimentos");

            migrationBuilder.RenameColumn(
                name: "XLargeUrl",
                schema: "empreendimento",
                table: "Imagens",
                newName: "Nomedoarquivo");

            migrationBuilder.RenameColumn(
                name: "ThumbUrl",
                schema: "empreendimento",
                table: "Imagens",
                newName: "Extensao");

            migrationBuilder.RenameColumn(
                name: "MediumUrl",
                schema: "empreendimento",
                table: "Imagens",
                newName: "Caminho");

            migrationBuilder.AddColumn<long>(
                name: "Tamanho",
                schema: "empreendimento",
                table: "Imagens",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
