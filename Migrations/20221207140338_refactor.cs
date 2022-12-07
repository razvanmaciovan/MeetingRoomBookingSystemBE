using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Locus.Migrations
{
    public partial class refactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Tenants_TenantId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Rooms_RoomId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Tenants_TenantId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_TenantId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Floor",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Floor",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "Images",
                newName: "LayoutId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_TenantId",
                table: "Images",
                newName: "IX_Images_LayoutId");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Reservations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Layouts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Layouts_TenantId",
                table: "Layouts",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Layouts_LayoutId",
                table: "Images",
                column: "LayoutId",
                principalTable: "Layouts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Layouts_Tenants_TenantId",
                table: "Layouts",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Rooms_RoomId",
                table: "Reservations",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Layouts_LayoutId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Layouts_Tenants_TenantId",
                table: "Layouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Rooms_RoomId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Layouts_TenantId",
                table: "Layouts");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Layouts");

            migrationBuilder.RenameColumn(
                name: "LayoutId",
                table: "Images",
                newName: "TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_LayoutId",
                table: "Images",
                newName: "IX_Images_TenantId");

            migrationBuilder.AddColumn<int>(
                name: "Floor",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Floor",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_TenantId",
                table: "Rooms",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Tenants_TenantId",
                table: "Images",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Rooms_RoomId",
                table: "Reservations",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Tenants_TenantId",
                table: "Rooms",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id");
        }
    }
}
