using System;
using System.Collections.Generic;

namespace mapline.Models
{
    public class Language
    {
        public string Identifier { get; set; }

        public IEnumerable<Language> Parents { get; set; } 

        /// <summary>
        /// Represents the area in GeoJson format.
        /// </summary>
        public string Area { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Features { get; set; }

        public string AdditionalDetails { get; set; }
    }
}
