using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mapline.Web.Data.Building
{
    public interface IDataBuilder
    {
        IEnumerable<Language> Languages { get; set; }
    }
}
