using Mapline.Web.Data.Building;
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
        public MaplineDbContext(DbContextOptions<MaplineDbContext> options)  
            : this(options, LanguagesBuilder.CreateDataBuilder()) 
        {

        }

        public MaplineDbContext(DbContextOptions<MaplineDbContext> options, IDataBuilder dataBuilder)
            : base(options)
        {
            DataBuilder = dataBuilder ?? throw new ArgumentNullException(nameof(dataBuilder));
        }

        public IDataBuilder DataBuilder { get; set; }

        public virtual DbSet<Language> Languages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>().HasMany<LanguageRelationship>(l => l.ParentRelationships).WithOne(lr => lr.Parent);
            modelBuilder.Entity<Language>().HasMany<LanguageRelationship>(l => l.ChildRelationships).WithOne(lr => lr.Child);
            modelBuilder.ToEntityTable<Language>()
                .HasData(DataBuilder.Languages)
            ;

            modelBuilder.ToEntityTable<Filter>();

            modelBuilder.ToEntityTable<LanguageRelationship>();
            modelBuilder.ToEntityTable<LanguageFilter>();
            modelBuilder.ToEntityTable<Filter>();
        }
    }
}
