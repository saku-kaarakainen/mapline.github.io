using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
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
            //migrationBuilder.CreateTable(
            //    name: "Language",
            //    columns: table => new
            //    {
            //        Id = table.PK_Column<long>(),
            //        Name = table.Column<string>(nullable: false),
            //        Area = table.Column<Geometry>(nullable: false),
            //        YearCurrent = table.Column<int>(nullable: true),
            //        YearStart = table.Column<int>(nullable: true),
            //        YearEnd = table.Column<int>(nullable: true),
            //        Features = table.Column<string>(nullable: true), // JSON
            //        AdditionalDetails = table.Column<string>(nullable: true), // JSON
            //    },
            //    constraints: table => table.PrimaryKey("PK_Language_Id", x => x.Id));

            //migrationBuilder.CreateTable(
            //    name: "Filter",
            //    columns: table => new
            //    {
            //        Id = table.PK_Column<long>(),
            //        Name = table.Column<string>(nullable: false),
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Filter_Id", x => x.Id);
            //        table.PrimaryKey("PK_Filter_Name", f => f.Name);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LanguageFilter",
            //    columns: table => new
            //    {
            //        Id = table.PK_Column<long>(),
            //        LanguageId = table.Column<long>(),
            //        FilterId = table.Column<long>()
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LanguageFilter_Id", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_LanguageFilter_Language_LanguageId",
            //            column: x => x.LanguageId,
            //            principalTable: "Language",
            //            principalColumn: "LanguageId",
            //            onDelete: ReferentialAction.SetNull);
            //        table.ForeignKey(
            //            name: "FK_LanguageFilter_Filter_FilterId",
            //            column: x => x.FilterId,
            //            principalTable: "Filter",
            //            principalColumn: "FilterId",
            //            onDelete: ReferentialAction.SetNull);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LanguageRelationship",
            //    columns: table => new
            //    {
            //        Id = table.PK_Column<long>(),
            //        Type = table.Column<int>(),
            //        ParentId = table.Column<long>(),
            //        ChildId = table.Column<long>(),
            //    },
            //    constraints: table =>
            //    { 
            //        table.PrimaryKey("PK_LanguageRelationship_Id", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_LanguageRelationship_Language_ParentId",
            //            column: x => x.ParentId,
            //            principalTable: "Language",
            //            principalColumn: "ParentId",
            //            onDelete: ReferentialAction.SetNull);
            //        table.ForeignKey(
            //            name: "FK_LanguageRelationship_Language_ChildId",
            //            column: x => x.ChildId,
            //            principalTable: "Language",
            //            principalColumn: "ChildId",
            //            onDelete: ReferentialAction.SetNull);
            //    });           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Language");
        }
    }

    public static class ColumnsBuilderExtensions
    {
        public static OperationBuilder<AddColumnOperation> PK_Column<TType>(this ColumnsBuilder table)
        {
            return table.Column<TType>(nullable: false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        }
    }
}
