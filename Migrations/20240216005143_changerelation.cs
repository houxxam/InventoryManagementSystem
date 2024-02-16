using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvWebApp.Migrations
{
    /// <inheritdoc />
    public partial class changerelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceGroupId",
                table: "Materiels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Materiels_ServiceGroupId",
                table: "Materiels",
                column: "ServiceGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materiels_serviceGroups_ServiceGroupId",
                table: "Materiels",
                column: "ServiceGroupId",
                principalTable: "serviceGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materiels_serviceGroups_ServiceGroupId",
                table: "Materiels");

            migrationBuilder.DropIndex(
                name: "IX_Materiels_ServiceGroupId",
                table: "Materiels");

            migrationBuilder.DropColumn(
                name: "ServiceGroupId",
                table: "Materiels");
        }
    }
}
