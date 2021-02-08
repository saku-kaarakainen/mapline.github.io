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

namespace Mapline.Web.Data
{
    public class Language
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Geometry Area { get; set; }
        public int? YearCurrent { get; set; }
        public int? YearStart { get; set; }
        public int? YearEnd { get; set; }

        public string Features { get; set; }

        public string AdditionalDetails { get; set; }

        public static Language CreateFromJson(string json)
        {
            if (string.IsNullOrEmpty(json))
                throw new ArgumentException("The specified json is null or empty.", nameof(json));

            JsonProxyClass proxyClass = JsonConvert.DeserializeObject<JsonProxyClass>(json);

            // TODO: use AutoMapper
            return proxyClass == default ? default : new Language
            {
                Id = default, // proxyClass.Id,
                Name = proxyClass.Name,
                Area = default, // proxyClass.Area,
                YearCurrent = proxyClass.YearCurrent,
                YearStart = proxyClass.YearStart,
                YearEnd = proxyClass.YearEnd,
                Features = proxyClass.Features.ToString(),
                AdditionalDetails = proxyClass.AdditionalDetails.ToString()
            };
        }

        /// <summary>
        /// Represents the class of <see cref="Language" /> that can be converted to a class from a JSON string.
        /// </summary>
        private class JsonProxyClass
        {
            public string Name { get; set; }
            public int? YearCurrent { get; set; }
            public int? YearStart { get; set; }
            public int? YearEnd { get; set; }

            public object Features { get; set; }
            public object AdditionalDetails { get; set; }
        }
    }
}

