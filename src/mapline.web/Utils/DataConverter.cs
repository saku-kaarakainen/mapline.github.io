using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mapline.Web.Utils
{
    public static class DataConverter
    {
        public static FeatureCollection StringToGeoJson(string data)
        {
            var geoJsonSerializer = GeoJsonSerializer.Create();
            using var geoJsonStringReader = new StringReader(data);
            using var geoJsonJsonReader = new JsonTextReader(geoJsonStringReader);
            var geojson = geoJsonSerializer.Deserialize<FeatureCollection>(geoJsonJsonReader);
            return geojson;
        }

        public static Geometry ToGeometry(this FeatureCollection features)
        {
            if (features == null)
            {
                throw new ArgumentNullException(nameof(features));
            }

            if(features.Count != 1)
            {
                throw new NotSupportedException($"Right now only one geometry pear feature is supported. Geometry count: {features.Count}");
            }

            return features.First().Geometry;
        }
    }
}
