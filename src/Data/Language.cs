using NetTopologySuite.Geometries;

namespace mapline.Data
{
    /// <remarks>Note: This is being used as EF Class</remarks>
    public partial class Language
    {        
        public long Id { get; set; }

        public string StringIdentifier { get; set; }

        public Polygon Area { get; set; }

        public int? YearStart { get; set; }

        public int? YearEnd { get; set; }

        public int? YearCurrent { get; set; }

        public string Features { get; set; }

        public string AdditionalDetails { get; set; }
    } 
}