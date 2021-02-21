using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mapline.Web.Data
{
    public class LanguageFilter
    {
        public LanguageFilter(Language language = null, Filter filter = null)
        {
            if (language != null)
            {
                this.Language = language;
                this.LanguageId = language.Id;
            }

            if (filter != null)
            {
                this.Filter = filter;
                this.FilterId = filter.Id;
            }
        }

        public long LanguageId { get; set; }
        public Language Language { get; set; }

        public long FilterId { get; set; }
        public Filter Filter { get; set; }
    }
}
