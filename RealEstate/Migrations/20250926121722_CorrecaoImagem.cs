using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoImagem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arquivos",
                schema: "empreendimento");

            migrationBuilder.DropTable(
                name: "ImagemVersao");

            migrationBuilder.AlterColumn<string>(
                name: "NomeArquivo",
                schema: "empreendimento",
                table: "Imagens",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Extensao",
                schema: "empreendimento",
                table: "Imagens",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Caminho",
                schema: "empreendimento",
                table: "Imagens",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "EmpreendimentoId1",
                schema: "empreendimento",
                table: "Imagens",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EntidadeId",
                schema: "empreendimento",
                table: "Imagens",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoEntidade",
                schema: "empreendimento",
                table: "Imagens",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Imagens_EmpreendimentoId1",
                schema: "empreendimento",
                table: "Imagens",
                column: "EmpreendimentoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Imagens_Empreendimentos_EmpreendimentoId1",
                schema: "empreendimento",
                table: "Imagens",
                column: "EmpreendimentoId1",
                principalSchema: "empreendimento",
                principalTable: "Empreendimentos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Imagens_Empreendimentos_EmpreendimentoId1",
                schema: "empreendimento",
                table: "Imagens");

            migrationBuilder.DropIndex(
                name: "IX_Imagens_EmpreendimentoId1",
                schema: "empreendimento",
                table: "Imagens");

            migrationBuilder.DropColumn(
                name: "EmpreendimentoId1",
                schema: "empreendimento",
                table: "Imagens");

            migrationBuilder.DropColumn(
                name: "EntidadeId",
                schema: "empreendimento",
                table: "Imagens");

            migrationBuilder.DropColumn(
                name: "TipoEntidade",
                schema: "empreendimento",
                table: "Imagens");

            migrationBuilder.AlterColumn<string>(
                name: "NomeArquivo",
                schema: "empreendimento",
                table: "Imagens",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Extensao",
                schema: "empreendimento",
                table: "Imagens",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Caminho",
                schema: "empreendimento",
                table: "Imagens",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Arquivos",
                schema: "empreendimento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpreendimentoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Caminho = table.Column<string>(type: "text", nullable: true),
                    Extensao = table.Column<string>(type: "text", nullable: true),
                    Nomedoarquivo = table.Column<string>(type: "text", nullable: true),
                    TamanhoDoArquivo = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arquivos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arquivos_Empreendimentos_EmpreendimentoId",
                        column: x => x.EmpreendimentoId,
                        principalSchema: "empreendimento",
                        principalTable: "Empreendimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImagemVersao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Caminho = table.Column<string>(type: "text", nullable: false),
                    Extensao = table.Column<string>(type: "text", nullable: false),
                    ImagemId = table.Column<Guid>(type: "uuid", nullable: true),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Tamanho = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagemVersao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagemVersao_Imagens_ImagemId",
                        column: x => x.ImagemId,
                        principalSchema: "empreendimento",
                        principalTable: "Imagens",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Arquivos_EmpreendimentoId",
                schema: "empreendimento",
                table: "Arquivos",
                column: "EmpreendimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagemVersao_ImagemId",
                table: "ImagemVersao",
                column: "ImagemId");
        }
    }
}
