using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Migrations
{
    /// <inheritdoc />
    public partial class Ajuste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empreendimentos_Endereos_EnderecoId",
                schema: "empreendimento",
                table: "Empreendimentos");

            migrationBuilder.DropIndex(
                name: "IX_Empreendimentos_EnderecoId",
                schema: "empreendimento",
                table: "Empreendimentos");

            migrationBuilder.DropColumn(
                name: "EnderecoId",
                schema: "empreendimento",
                table: "Empreendimentos");

            migrationBuilder.AddColumn<Guid>(
                name: "EmpreendimentoId",
                schema: "empreendimento",
                table: "Endereos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                schema: "empreendimento",
                table: "Endereos",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                schema: "empreendimento",
                table: "Endereos",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "auth",
                table: "AspNetUserTokens",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                schema: "auth",
                table: "AspNetUserTokens",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                schema: "auth",
                table: "AspNetUserLogins",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                schema: "auth",
                table: "AspNetUserLogins",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Endereos_EmpreendimentoId",
                schema: "empreendimento",
                table: "Endereos",
                column: "EmpreendimentoId",
                unique: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereos_Empreendimentos_EmpreendimentoId",
                schema: "empreendimento",
                table: "Endereos");

            migrationBuilder.DropIndex(
                name: "IX_Endereos_EmpreendimentoId",
                schema: "empreendimento",
                table: "Endereos");

            migrationBuilder.DropColumn(
                name: "EmpreendimentoId",
                schema: "empreendimento",
                table: "Endereos");

            migrationBuilder.DropColumn(
                name: "Latitude",
                schema: "empreendimento",
                table: "Endereos");

            migrationBuilder.DropColumn(
                name: "Longitude",
                schema: "empreendimento",
                table: "Endereos");

            migrationBuilder.AddColumn<Guid>(
                name: "EnderecoId",
                schema: "empreendimento",
                table: "Empreendimentos",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "auth",
                table: "AspNetUserTokens",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                schema: "auth",
                table: "AspNetUserTokens",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                schema: "auth",
                table: "AspNetUserLogins",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                schema: "auth",
                table: "AspNetUserLogins",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateIndex(
                name: "IX_Empreendimentos_EnderecoId",
                schema: "empreendimento",
                table: "Empreendimentos",
                column: "EnderecoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empreendimentos_Endereos_EnderecoId",
                schema: "empreendimento",
                table: "Empreendimentos",
                column: "EnderecoId",
                principalSchema: "empreendimento",
                principalTable: "Endereos",
                principalColumn: "Id");
        }
    }
}
