using Mapline.Web.Data;
using Mapline.Web.Data.Building;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapline.Tests.Helpers
{
    public class TestDataBuilder : IDataBuilder
    {
        public TestDataBuilder(params Language[] languages) => Languages = languages;
        public TestDataBuilder(IEnumerable<Language> languages) => Languages = languages;


        public IEnumerable<Language> Languages { get; set; }

        public static IDataBuilder CreateDefault()
        {
            return new TestDataBuilder(
                new Language
                {
                    Name = "Test1",
                    YearStart = -2000,
                    YearEnd = 0000
                },
                new Language
                {
                    Name = "Test2",
                    YearStart = -1000,
                    YearEnd = 1000
                }
            );
        }
   
    }
}
