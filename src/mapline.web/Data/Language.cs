using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mapline.Data
{
    public class Language
    {
        public long Id { get; set; }
        public string Name { get; set; }
        //public Geometry Area { get; set; }
        public int? YearCurrent { get; set; }
        public int? YearStart { get; set; }
        public int? YearEnd { get; set; }
        public string Features { get; set; }
        public string AdditionalDetails { get; set; }
    }
}
