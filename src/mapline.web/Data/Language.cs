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

namespace Mapline.Web.Data
{
    /// <summary>
    /// Database reprentation of Language.
    /// </summary>
    public class Language
    {
        public long Id { get; set; }
        public string Name { get; set; }

        [JsonProperty(PropertyName = "geometry", ItemConverterType = typeof(GeometryConverter))]
        public Geometry Area { get; set; }
        public int? YearCurrent { get; set; }
        public int? YearStart { get; set; }
        public int? YearEnd { get; set; }

        public string Features { get; set; }

        public string AdditionalDetails { get; set; }
    }
}

