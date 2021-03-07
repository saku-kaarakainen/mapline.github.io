using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mapline.Web.Data.Building
{
    public class DataBuilder : IDataBuilder
    {
        public DataBuilder() { }
        public DataBuilder(IEnumerable<Language> languages)  => this.Languages = languages ?? throw new ArgumentNullException(nameof(languages));

        public IEnumerable<Language> Languages { get; set; }
    }
}
