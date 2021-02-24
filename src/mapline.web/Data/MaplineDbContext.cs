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

            modelBuilder.Entity<Language>()
                .ToTable("Language")
                .HasData(builder.Languages)
            ;

            modelBuilder.Entity<Filter>()
                .ToTable("Filter")
                //.HasData(builder.Languages)
            ;
        }
    }

    public static class DbSetExtensions
    {
        public static FeatureCollection ToFeatureCollection<TData>(this DbSet<TData> dataSet)
            where TData : class, IFeatureable
        {
            var featureCollection = new FeatureCollection();

            // Feature collection cannot be initialized prettier right now...
            // https://github.com/NetTopologySuite/NetTopologySuite.Features/pull/12
            foreach (var element in dataSet)
            {
                featureCollection.Add(element.ToFeature());
            }

            return featureCollection;
        }
    }
}
