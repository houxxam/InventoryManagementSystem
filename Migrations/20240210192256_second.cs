using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvWebApp.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Materiels_ServiceId",
                table: "Materiels",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materiels_Services_ServiceId",
                table: "Materiels",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materiels_Services_ServiceId",
                table: "Materiels");

            migrationBuilder.DropIndex(
                name: "IX_Materiels_ServiceId",
                table: "Materiels");
        }
    }
}
