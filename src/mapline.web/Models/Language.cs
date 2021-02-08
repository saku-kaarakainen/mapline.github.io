using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapline.Web.Utils;
using System.Text.Json.Serialization;
using NetTopologySuite.IO.Converters;
using Newtonsoft.Json;

namespace Mapline.Web.Models
{
    /// <summary>
    /// Model reprensentation of Language, 
    /// which can be sent via API.
    /// </summary>
    public class Language
    {
        public string Name { get; set; }

        [JsonProperty(PropertyName = "geometry", ItemConverterType = typeof(GeometryConverter))]
        public Geometry Area { get; set; }
        public int? YearCurrent { get; set; }
        public int? YearStart { get; set; }
        public int? YearEnd { get; set; }

        public string Features { get; set; }

        public string AdditionalDetails { get; set; }

        public static Language FromData(Data.Language data)
        {
            // TODO: Use AutoMapper
            return data == default ? default : new Language
            {
                // Id is no visible via API,
                Name = data.Name,
                Area = data.Area,
                YearCurrent = data.YearCurrent,
                YearStart = data.YearStart,
                YearEnd = data.YearEnd,
                Features = data.Features.ToString(),
                AdditionalDetails = data.AdditionalDetails.ToString()
            };
        }
    }
}
