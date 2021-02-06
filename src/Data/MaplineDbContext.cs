using System;
using System.IO;
using System.Linq;
using Newtonsoft;
using Microsoft.EntityFrameworkCore;
using mapline.Models;
using Newtonsoft.Json;
using NetTopologySuite.IO;
using NetTopologySuite.Features;

namespace mapline.Data
{
    /// <summary>
    /// The database context for the Mapline, 
    /// which is using the code first approach. 
    /// </summary>
    public partial class MaplineDbContext : DbContext
    {
        private static long seedCounter = 1;
        public MaplineDbContext(DbContextOptions options)
            : base(options)
        {

        }
    
        public DbSet<Language> Languages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region Seed data

            const string tableJsonSuffix = "\\table.json";
            const string areaJsonSuffix = "\\area.geojson";
            const string languageFolder = "..\\data\\GeoJson\\Language";

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

               var language = (Language)JsonConvert.DeserializeObject<Magic.Language>(jsonTable);
               language.Id = seedCounter++;
               language.StringIdentifier = folder.Replace(languageFolder, "").Replace(areaJsonSuffix, "").TrimStart('\\');
               language.Area = "test"; // areaFeatures.First().Geometry;

               return language;
            }
            #endregion

            modelBuilder.Entity<Language>()
                .ToTable("Language")
                //.HasData(languages)
            ;
        }
    }
}