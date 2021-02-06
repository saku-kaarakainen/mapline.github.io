using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetTopologySuite.Features;
using EntityModel = mapline.Data.Language;

namespace mapline.Data.Deserializable
{
    public partial class Language
    {
        public string StringIdentifier { get; set; }

        public int? YearStart { get; set; }

        public int? YearEnd { get; set; }

        public int? YearCurrent { get; set; }

        public object Features { get; set; }

        public object AdditionalDetails { get; set; }

        public static explicit operator EntityModel(Language magicClass)
             => magicClass == default ? default : new EntityModel
             {
                 Id = default,
                 StringIdentifier = magicClass.StringIdentifier,
                 YearStart = magicClass.YearStart,
                 YearEnd = magicClass.YearEnd,
                 YearCurrent = magicClass.YearCurrent,
                 Area = null,
                 Features = magicClass.Features.ToString(),
                 AdditionalDetails = magicClass.AdditionalDetails.ToString()
             };
    }
}