using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetTopologySuite.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mapline.Web.Data
{
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

    public static class EntityExtensions
    {
        public static EntityTypeBuilder<TType> ToEntityTable<TType>(this ModelBuilder builder)
            where TType : class
        {
            return builder.Entity<TType>().ToTable(typeof(TType).Name);
        }
    }
}
