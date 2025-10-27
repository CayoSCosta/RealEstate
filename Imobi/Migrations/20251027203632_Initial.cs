using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Imobi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cep = table.Column<string>(type: "text", nullable: true),
                    Logradouro = table.Column<string>(type: "text", nullable: true),
                    Numero = table.Column<string>(type: "text", nullable: true),
                    Complemento = table.Column<string>(type: "text", nullable: true),
                    Bairro = table.Column<string>(type: "text", nullable: true),
                    Cidade = table.Column<string>(type: "text", nullable: true),
                    Uf = table.Column<string>(type: "text", nullable: true),
                    Latitude = table.Column<double>(type: "double precision", nullable: true),
                    Longitude = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstalacaoCondominios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Recursos = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstalacaoCondominios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empreendimentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: true),
                    Sobre = table.Column<string>(type: "text", nullable: true),
                    AreaConstruida = table.Column<string>(type: "text", nullable: true),
                    Estagio = table.Column<string>(type: "text", nullable: true),
                    BanheirosTotal = table.Column<string>(type: "text", nullable: true),
                    DormitoriosTotal = table.Column<string>(type: "text", nullable: true),
                    SuitesTotal = table.Column<string>(type: "text", nullable: true),
                    VagasTotal = table.Column<string>(type: "text", nullable: true),
                    UnidadeId = table.Column<int>(type: "integer", nullable: false),
                    EnderecoId = table.Column<int>(type: "integer", nullable: false),
                    ArquivoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empreendimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empreendimentos_Enderecos_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Enderecos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Unidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: true),
                    Dormitorios = table.Column<int>(type: "integer", nullable: false),
                    Suites = table.Column<int>(type: "integer", nullable: false),
                    Banheiros = table.Column<int>(type: "integer", nullable: false),
                    Vagas = table.Column<int>(type: "integer", nullable: false),
                    AreaConstruida = table.Column<int>(type: "integer", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric", nullable: true),
                    EmpreendimentoId = table.Column<int>(type: "integer", nullable: false),
                    ArquivoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Unidades_Empreendimentos_EmpreendimentoId",
                        column: x => x.EmpreendimentoId,
                        principalTable: "Empreendimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Arquivos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomeArquivo = table.Column<string>(type: "text", nullable: true),
                    Caminho = table.Column<string>(type: "text", nullable: true),
                    Extensao = table.Column<string>(type: "text", nullable: true),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    EmpreendimentoId = table.Column<int>(type: "integer", nullable: true),
                    UnidadeId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arquivos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arquivos_Empreendimentos_EmpreendimentoId",
                        column: x => x.EmpreendimentoId,
                        principalTable: "Empreendimentos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Arquivos_Unidades_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidades",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Condominios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InstalacoesId = table.Column<int>(type: "integer", nullable: false),
                    EmpreendimentoId1 = table.Column<int>(type: "integer", nullable: true),
                    EmpreendimentoId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArquivoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condominios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Condominios_Arquivos_ArquivoId",
                        column: x => x.ArquivoId,
                        principalTable: "Arquivos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Condominios_Empreendimentos_EmpreendimentoId1",
                        column: x => x.EmpreendimentoId1,
                        principalTable: "Empreendimentos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Condominios_InstalacaoCondominios_InstalacoesId",
                        column: x => x.InstalacoesId,
                        principalTable: "InstalacaoCondominios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Arquivos_EmpreendimentoId",
                table: "Arquivos",
                column: "EmpreendimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Arquivos_UnidadeId",
                table: "Arquivos",
                column: "UnidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Condominios_ArquivoId",
                table: "Condominios",
                column: "ArquivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Condominios_EmpreendimentoId1",
                table: "Condominios",
                column: "EmpreendimentoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Condominios_InstalacoesId",
                table: "Condominios",
                column: "InstalacoesId");

            migrationBuilder.CreateIndex(
                name: "IX_Empreendimentos_EnderecoId",
                table: "Empreendimentos",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Unidades_EmpreendimentoId",
                table: "Unidades",
                column: "EmpreendimentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Condominios");

            migrationBuilder.DropTable(
                name: "Arquivos");

            migrationBuilder.DropTable(
                name: "InstalacaoCondominios");

            migrationBuilder.DropTable(
                name: "Unidades");

            migrationBuilder.DropTable(
                name: "Empreendimentos");

            migrationBuilder.DropTable(
                name: "Enderecos");
        }
    }
}
