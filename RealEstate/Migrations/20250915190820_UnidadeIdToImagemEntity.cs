using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Migrations
{
    /// <inheritdoc />
    public partial class UnidadeIdToImagemEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Imagens_Empreendimentos_EmpreendimentoId",
                schema: "empreendimento",
                table: "Imagens");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmpreendimentoId",
                schema: "empreendimento",
                table: "Imagens",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Imagens_Empreendimentos_EmpreendimentoId",
                schema: "empreendimento",
                table: "Imagens",
                column: "EmpreendimentoId",
                principalSchema: "empreendimento",
                principalTable: "Empreendimentos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Imagens_Empreendimentos_EmpreendimentoId",
                schema: "empreendimento",
                table: "Imagens");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmpreendimentoId",
                schema: "empreendimento",
                table: "Imagens",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Imagens_Empreendimentos_EmpreendimentoId",
                schema: "empreendimento",
                table: "Imagens",
                column: "EmpreendimentoId",
                principalSchema: "empreendimento",
                principalTable: "Empreendimentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
