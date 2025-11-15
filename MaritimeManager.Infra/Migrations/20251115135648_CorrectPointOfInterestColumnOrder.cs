using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaritimeManager.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CorrectPointOfInterestColumnOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "PointsOfInterest",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PointsOfInterest",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "PointsOfInterest",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL")
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "PointsOfInterest",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL")
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<string>(
                name: "Identifier",
                table: "PointsOfInterest",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "PointsOfInterest",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PointsOfInterest",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Relational:ColumnOrder", 0)
                .Annotation("Sqlite:Autoincrement", true)
                .OldAnnotation("Sqlite:Autoincrement", true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "PointsOfInterest",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PointsOfInterest",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "PointsOfInterest",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL")
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "PointsOfInterest",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL")
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<string>(
                name: "Identifier",
                table: "PointsOfInterest",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "PointsOfInterest",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PointsOfInterest",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true)
                .OldAnnotation("Relational:ColumnOrder", 0)
                .OldAnnotation("Sqlite:Autoincrement", true);
        }
    }
}
