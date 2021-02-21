using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mapline.Web.Data
{
    public class Filter
    {
        public Filter(string name, bool @checked)
        {
            Name = name;
            Checked = @checked;
            LanguageFilters = new HashSet<LanguageFilter>();
        }

        public long Id { get; set; } 
        public string Name { get; set; }

        [NotMapped]
        public bool? Checked { get; set; }

        public ICollection<LanguageFilter> LanguageFilters { get; set; }
    }
}
