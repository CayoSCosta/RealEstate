using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Imobi.Migrations
{
    /// <inheritdoc />
    public partial class AddUnidadeId2Arquivo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arquivos_Unidades_UnidadeId",
                table: "Arquivos");

            migrationBuilder.AlterColumn<int>(
                name: "UnidadeId",
                table: "Arquivos",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Arquivos_Unidades_UnidadeId",
                table: "Arquivos",
                column: "UnidadeId",
                principalTable: "Unidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arquivos_Unidades_UnidadeId",
                table: "Arquivos");

            migrationBuilder.AlterColumn<int>(
                name: "UnidadeId",
                table: "Arquivos",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Arquivos_Unidades_UnidadeId",
                table: "Arquivos",
                column: "UnidadeId",
                principalTable: "Unidades",
                principalColumn: "Id");
        }
    }
}
