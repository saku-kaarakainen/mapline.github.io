using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

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
            // migrationBuilder.Sql($"CREATE DATABASE {NameOfDataDatabase}}");

            // Database is not created here.
            // For that, check MaplineDbContext
            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new 
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", 
                            SqlServerValueGenerationStrategy.IdentityColumn),  
                    
                    // Area geography
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Features = table.Column<string>(nullable: true), // JSON
                    AdditionalDetails = table.Column<string>(nullable: true), // JSON
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", language => language.Id);
                }
            );

            // https://github.com/dotnet/efcore/issues/1100#issuecomment-286362657
            migrationBuilder.Sql($"ALTER TABLE [dbo].[{NameOfDataDatabase}] ADD [Area] geography");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Language");
        } 
    }
}