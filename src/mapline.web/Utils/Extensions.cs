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
    public static class Extensions
    {
        public static string ToGeoJson(this Geometry geometry)
        {
            if (geometry == null)
                throw new ArgumentNullException(nameof(geometry));

            using var stringWriter = new StringWriter();
            using var jsonWriter = new JsonTextWriter(stringWriter);
            GeoJsonSerializer.Create().Serialize(jsonWriter, geometry);

            // "{"type":"Polygon","coordinates":[[[39.46289062499999,54.36775852406841],[38.86962890625,52.335339071889386],[47.43896484375,51.09662294502995],[48.8232421875,52.81604319154934],[43.4619140625,54.62297813269033],[39.46289062499999,54.36775852406841]]]}"

            var result = stringWriter.ToString();

            return result;
        }

        public static string ToGeoJsonFeature(this Geometry geometry)
        {
            var geometryJson = ToGeoJson(geometry);

            return @$"{{
    ""type"": ""Feature"",
    ""geometry"": {geometryJson}
}}";
        }

        public static Geometry GeoJsonStringToGeometry(this string geoJson)
        {
            using var stringReader = new StringReader(geoJson);
            using var jsonReader = new JsonTextReader(stringReader);
            return GeoJsonSerializer.Create().Deserialize<Geometry>(jsonReader);
        }
    }
}
