using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APBD_REST.Migrations
{
    /// <inheritdoc />
    public partial class ModelCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Visits_AnimalID",
                table: "Visits",
                column: "AnimalID");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Animals_AnimalID",
                table: "Visits",
                column: "AnimalID",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Animals_AnimalID",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Visits_AnimalID",
                table: "Visits");
        }
    }
}
