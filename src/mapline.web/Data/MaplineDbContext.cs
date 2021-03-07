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
            : base(options)
        {

        }

        private IDataBuilder _dataBuilder;
        public IDataBuilder DataBuilder 
        {
            get => _dataBuilder ??= LanguagesBuilder.CreateDataBuilder();           
            init => _dataBuilder = value;
        }

        public virtual DbSet<Language> Languages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if(DataBuilder == null)
            {
                // This shouldn't happen because if the getter null initialization.
                // So if this happens, there's something wrong in the LanguageBuilder.CreateDataBulder()
                throw new InvalidOperationException("The DataBuiler is not null."); 
            }

            modelBuilder.Entity<Language>().HasMany<LanguageRelationship>(l => l.ParentRelationships).WithOne(lr => lr.Parent);
            modelBuilder.Entity<Language>().HasMany<LanguageRelationship>(l => l.ChildRelationships).WithOne(lr => lr.Child);            
            modelBuilder.Entity<Language>().Property(x => x.Area).HasColumnType("geometry");// Area type must be be specified: https://github.com/NetTopologySuite/NetTopologySuite/issues/365
            modelBuilder.ToEntityTable<Language>().HasData(DataBuilder.Languages);

            modelBuilder.ToEntityTable<Filter>();

            modelBuilder.ToEntityTable<LanguageRelationship>();
            modelBuilder.ToEntityTable<LanguageFilter>();
            modelBuilder.ToEntityTable<Filter>();
        }
    }
}
