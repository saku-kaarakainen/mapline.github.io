using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Features;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mapline.Web.Data
{
    public class MaplineDbContext : DbContext
    {
        private static int seedCounter = 1;

        public MaplineDbContext(DbContextOptions<MaplineDbContext> options) 
            : base(options)
        {

        }

        public DbSet<Language> Languages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            const string tableJsonSuffix = "\\table.json";
            const string areaJsonSuffix = "\\area.geojson";
            const string languageFolder = "..\\..\\data\\Language";

            // name of the folder is the string identifier
            var folders = Directory.GetDirectories(languageFolder);
            var languages = folders.Select(ToLanguage).Where(lang => lang != default);
            Language ToLanguage(string folder)
            {
                var tableFilePath = folder + tableJsonSuffix;
                var areaFilePath = folder + areaJsonSuffix;

                if (!File.Exists(tableFilePath))
                {
                    throw new FileNotFoundException("table.json is required to create table.", tableFilePath);
                }

                if (!File.Exists(areaFilePath))
                {
                    throw new FileNotFoundException("table.json is required to create the geometry to the table.", areaFilePath);
                }

                using var readerTable = new StreamReader(tableFilePath);
                using var readerArea = new StreamReader(areaFilePath);

                var jsonTable = readerTable.ReadToEnd();
                var geoJsonArea = readerArea.ReadToEnd();

                var geoJsonSerializer = GeoJsonSerializer.Create();
                using var geoJsonStringReader = new StringReader(geoJsonArea);
                using var geoJsonJsonReader = new JsonTextReader(geoJsonStringReader);
                var areaFeatures = geoJsonSerializer.Deserialize<FeatureCollection>(geoJsonJsonReader);

                if (areaFeatures.Count != 1)
                {
                    throw new NotSupportedException($"Right only one geometry is supported. Geometry count: {areaFeatures.Count}");
                }

                var language = Language.CreateFromJson(jsonTable);
                language.Id = seedCounter++;
                language.Name = folder.Replace(languageFolder, "").Replace(areaJsonSuffix, "").TrimStart('\\');
                language.Area = areaFeatures.First().Geometry;

                return language;
            }

            modelBuilder.Entity<Language>()
                .ToTable("Language")
                .HasData(languages)
            ;
        }
    }
}
