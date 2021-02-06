using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace mapline.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StringIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Area = table.Column<Geometry>(type: "geography", nullable: true),
                    YearStart = table.Column<int>(type: "int", nullable: true),
                    YearEnd = table.Column<int>(type: "int", nullable: true),
                    YearCurrent = table.Column<int>(type: "int", nullable: true),
                    Features = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalDetails = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Language");
        }
    }
}
