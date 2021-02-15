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
            var languages = folders
                .Select(folder => LanguageHelper.Instance.FolderToLanguage(folder, ref seedCounter, LanguageFolder, areaJsonSuffix))
                .Where(lang => lang != default);

            modelBuilder.Entity<Language>()
                .ToTable("Language")
                .HasData(languages)
            ;
        }
    }
}
