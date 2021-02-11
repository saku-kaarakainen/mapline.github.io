using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mapline.Web.Models
{
    public class Filter
    {
        public Filter() { }
        public Filter(string name, bool @checked)
        {
            Name = name;
            Checked = @checked;
        }

        public string Name { get; set; }
        public bool Checked { get; set; }
    }
}
