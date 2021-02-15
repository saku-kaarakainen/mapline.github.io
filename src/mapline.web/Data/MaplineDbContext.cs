using Mapline.Web.Utils; 
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

        public virtual DbSet<Language> Languages { get; set; }

        protected string LanguageFolder { get; set; } = "..\\..\\data\\Language";

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            const string tableJsonSuffix = "\\features.json";
            const string areaJsonSuffix = "\\area.geojson";

            // name of the folder is the string identifier
            var folders = Directory.GetDirectories(LanguageFolder);
            var languages = folders.Select(ToLanguage).Where(lang => lang != default);
            Language ToLanguage(string folder)
            {

                var yearDirectory = Directory.GetDirectories(folder)[0];
                
                var tableFilePath = yearDirectory + tableJsonSuffix;
                var areaFilePath = yearDirectory + areaJsonSuffix;

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

                var featuresTable = readerTable.ReadToEnd();
                var geoJsonArea = readerArea.ReadToEnd();

                var geoJsonSerializer = GeoJsonSerializer.Create();
                using var geoJsonStringReader = new StringReader(geoJsonArea);
                using var geoJsonJsonReader = new JsonTextReader(geoJsonStringReader);
                var areaFeatures = geoJsonSerializer.Deserialize<FeatureCollection>(geoJsonJsonReader);

                if (areaFeatures.Count != 1)
                {
                    throw new NotSupportedException($"Right only one geometry is supported. Geometry count: {areaFeatures.Count}");
                }

                var years = yearDirectory.Replace(folder, "").TrimStart('\\');
                var (start, end) = DirectoryHelper.ParseYearsFromTheFolderName(years);

                return new Language()
                {
                    Id = seedCounter++,
                    Name = folder.Replace(LanguageFolder, "").Replace(areaJsonSuffix, "").TrimStart('\\'),
                    YearStart = start,
                    YearEnd = end,
                    Area = areaFeatures.First().Geometry,
                    Features = System.Text.RegularExpressions.Regex.Replace(featuresTable, @"\t|\n|\r", "").Replace("   ", " "),
                    AdditionalDetails = "{}"
                };
            }

            modelBuilder.Entity<Language>()
                .ToTable("Language")
                .HasData(languages)
            ;
        }
    }
}
