using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VisitorService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixRolesAndSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Roles",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id","Name", "Description" },
                values: new object[,]
                {
                    { new Guid("1826500d-587b-4009-8d7b-422838183186"),3, "Responsável pelo controle de fluxo, execução de check-in, check-out e monitoramento de acessos em tempo real." },
                    { new Guid("72579b2a-795a-4700-9118-09549d44c77c"),1, "Perfil com permissões limitadas, focado na visualização de dados e gerenciamento do próprio cadastro." },
                    { new Guid("f399f57d-e692-411a-853f-42a9693952f4"),2, "Acesso total ao sistema, com poderes para gerenciar usuários, configurar roles, extrair relatórios e auditar logs" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1826500d-587b-4009-8d7b-422838183186"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("72579b2a-795a-4700-9118-09549d44c77c"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f399f57d-e692-411a-853f-42a9693952f4"));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Roles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);
        }
    }
}
