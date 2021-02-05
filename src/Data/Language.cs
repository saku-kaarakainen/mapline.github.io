using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace mapline.Data
{
    /// <remarks>Note: This is being used as EF Class</remarks>
    public partial class Language
    {
        
        public long Id { get; set; }

        // public DbSet<Language> Parents { get; set; }
        // public LanguageParentChild ParentChild { get; set; }

        /// <summary>
        /// Represents the area in GeoJson format.
        /// </summary>
        public Geometry Area { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Features { get; set; }

        public string AdditionalDetails { get; set; }
    }
}
