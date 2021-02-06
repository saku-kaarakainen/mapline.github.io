using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

using NetTopologySuite.Geometries;

namespace mapline.Migrations 
{
    /// <summary>
    /// The initial migration
    /// </summary>
    public partial class initialMigration : Migration
    {
        public const string NameOfDataDatabase = "Mapline";

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new 
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", 
                            SqlServerValueGenerationStrategy.IdentityColumn),  
                    StringIdentifier = table.Column<string>(nullable: false),
                    StartDate = table.Column<int>(nullable: true),
                    EndDate = table.Column<int>(nullable: true),
                    Year = table.Column<int>(nullable: true), 
                    Features = table.Column<string>(nullable: true), // JSON
                    AdditionalDetails = table.Column<string>(nullable: true), // JSON
                    Geography = table.Column<Geometry>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", language => language.Id);
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Language");
        } 
    }
}