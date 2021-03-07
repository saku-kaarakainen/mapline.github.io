using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mapline.Web.Data
{
    public class LanguageFilter
    {
        public LanguageFilter() { }

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

        public long Id { get; set; }

        public long LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public virtual Language Language { get; set; }

        public long FilterId { get; set; }
        [ForeignKey("FilterId")]
        public virtual Filter Filter { get; set; }
    }
}
