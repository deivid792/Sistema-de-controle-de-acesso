using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisitorService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeCheckInAndCheckOutOptionalOnVisits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckOut",
                table: "Visits",
                type: "DATETIME2(0)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2(0)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckIn",
                table: "Visits",
                type: "DATETIME2(0)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2(0)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckOut",
                table: "Visits",
                type: "DATETIME2(0)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2(0)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckIn",
                table: "Visits",
                type: "DATETIME2(0)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "DATETIME2(0)",
                oldNullable: true);
        }
    }
}
