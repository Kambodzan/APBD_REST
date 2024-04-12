using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_REST.Migrations
{
    /// <inheritdoc />
    public partial class Visits_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Animals_AnimalId",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Visits_AnimalId",
                table: "Visits");

            migrationBuilder.RenameColumn(
                name: "AnimalId",
                table: "Visits",
                newName: "AnimalID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AnimalID",
                table: "Visits",
                newName: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_AnimalId",
                table: "Visits",
                column: "AnimalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Animals_AnimalId",
                table: "Visits",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
