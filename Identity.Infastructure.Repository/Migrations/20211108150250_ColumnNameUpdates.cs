using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Infastructure.Migrations
{
    public partial class ColumnNameUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claim_Role_RoleId",
                table: "Claim");

            migrationBuilder.RenameColumn(
                name: "RoleName",
                table: "Role",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ClaimName",
                table: "Claim",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "Claim",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Claim_Role_RoleId",
                table: "Claim",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claim_Role_RoleId",
                table: "Claim");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Role",
                newName: "RoleName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Claim",
                newName: "ClaimName");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "Claim",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Claim_Role_RoleId",
                table: "Claim",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
