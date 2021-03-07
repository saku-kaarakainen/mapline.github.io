using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mapline.Web.Data
{
    public class Filter
    {
        #region .ctor
        public Filter() { }

        public Filter(long id) => this.Id = id;

        public Filter(string name, bool @checked)
        {
            LanguageFilters = new HashSet<LanguageFilter>();
            Name = name;
            Checked = @checked;                   
        }
        #endregion

        public long Id { get; set; } 
        public string Name { get; set; }

        [NotMapped]
        public bool? Checked { get; set; }

        public virtual ICollection<LanguageFilter> LanguageFilters { get; set; }
    }
}
