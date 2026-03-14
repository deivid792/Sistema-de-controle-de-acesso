using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisitorService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixSnapshotSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1826500d-587b-4009-8d7b-422838183186"),
                column: "Name",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("72579b2a-795a-4700-9118-09549d44c77c"),
                column: "Name",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f399f57d-e692-411a-853f-42a9693952f4"),
                column: "Name",
                value: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
