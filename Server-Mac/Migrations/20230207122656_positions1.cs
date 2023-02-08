using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerMac.Migrations
{
    /// <inheritdoc />
    public partial class positions1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePosition_Positions_PositionId",
                table: "EmployeePosition");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePosition_Positions_PositionId",
                table: "EmployeePosition",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePosition_Positions_PositionId",
                table: "EmployeePosition");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePosition_Positions_PositionId",
                table: "EmployeePosition",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id");
        }
    }
}
