using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Migrations
{
    /// <inheritdoc />
    public partial class AjusteNosScheemas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "empreendimento");

            migrationBuilder.CreateTable(
                name: "Endereos",
                schema: "empreendimento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Cep = table.Column<string>(type: "text", nullable: true),
                    Logradouro = table.Column<string>(type: "text", nullable: true),
                    Complemento = table.Column<string>(type: "text", nullable: true),
                    Bairro = table.Column<string>(type: "text", nullable: true),
                    Cidade = table.Column<string>(type: "text", nullable: true),
                    Uf = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstalacaoCondominios",
                schema: "empreendimento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstalacaoCondominios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unidades",
                schema: "empreendimento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: true),
                    Dormitorios = table.Column<int>(type: "integer", nullable: false),
                    Suites = table.Column<int>(type: "integer", nullable: false),
                    Banheiros = table.Column<int>(type: "integer", nullable: false),
                    Vagas = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empreendimentos",
                schema: "empreendimento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: true),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    AreaConstruidaMin = table.Column<int>(type: "integer", nullable: false),
                    AreaConstruidaMax = table.Column<int>(type: "integer", nullable: false),
                    DormitoriosMin = table.Column<int>(type: "integer", nullable: false),
                    DormitoriosMax = table.Column<int>(type: "integer", nullable: false),
                    BanheirosMin = table.Column<int>(type: "integer", nullable: false),
                    BanheirosMax = table.Column<int>(type: "integer", nullable: false),
                    EnderecoId = table.Column<Guid>(type: "uuid", nullable: true),
                    CriadoPor = table.Column<Guid>(type: "uuid", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AtualizadoPor = table.Column<Guid>(type: "uuid", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empreendimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empreendimentos_Endereos_EnderecoId",
                        column: x => x.EnderecoId,
                        principalSchema: "empreendimento",
                        principalTable: "Endereos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Plantas",
                schema: "empreendimento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TipoDaPlanta = table.Column<int>(type: "integer", nullable: true),
                    Nomedoarquivo = table.Column<string>(type: "text", nullable: true),
                    Caminho = table.Column<string>(type: "text", nullable: true),
                    Extensao = table.Column<string>(type: "text", nullable: true),
                    TamanhoDoArquivo = table.Column<long>(type: "bigint", nullable: false),
                    UnidadeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plantas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plantas_Unidades_UnidadeId",
                        column: x => x.UnidadeId,
                        principalSchema: "empreendimento",
                        principalTable: "Unidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Arquivos",
                schema: "empreendimento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nomedoarquivo = table.Column<string>(type: "text", nullable: true),
                    Caminho = table.Column<string>(type: "text", nullable: true),
                    Extensao = table.Column<string>(type: "text", nullable: true),
                    TamanhoDoArquivo = table.Column<long>(type: "bigint", nullable: false),
                    EmpreendimentoId = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "Condominios",
                schema: "empreendimento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpreendimentoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condominios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Condominios_Empreendimentos_EmpreendimentoId",
                        column: x => x.EmpreendimentoId,
                        principalSchema: "empreendimento",
                        principalTable: "Empreendimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Imagens",
                schema: "empreendimento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nomedoarquivo = table.Column<string>(type: "text", nullable: true),
                    Caminho = table.Column<string>(type: "text", nullable: true),
                    Extensao = table.Column<string>(type: "text", nullable: true),
                    Tamanho = table.Column<long>(type: "bigint", nullable: false),
                    EmpreendimentoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Imagens_Empreendimentos_EmpreendimentoId",
                        column: x => x.EmpreendimentoId,
                        principalSchema: "empreendimento",
                        principalTable: "Empreendimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CondominioInstalacaoCondominio",
                schema: "empreendimento",
                columns: table => new
                {
                    CondominiosId = table.Column<Guid>(type: "uuid", nullable: false),
                    InstalacoesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CondominioInstalacaoCondominio", x => new { x.CondominiosId, x.InstalacoesId });
                    table.ForeignKey(
                        name: "FK_CondominioInstalacaoCondominio_Condominios_CondominiosId",
                        column: x => x.CondominiosId,
                        principalSchema: "empreendimento",
                        principalTable: "Condominios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CondominioInstalacaoCondominio_InstalacaoCondominios_Instal~",
                        column: x => x.InstalacoesId,
                        principalSchema: "empreendimento",
                        principalTable: "InstalacaoCondominios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Arquivos_EmpreendimentoId",
                schema: "empreendimento",
                table: "Arquivos",
                column: "EmpreendimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_CondominioInstalacaoCondominio_InstalacoesId",
                schema: "empreendimento",
                table: "CondominioInstalacaoCondominio",
                column: "InstalacoesId");

            migrationBuilder.CreateIndex(
                name: "IX_Condominios_EmpreendimentoId",
                schema: "empreendimento",
                table: "Condominios",
                column: "EmpreendimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Empreendimentos_EnderecoId",
                schema: "empreendimento",
                table: "Empreendimentos",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Imagens_EmpreendimentoId",
                schema: "empreendimento",
                table: "Imagens",
                column: "EmpreendimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Plantas_UnidadeId",
                schema: "empreendimento",
                table: "Plantas",
                column: "UnidadeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arquivos",
                schema: "empreendimento");

            migrationBuilder.DropTable(
                name: "CondominioInstalacaoCondominio",
                schema: "empreendimento");

            migrationBuilder.DropTable(
                name: "Imagens",
                schema: "empreendimento");

            migrationBuilder.DropTable(
                name: "Plantas",
                schema: "empreendimento");

            migrationBuilder.DropTable(
                name: "Condominios",
                schema: "empreendimento");

            migrationBuilder.DropTable(
                name: "InstalacaoCondominios",
                schema: "empreendimento");

            migrationBuilder.DropTable(
                name: "Unidades",
                schema: "empreendimento");

            migrationBuilder.DropTable(
                name: "Empreendimentos",
                schema: "empreendimento");

            migrationBuilder.DropTable(
                name: "Endereos",
                schema: "empreendimento");
        }
    }
}
