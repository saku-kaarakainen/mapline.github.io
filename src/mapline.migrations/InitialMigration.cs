using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Mapline.Migrations
{
    public class InitialMigration : Migration
    {
        public const string Database = "Mapline";

        protected override void Up([NotNull] MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new 
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),

                    Name = table.Column<string>(nullable: false),
                    Area = table.Column<Geometry>(nullable: false),
                    YearCurrent = table.Column<int>(nullable: true),
                    YearStart = table.Column<int>(nullable: true),
                    YearEnd = table.Column<int>(nullable: true),
                    Features = table.Column<string>(nullable: true), // JSON
                    AdditionalDetails = table.Column<string>(nullable: true), // JSON

                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language_Id", l => l.Id);
                    table.PrimaryKey("PK_Language_Name", l => l.Name);
                }
            );

            migrationBuilder.CreateTable<object>(
                name: "Filter",
                columns: table => throw new NotImplementedException(),
                // long Id
                // string Name
                // ICollection<LanguageFilter> LanguageFilters 
                constraints: table => throw new NotImplementedException()
            );

            migrationBuilder.CreateTable<object>(
                name: "LanguageFilter",
                columns: table => throw new NotImplementedException(),
                    // long LanguageId   
                    // Language Language 
                    // long FilterId     
                    // Filter Filter     
                constraints: table => throw new NotImplementedException()
            );

            migrationBuilder.CreateTable<object>(
                name: "LanguageRelationship",
                /*
                         public RelationshipType Type { get; set; }

        public long ParentId { get; set; }
        public Language Parent { get; set; }

        public long ChildID { get; set; }
        public Language Child { get; set; }
                 */
                columns: table => throw new NotImplementedException(),
                constraints: table => throw new NotImplementedException()
            );

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Language");
        }
    }
}
