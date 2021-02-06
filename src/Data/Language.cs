namespace mapline.Data
{
    using NetTopologySuite.Geometries;

    /// <remarks>Note: This is being used as EF Class</remarks>
    public partial class Language
    {        
        public long Id { get; set; }

        public string StringIdentifier { get; set; }

        public Geometry Area { get; set; }

        public int? StartDate { get; set; }

        public int? EndDate { get; set; }

        public string Features { get; set; }

        public string AdditionalDetails { get; set; }
    } 
}

namespace mapline.Data.Magic
{
    using NetTopologySuite.Features;

    /// <summary>
    /// Represents the magic version of <see cref="Data.Language"/>.
    /// </summary>
    /// <remarks>
    /// Magic classes are used to deserialize classes, which can't be deserialized as is.
    /// </remarks>
    public partial class Language
    {
        public long Id { get; set; }

        public string StringIdentifier { get; set; }

        public FeatureCollection Area { get; set; }

        public int? StartDate { get; set; }

        public int? EndDate { get; set; }

        public object Features { get; set; }

        public object AdditionalDetails { get; set; }

        public static implicit operator Data.Language(Magic.Language magicClass)
        {
            return new Data.Language
            {
                Id = magicClass.Id,
                StringIdentifier = magicClass.StringIdentifier,
                StartDate = magicClass.StartDate,
                EndDate = magicClass.EndDate,
                Area = null,
                Features = magicClass.Features.ToString(),
                AdditionalDetails =magicClass.AdditionalDetails.ToString()
            };
        }
    }
}
