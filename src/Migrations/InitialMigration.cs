using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

using NetTopologySuite.Geometries;

namespace mapline.Migrations 
{
    /// <summary>
    /// The initial migration
    /// </summary>
    public partial class InitialMigration : Migration
    {
        public const string NameOfDataDatabase = "Mapline";

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new 
                {
                    Id = table.Column<long>(nullable: false).AsIdentity(),
                    StringIdentifier = table.Column<string>(nullable: true), //.AsIdentity(),
                    YearStart = table.Column<int>(nullable: true),
                    YearEnd = table.Column<int>(nullable: true),
                    YearCurrent = table.Column<int>(nullable: true), 
                    Features = table.Column<string>(nullable: true), // JSON
                    AdditionalDetails = table.Column<string>(nullable: true), // JSON
                    Geography = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language_longId", x => x.Id);
                    //table.PrimaryKey("PK_Language_stringId", x => x.StringIdentifier);
                }
            );;
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Language");
        } 
    }

    public static class OperationBuilderExtensions
    {
        public static Microsoft.EntityFrameworkCore.Migrations.Operations.Builders.OperationBuilder<TIdentity> AsIdentity<TIdentity>
            (this Microsoft.EntityFrameworkCore.Migrations.Operations.Builders.OperationBuilder<TIdentity> builder)
            where TIdentity : Microsoft.EntityFrameworkCore.Migrations.Operations.MigrationOperation        
            => builder.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        
    }
}