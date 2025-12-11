using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Imobi.Migrations
{
    /// <inheritdoc />
    public partial class AjustarSchemas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.EnsureSchema(
                name: "realestate");

            migrationBuilder.EnsureSchema(
                name: "auth");

            migrationBuilder.RenameTable(
                name: "Unidades",
                newName: "Unidades",
                newSchema: "realestate");

            migrationBuilder.RenameTable(
                name: "InstalacaoCondominios",
                newName: "InstalacaoCondominios",
                newSchema: "realestate");

            migrationBuilder.RenameTable(
                name: "Enderecos",
                newName: "Enderecos",
                newSchema: "realestate");

            migrationBuilder.RenameTable(
                name: "Empreendimentos",
                newName: "Empreendimentos",
                newSchema: "realestate");

            migrationBuilder.RenameTable(
                name: "Condominios",
                newName: "Condominios",
                newSchema: "realestate");

            migrationBuilder.RenameTable(
                name: "Arquivos",
                newName: "Arquivos",
                newSchema: "realestate");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "UsuariosTokens",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "Usuarios",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "UsuariosPerfis",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "UsuariosLogins",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "UsuariosClaims",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "Perfis",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "PerfisClaims",
                newSchema: "auth");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "auth",
                table: "UsuariosPerfis",
                newName: "IX_UsuariosPerfis_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "auth",
                table: "UsuariosLogins",
                newName: "IX_UsuariosLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "auth",
                table: "UsuariosClaims",
                newName: "IX_UsuariosClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "auth",
                table: "PerfisClaims",
                newName: "IX_PerfisClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuariosTokens",
                schema: "auth",
                table: "UsuariosTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                schema: "auth",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuariosPerfis",
                schema: "auth",
                table: "UsuariosPerfis",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuariosLogins",
                schema: "auth",
                table: "UsuariosLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuariosClaims",
                schema: "auth",
                table: "UsuariosClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Perfis",
                schema: "auth",
                table: "Perfis",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PerfisClaims",
                schema: "auth",
                table: "PerfisClaims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PerfisClaims_Perfis_RoleId",
                schema: "auth",
                table: "PerfisClaims",
                column: "RoleId",
                principalSchema: "auth",
                principalTable: "Perfis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosClaims_Usuarios_UserId",
                schema: "auth",
                table: "UsuariosClaims",
                column: "UserId",
                principalSchema: "auth",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosLogins_Usuarios_UserId",
                schema: "auth",
                table: "UsuariosLogins",
                column: "UserId",
                principalSchema: "auth",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosPerfis_Perfis_RoleId",
                schema: "auth",
                table: "UsuariosPerfis",
                column: "RoleId",
                principalSchema: "auth",
                principalTable: "Perfis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosPerfis_Usuarios_UserId",
                schema: "auth",
                table: "UsuariosPerfis",
                column: "UserId",
                principalSchema: "auth",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosTokens_Usuarios_UserId",
                schema: "auth",
                table: "UsuariosTokens",
                column: "UserId",
                principalSchema: "auth",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerfisClaims_Perfis_RoleId",
                schema: "auth",
                table: "PerfisClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosClaims_Usuarios_UserId",
                schema: "auth",
                table: "UsuariosClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosLogins_Usuarios_UserId",
                schema: "auth",
                table: "UsuariosLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosPerfis_Perfis_RoleId",
                schema: "auth",
                table: "UsuariosPerfis");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosPerfis_Usuarios_UserId",
                schema: "auth",
                table: "UsuariosPerfis");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosTokens_Usuarios_UserId",
                schema: "auth",
                table: "UsuariosTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuariosTokens",
                schema: "auth",
                table: "UsuariosTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuariosPerfis",
                schema: "auth",
                table: "UsuariosPerfis");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuariosLogins",
                schema: "auth",
                table: "UsuariosLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuariosClaims",
                schema: "auth",
                table: "UsuariosClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                schema: "auth",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PerfisClaims",
                schema: "auth",
                table: "PerfisClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Perfis",
                schema: "auth",
                table: "Perfis");

            migrationBuilder.RenameTable(
                name: "Unidades",
                schema: "realestate",
                newName: "Unidades");

            migrationBuilder.RenameTable(
                name: "InstalacaoCondominios",
                schema: "realestate",
                newName: "InstalacaoCondominios");

            migrationBuilder.RenameTable(
                name: "Enderecos",
                schema: "realestate",
                newName: "Enderecos");

            migrationBuilder.RenameTable(
                name: "Empreendimentos",
                schema: "realestate",
                newName: "Empreendimentos");

            migrationBuilder.RenameTable(
                name: "Condominios",
                schema: "realestate",
                newName: "Condominios");

            migrationBuilder.RenameTable(
                name: "Arquivos",
                schema: "realestate",
                newName: "Arquivos");

            migrationBuilder.RenameTable(
                name: "UsuariosTokens",
                schema: "auth",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "UsuariosPerfis",
                schema: "auth",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "UsuariosLogins",
                schema: "auth",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "UsuariosClaims",
                schema: "auth",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                schema: "auth",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "PerfisClaims",
                schema: "auth",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "Perfis",
                schema: "auth",
                newName: "AspNetRoles");

            migrationBuilder.RenameIndex(
                name: "IX_UsuariosPerfis_RoleId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UsuariosLogins_UserId",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UsuariosClaims_UserId",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PerfisClaims_RoleId",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
