using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Migrations
{
    /// <inheritdoc />
    public partial class Ajuste3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_CondominioInstalacaoCondominio_InstalacoesCondominios_Insta~",
            //    schema: "empreendimento",
            //    table: "CondominioInstalacaoCondominio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstalacoesCondominios",
                schema: "empreendimento",
                table: "InstalacoesCondominios");

            migrationBuilder.RenameTable(
                name: "InstalacoesCondominios",
                schema: "empreendimento",
                newName: "InstalacaoCondominios",
                newSchema: "empreendimento");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstalacaoCondominios",
                schema: "empreendimento",
                table: "InstalacaoCondominios",
                column: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CondominioInstalacaoCondominio_InstalacaoCondominios_Instal~",
            //    schema: "empreendimento",
            //    table: "CondominioInstalacaoCondominio",
            //    column: "InstalacoesId",
            //    principalSchema: "empreendimento",
            //    principalTable: "InstalacaoCondominios",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_CondominioInstalacaoCondominio_InstalacaoCondominios_Instal~",
            //    schema: "empreendimento",
            //    table: "CondominioInstalacaoCondominio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstalacaoCondominios",
                schema: "empreendimento",
                table: "InstalacaoCondominios");

            migrationBuilder.RenameTable(
                name: "InstalacaoCondominios",
                schema: "empreendimento",
                newName: "InstalacoesCondominios",
                newSchema: "empreendimento");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstalacoesCondominios",
                schema: "empreendimento",
                table: "InstalacoesCondominios",
                column: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CondominioInstalacaoCondominio_InstalacoesCondominios_Insta~",
            //    schema: "empreendimento",
            //    table: "CondominioInstalacaoCondominio",
            //    column: "InstalacoesId",
            //    principalSchema: "empreendimento",
            //    principalTable: "InstalacoesCondominios",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
