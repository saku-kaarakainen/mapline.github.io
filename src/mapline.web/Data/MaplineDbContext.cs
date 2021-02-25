using Mapline.Web.Utils; 
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Features;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mapline.Web.Data
{
    public class MaplineDbContext : DbContext
    {
        private static readonly int seedCounter = 1;

        public MaplineDbContext(DbContextOptions<MaplineDbContext> options) 
            : base(options)
        {
           
        }

        public virtual DbSet<Language> Languages { get; set; }

        protected string LanguageFolder { get; set; } = "..\\..\\data\\Language";

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            const string areaJsonSuffix = "\\area.geojson";
            var helper = new DirectoryHelper();
            var builder = new LanguagesBuilder(helper, LanguageFolder, areaJsonSuffix);
            builder.Counter = seedCounter;
            builder.CreateData();

            //modelBuilder.Entity<>

            modelBuilder.Entity<Language>().HasMany<LanguageRelationship>(l => l.ParentRelationships).WithOne(lr => lr.Parent);
            modelBuilder.Entity<Language>().HasMany<LanguageRelationship>(l => l.ChildRelationships).WithOne(lr => lr.Child);
            modelBuilder.ToEntityTable<Language>()
                .HasData(builder.Languages)
            ;

            modelBuilder.ToEntityTable<Filter>();

            modelBuilder.ToEntityTable<LanguageRelationship>();
            modelBuilder.ToEntityTable<LanguageFilter>();
            modelBuilder.ToEntityTable<Filter>();
        }
    }




}
