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
            return stringWriter.ToString();
        }

        public static Geometry GeoJsonStringToGeometry(this string geoJson)
        {
            using var stringReader = new StringReader(geoJson);
            using var jsonReader = new JsonTextReader(stringReader);
            return GeoJsonSerializer.Create().Deserialize<Geometry>(jsonReader);
        }
    }
}
