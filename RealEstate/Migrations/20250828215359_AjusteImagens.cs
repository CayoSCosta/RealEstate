using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Migrations
{
    /// <inheritdoc />
    public partial class AjusteImagens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "XLargeUrl",
                schema: "empreendimento",
                table: "Imagens",
                newName: "XLargeTamanho");

            migrationBuilder.RenameColumn(
                name: "ThumbUrl",
                schema: "empreendimento",
                table: "Imagens",
                newName: "XLargeExtensao");

            migrationBuilder.RenameColumn(
                name: "MediumUrl",
                schema: "empreendimento",
                table: "Imagens",
                newName: "XLargeCaminho");

            migrationBuilder.RenameColumn(
                name: "LargeUrl",
                schema: "empreendimento",
                table: "Imagens",
                newName: "ThumbTamanho");

            migrationBuilder.AddColumn<string>(
                name: "LargeCaminho",
                schema: "empreendimento",
                table: "Imagens",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LargeExtensao",
                schema: "empreendimento",
                table: "Imagens",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LargeTamanho",
                schema: "empreendimento",
                table: "Imagens",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MediumCaminho",
                schema: "empreendimento",
                table: "Imagens",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MediumExtensao",
                schema: "empreendimento",
                table: "Imagens",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MediumTamanho",
                schema: "empreendimento",
                table: "Imagens",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbCaminho",
                schema: "empreendimento",
                table: "Imagens",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbExtensao",
                schema: "empreendimento",
                table: "Imagens",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LargeCaminho",
                schema: "empreendimento",
                table: "Imagens");

            migrationBuilder.DropColumn(
                name: "LargeExtensao",
                schema: "empreendimento",
                table: "Imagens");

            migrationBuilder.DropColumn(
                name: "LargeTamanho",
                schema: "empreendimento",
                table: "Imagens");

            migrationBuilder.DropColumn(
                name: "MediumCaminho",
                schema: "empreendimento",
                table: "Imagens");

            migrationBuilder.DropColumn(
                name: "MediumExtensao",
                schema: "empreendimento",
                table: "Imagens");

            migrationBuilder.DropColumn(
                name: "MediumTamanho",
                schema: "empreendimento",
                table: "Imagens");

            migrationBuilder.DropColumn(
                name: "ThumbCaminho",
                schema: "empreendimento",
                table: "Imagens");

            migrationBuilder.DropColumn(
                name: "ThumbExtensao",
                schema: "empreendimento",
                table: "Imagens");

            migrationBuilder.RenameColumn(
                name: "XLargeTamanho",
                schema: "empreendimento",
                table: "Imagens",
                newName: "XLargeUrl");

            migrationBuilder.RenameColumn(
                name: "XLargeExtensao",
                schema: "empreendimento",
                table: "Imagens",
                newName: "ThumbUrl");

            migrationBuilder.RenameColumn(
                name: "XLargeCaminho",
                schema: "empreendimento",
                table: "Imagens",
                newName: "MediumUrl");

            migrationBuilder.RenameColumn(
                name: "ThumbTamanho",
                schema: "empreendimento",
                table: "Imagens",
                newName: "LargeUrl");
        }
    }
}
