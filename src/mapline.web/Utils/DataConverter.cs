using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapline.Web.Utils
{
    public static class DataConverter
    {
        public static TResult DeserializeGeoJson<TResult>(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentException("The given data is null or empety", nameof(data));
            }

            var geoJsonSerializer = GeoJsonSerializer.Create();
            using var geoJsonStringReader = new StringReader(data);
            using var geoJsonJsonReader = new JsonTextReader(geoJsonStringReader);
            var spatial = geoJsonSerializer.Deserialize<TResult>(geoJsonJsonReader);
            return spatial;
        }

        public static string SerializeSpatialData<TSpatial>(this TSpatial spatial)
        {
            if(spatial == null)
            {
                throw new ArgumentNullException(nameof(spatial), $"The spatial data of type '{spatial.GetType().Name}' is null.");
            }

            var geoJsonSerializer = GeoJsonSerializer.Create();
            var stringBuilder = new StringBuilder();
            using var geoJsonStringWriter = new StringWriter(stringBuilder);
            using var geoJsonJsonWriter = new JsonTextWriter(geoJsonStringWriter);
            geoJsonSerializer.Serialize(geoJsonJsonWriter, spatial);

            var geoJson = stringBuilder.ToString();
            return geoJson;
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
