using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Locus.Migrations
{
    public partial class TenantId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_TenantId",
                table: "Rooms",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_TenantId",
                table: "Images",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Tenants_TenantId",
                table: "Images",
                column: "TenantId",
                principalTable: "Tenants",
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
                name: "FK_Images_Tenants_TenantId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Tenants_TenantId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_TenantId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Images_TenantId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Images");
        }
    }
}
