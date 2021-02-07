using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace mapline.Migrations
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
                    //Area = table.Column<Geometry>(nullable: false),
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            base.Down(migrationBuilder);

            migrationBuilder.DropTable("Language");
        }
    }
}
