using NetTopologySuite.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mapline.Web.Models
{
    public class SaveLanguageModel : Mapline.Web.Data.Language
    {
        // TODO: Rename the original features
        public FeatureCollection GeoJsonFeatures { get; set; }

    }
}
