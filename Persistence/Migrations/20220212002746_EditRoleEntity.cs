using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class EditRoleEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 12, 3, 57, 46, 131, DateTimeKind.Local).AddTicks(7453),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 12, 3, 23, 11, 251, DateTimeKind.Local).AddTicks(6912));

            migrationBuilder.AlterColumn<string>(
                name: "RoleTitle",
                table: "Roles",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 400);

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 12, 3, 57, 46, 109, DateTimeKind.Local).AddTicks(8222),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 12, 3, 23, 11, 235, DateTimeKind.Local).AddTicks(2656));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 12, 3, 23, 11, 251, DateTimeKind.Local).AddTicks(6912),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 12, 3, 57, 46, 131, DateTimeKind.Local).AddTicks(7453));

            migrationBuilder.AlterColumn<long>(
                name: "RoleTitle",
                table: "Roles",
                type: "bigint",
                maxLength: 400,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(400)",
                oldMaxLength: 400);

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 12, 3, 23, 11, 235, DateTimeKind.Local).AddTicks(2656),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 12, 3, 57, 46, 109, DateTimeKind.Local).AddTicks(8222));
        }
    }
}
