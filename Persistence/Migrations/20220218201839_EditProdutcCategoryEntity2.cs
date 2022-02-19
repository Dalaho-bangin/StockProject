using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class EditProdutcCategoryEntity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 18, 23, 48, 38, 92, DateTimeKind.Local).AddTicks(8295),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 18, 17, 18, 55, 376, DateTimeKind.Local).AddTicks(8326));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 18, 23, 48, 38, 92, DateTimeKind.Local).AddTicks(4639),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 18, 17, 18, 55, 376, DateTimeKind.Local).AddTicks(4341));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 18, 23, 48, 38, 75, DateTimeKind.Local).AddTicks(2114),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 18, 17, 18, 55, 360, DateTimeKind.Local).AddTicks(228));

            migrationBuilder.AddColumn<string>(
                name: "ParentProductCategoryName",
                table: "ProductCategories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentProductCategoryName",
                table: "ProductCategories");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 18, 17, 18, 55, 376, DateTimeKind.Local).AddTicks(8326),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 18, 23, 48, 38, 92, DateTimeKind.Local).AddTicks(8295));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 18, 17, 18, 55, 376, DateTimeKind.Local).AddTicks(4341),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 18, 23, 48, 38, 92, DateTimeKind.Local).AddTicks(4639));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 18, 17, 18, 55, 360, DateTimeKind.Local).AddTicks(228),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 2, 18, 23, 48, 38, 75, DateTimeKind.Local).AddTicks(2114));
        }
    }
}
