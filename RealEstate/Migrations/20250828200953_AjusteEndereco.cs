using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Migrations
{
    /// <inheritdoc />
    public partial class AjusteEndereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereos_Empreendimentos_EmpreendimentoId",
                schema: "empreendimento",
                table: "Endereos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Endereos",
                schema: "empreendimento",
                table: "Endereos");

            migrationBuilder.RenameTable(
                name: "Endereos",
                schema: "empreendimento",
                newName: "Enderecos",
                newSchema: "empreendimento");

            migrationBuilder.RenameIndex(
                name: "IX_Endereos_EmpreendimentoId",
                schema: "empreendimento",
                table: "Enderecos",
                newName: "IX_Enderecos_EmpreendimentoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enderecos",
                schema: "empreendimento",
                table: "Enderecos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Empreendimentos_EmpreendimentoId",
                schema: "empreendimento",
                table: "Enderecos",
                column: "EmpreendimentoId",
                principalSchema: "empreendimento",
                principalTable: "Empreendimentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Empreendimentos_EmpreendimentoId",
                schema: "empreendimento",
                table: "Enderecos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enderecos",
                schema: "empreendimento",
                table: "Enderecos");

            migrationBuilder.RenameTable(
                name: "Enderecos",
                schema: "empreendimento",
                newName: "Endereos",
                newSchema: "empreendimento");

            migrationBuilder.RenameIndex(
                name: "IX_Enderecos_EmpreendimentoId",
                schema: "empreendimento",
                table: "Endereos",
                newName: "IX_Endereos_EmpreendimentoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Endereos",
                schema: "empreendimento",
                table: "Endereos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Endereos_Empreendimentos_EmpreendimentoId",
                schema: "empreendimento",
                table: "Endereos",
                column: "EmpreendimentoId",
                principalSchema: "empreendimento",
                principalTable: "Empreendimentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
