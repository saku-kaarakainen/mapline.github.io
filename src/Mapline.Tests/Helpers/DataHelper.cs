using Mapline.Web.Utils;
using NetTopologySuite.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapline.Tests
{
    public static class DataHelper
    {
        public static FeatureCollection CreateFromFile(string filename)
        {
            var data = System.IO.File.ReadAllText($"./../../../data/{filename}");

            return DataConverter.StringToGeoJson(data);
        }
    }
}
