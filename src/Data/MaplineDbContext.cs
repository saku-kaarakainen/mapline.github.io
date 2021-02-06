using System.IO;
using System.Linq;
using Newtonsoft;
using Microsoft.EntityFrameworkCore;
using mapline.Models;
using Newtonsoft.Json;
using NetTopologySuite.IO;
using NetTopologySuite.Geometries;

namespace mapline.Data
{
    /// <summary>
    /// The database context for the Mapline, 
    /// which is using the code first approach. 
    /// </summary>
    public partial class MaplineDbContext : DbContext
    {
        public MaplineDbContext(DbContextOptions options)
            : base(options)
        {

        }
    
        public DbSet<Language> Languages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // name of the folder is the string identifier
            var folders = Directory.GetDirectories("..\\data\\GeoJson\\Language");
            var languages = folders.Select(ToLanguage).Where(lang => lang != default);
            Language ToLanguage(string folder)
            {
                var table = folder + "\\table.json";
                var area = folder + "\\area.geojson";

                if (!File.Exists(table) || !File.Exists(area))
                {
                    // TODO: Do logging?
                    return default;
                }

                using var readerTable = new StreamReader(table);
                using var readerArea = new StreamReader(area);

                var jsonTable = readerTable.ReadToEnd();
                var geoJsonArea = readerArea.ReadToEnd();

                var geoJsonSerializer = GeoJsonSerializer.Create();
                using var geoJsonStringReader = new StringReader(geoJsonArea);
                using var geoJsonJsonReader = new JsonTextReader(geoJsonStringReader);
                var areaGeometry = geoJsonSerializer.Deserialize<Geometry>(geoJsonJsonReader);

                var language = JsonConvert.DeserializeObject<Language>(jsonTable);
                language.StringIdentifier = folder;
                language.Area = areaGeometry;

                return language;
            }

            modelBuilder.Entity<Language>()
                .ToTable("Language")
                .HasData(languages);
        }
    }
}