using Mapline.Web.Data;
using Mapline.Web.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NetTopologySuite.Algorithm;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mapline.Web.Models
{
    public class SaveLanguageModel : Language
    {
        // TODO: Rename the original features
        public object GeoJson { get; set; }
    }


    public static class SaveLanguageModelExtensions
    {
        public static void Validate(this SaveLanguageModel language)
        {
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            if (string.IsNullOrEmpty(language.Name)) 
                throw new ArgumentNullException(nameof(language.Name), "Name is null or empty.");

            if (!language.YearStart.HasValue) 
                throw new ArgumentNullException(nameof(language.YearStart), "YearStart has no value.");

            if (!language.YearEnd.HasValue) 
                throw new ArgumentNullException(nameof(language.YearEnd), "YearEnd has no value.");

            if (string.IsNullOrEmpty(language.GeoJson.ToString()) || language.GeoJson.ToString() == "{}")
                throw new ArgumentNullException(nameof(language.GeoJson), "GeoJson is null or empty");
        }

        public static async ValueTask<EntityEntry<Language>> AddModelAsync(this DbSet<Language> db, SaveLanguageModel model)
        {
            model.Validate();

            var feature =  DataConverter.DeserializeGeoJson<Feature>(model.GeoJson.ToString());
            model.Area = feature.Geometry;

            if (Orientation.IsCCW(model.Area.Coordinates))
            { 
                model.Area.Reverse();
            }

            if(model.Area == null)
            {
                throw new InvalidOperationException("Unable to convert given geoJSON into a Geometry");
            }

            return await db.AddAsync(model);
        }
    }
}
