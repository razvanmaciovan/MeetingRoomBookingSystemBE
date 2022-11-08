using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Locus.Migrations
{
    public partial class nullFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Layouts_LayoutId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Tenants_TenantId",
                table: "Rooms");

            migrationBuilder.AlterColumn<int>(
                name: "TenantId",
                table: "Rooms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LayoutId",
                table: "Rooms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Layouts_LayoutId",
                table: "Rooms",
                column: "LayoutId",
                principalTable: "Layouts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Tenants_TenantId",
                table: "Rooms",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Layouts_LayoutId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Tenants_TenantId",
                table: "Rooms");

            migrationBuilder.AlterColumn<int>(
                name: "TenantId",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LayoutId",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Layouts_LayoutId",
                table: "Rooms",
                column: "LayoutId",
                principalTable: "Layouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Tenants_TenantId",
                table: "Rooms",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
