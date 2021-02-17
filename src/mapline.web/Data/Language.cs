using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using NetTopologySuite.IO.Converters;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Features;

namespace Mapline.Web.Data
{
    public interface IFeatureable
    {
        Feature ToFeature();
    }

    /// <summary>
    /// Database reprentation of Language.
    /// </summary>
    public class Language : IFeatureable
    {
        public long Id { get; set; }
        public string Name { get; set; }

        [JsonProperty(PropertyName = "geometry", ItemConverterType = typeof(GeometryConverter))]
        public virtual Geometry Area { get; set; }
                
        public int? YearStart { get; set; }
        public int? YearEnd { get; set; }

        public string Features { get; set; }


        public string AdditionalDetails { get; set; }

        public Feature ToFeature() => new Feature(Area, new AttributesTable
        {
            { "name", Name },
            { "yearStart", YearStart },
            { "yearEnd", YearEnd },
            { "languageProperties", Features },
            { "AdditionalDetails", AdditionalDetails }
        });        
    }
}

