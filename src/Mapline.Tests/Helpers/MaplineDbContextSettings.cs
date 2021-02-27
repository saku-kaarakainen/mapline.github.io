using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapline.Tests.Helpers
{
    public class MaplineDbContextSettings
    {
        public string DatabaseName { get; set; } = "Mapline";
        public bool InitializeData { get; set; } = false;
        public bool CreateModel { get; set; } = false;
        public string LanguageFolder { get; set; } = ".\\..\\..\\..\\..\\..\\data\\Language";
    }
}
