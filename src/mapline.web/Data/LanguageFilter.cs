using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mapline.Web.Data
{
    public class LanguageFilter
    {
        public long LanguageId { get; set; }
        public Language Language { get; set; }

        public long FilterId { get; set; }
        public Filter Filter { get; set; }
    }
}
