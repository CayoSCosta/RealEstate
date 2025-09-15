using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RealEstate.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "empreendimento");

            migrationBuilder.EnsureSchema(
                name: "auth");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empreendimentos",
                schema: "empreendimento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: true),
                    Descricao = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    AreaConstruidaMin = table.Column<int>(type: "integer", nullable: false),
                    AreaConstruidaMax = table.Column<int>(type: "integer", nullable: false),
                    DormitoriosMin = table.Column<int>(type: "integer", nullable: false),
                    DormitoriosMax = table.Column<int>(type: "integer", nullable: false),
                    BanheirosMin = table.Column<int>(type: "integer", nullable: false),
                    BanheirosMax = table.Column<int>(type: "integer", nullable: false),
                    SuitesMin = table.Column<int>(type: "integer", nullable: false),
                    SuitesMax = table.Column<int>(type: "integer", nullable: false),
                    VagasDeGaragemMin = table.Column<int>(type: "integer", nullable: false),
                    VagasDeGaragemMax = table.Column<int>(type: "integer", nullable: false),
                    CriadoPor = table.Column<Guid>(type: "uuid", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AtualizadoPor = table.Column<Guid>(type: "uuid", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empreendimentos", x => x.Id);
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
                name: "AspNetRoleClaims",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "auth",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "auth",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "auth",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "auth",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "auth",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
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
                name: "Enderecos",
                schema: "empreendimento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Cep = table.Column<string>(type: "text", nullable: true),
                    Logradouro = table.Column<string>(type: "text", nullable: true),
                    Numero = table.Column<string>(type: "text", nullable: true),
                    Complemento = table.Column<string>(type: "text", nullable: true),
                    Bairro = table.Column<string>(type: "text", nullable: true),
                    Cidade = table.Column<string>(type: "text", nullable: true),
                    Uf = table.Column<string>(type: "text", nullable: true),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    EmpreendimentoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enderecos_Empreendimentos_EmpreendimentoId",
                        column: x => x.EmpreendimentoId,
                        principalSchema: "empreendimento",
                        principalTable: "Empreendimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Vagas = table.Column<int>(type: "integer", nullable: false),
                    AreaConstruida = table.Column<int>(type: "integer", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    EmpreendimentoId = table.Column<Guid>(type: "uuid", nullable: true),
                    CriadoPor = table.Column<Guid>(type: "uuid", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AtualizadoPor = table.Column<Guid>(type: "uuid", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Unidades_Empreendimentos_EmpreendimentoId",
                        column: x => x.EmpreendimentoId,
                        principalSchema: "empreendimento",
                        principalTable: "Empreendimentos",
                        principalColumn: "Id");
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

            migrationBuilder.CreateTable(
                name: "Imagens",
                schema: "empreendimento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    ThumbCaminho = table.Column<string>(type: "text", nullable: true),
                    MediumCaminho = table.Column<string>(type: "text", nullable: true),
                    LargeCaminho = table.Column<string>(type: "text", nullable: true),
                    XLargeCaminho = table.Column<string>(type: "text", nullable: true),
                    ThumbTamanho = table.Column<string>(type: "text", nullable: true),
                    MediumTamanho = table.Column<string>(type: "text", nullable: true),
                    LargeTamanho = table.Column<string>(type: "text", nullable: true),
                    XLargeTamanho = table.Column<string>(type: "text", nullable: true),
                    ThumbExtensao = table.Column<string>(type: "text", nullable: true),
                    MediumExtensao = table.Column<string>(type: "text", nullable: true),
                    LargeExtensao = table.Column<string>(type: "text", nullable: true),
                    XLargeExtensao = table.Column<string>(type: "text", nullable: true),
                    EmpreendimentoId = table.Column<Guid>(type: "uuid", nullable: false),
                    UnidadeId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Imagens_Unidades_UnidadeId",
                        column: x => x.UnidadeId,
                        principalSchema: "empreendimento",
                        principalTable: "Unidades",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Arquivos_EmpreendimentoId",
                schema: "empreendimento",
                table: "Arquivos",
                column: "EmpreendimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "auth",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "auth",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "auth",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "auth",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "auth",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

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
                name: "IX_Enderecos_EmpreendimentoId",
                schema: "empreendimento",
                table: "Enderecos",
                column: "EmpreendimentoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Imagens_EmpreendimentoId",
                schema: "empreendimento",
                table: "Imagens",
                column: "EmpreendimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Imagens_UnidadeId",
                schema: "empreendimento",
                table: "Imagens",
                column: "UnidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Unidades_EmpreendimentoId",
                schema: "empreendimento",
                table: "Unidades",
                column: "EmpreendimentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arquivos",
                schema: "empreendimento");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "CondominioInstalacaoCondominio",
                schema: "empreendimento");

            migrationBuilder.DropTable(
                name: "Enderecos",
                schema: "empreendimento");

            migrationBuilder.DropTable(
                name: "Imagens",
                schema: "empreendimento");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

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
        }
    }
}
