using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaritimeManager.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentifierToPointOfInterest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Identifier",
                table: "PointsOfInterest",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "PointsOfInterest");
        }
    }
}
