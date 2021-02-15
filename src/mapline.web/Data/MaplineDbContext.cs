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
            const string areaJsonSuffix = "\\area.geojson";

            // name of the folder is the string identifier
            var folders = Directory.GetDirectories(LanguageFolder);
            var languages = folders.Select(ToLanguage).Where(lang => lang != default);
            Language ToLanguage(string folder)
            {

                var yearDirectory = Directory.GetDirectories(folder)[0];
                var files = Directory
                    .GetFiles(yearDirectory)
                    .Select(fileName => DirectoryHelper.CreateColumn(fileName, yearDirectory))
                    .ToDictionary(key => key.Name, value => value.Value);

                var years = yearDirectory.Replace(folder, "").TrimStart('\\');
                var (start, end) = DirectoryHelper.ParseYearsFromTheFolderName(years);

                var lang = new Language()
                {
                    Id = seedCounter++,
                    Name = folder.Replace(LanguageFolder, "").Replace(areaJsonSuffix, "").TrimStart('\\'),
                    YearStart = start,
                    YearEnd = end,
                    Area = ((FeatureCollection)files["area"]).ToGeometry(),
                    Features = files["features"].ToString(),
                    AdditionalDetails = "{}"
                };

                return lang;
            }

            modelBuilder.Entity<Language>()
                .ToTable("Language")
                .HasData(languages)
            ;


        }
    }
}
