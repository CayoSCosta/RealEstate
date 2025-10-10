using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Imobi.Migrations
{
    /// <inheritdoc />
    public partial class Arquivo2Empreendimento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmpreendimentoId",
                table: "Arquivos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Arquivos_EmpreendimentoId",
                table: "Arquivos",
                column: "EmpreendimentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Arquivos_Empreendimentos_EmpreendimentoId",
                table: "Arquivos",
                column: "EmpreendimentoId",
                principalTable: "Empreendimentos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arquivos_Empreendimentos_EmpreendimentoId",
                table: "Arquivos");

            migrationBuilder.DropIndex(
                name: "IX_Arquivos_EmpreendimentoId",
                table: "Arquivos");

            migrationBuilder.DropColumn(
                name: "EmpreendimentoId",
                table: "Arquivos");
        }
    }
}
