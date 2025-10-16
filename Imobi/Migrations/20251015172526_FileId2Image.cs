using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Imobi.Migrations
{
    /// <inheritdoc />
    public partial class FileId2Image : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arquivos_Empreendimentos_EmpreendimentoId",
                table: "Arquivos");

            migrationBuilder.AddColumn<int>(
                name: "ArquivoId",
                table: "Unidades",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "EmpreendimentoId",
                table: "Arquivos",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnidadeId",
                table: "Arquivos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Arquivos_UnidadeId",
                table: "Arquivos",
                column: "UnidadeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Arquivos_Empreendimentos_EmpreendimentoId",
                table: "Arquivos",
                column: "EmpreendimentoId",
                principalTable: "Empreendimentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Arquivos_Unidades_UnidadeId",
                table: "Arquivos",
                column: "UnidadeId",
                principalTable: "Unidades",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arquivos_Empreendimentos_EmpreendimentoId",
                table: "Arquivos");

            migrationBuilder.DropForeignKey(
                name: "FK_Arquivos_Unidades_UnidadeId",
                table: "Arquivos");

            migrationBuilder.DropIndex(
                name: "IX_Arquivos_UnidadeId",
                table: "Arquivos");

            migrationBuilder.DropColumn(
                name: "ArquivoId",
                table: "Unidades");

            migrationBuilder.DropColumn(
                name: "UnidadeId",
                table: "Arquivos");

            migrationBuilder.AlterColumn<int>(
                name: "EmpreendimentoId",
                table: "Arquivos",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Arquivos_Empreendimentos_EmpreendimentoId",
                table: "Arquivos",
                column: "EmpreendimentoId",
                principalTable: "Empreendimentos",
                principalColumn: "Id");
        }
    }
}
