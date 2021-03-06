using Mapline.Tests.Helpers;
using Mapline.Web.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mapline.Tests.WebTests.DataTests
{
    public class MaplineDbContextTests 
    {
        private readonly InMemoryContextFactory factory;

        public MaplineDbContextTests()
        {

        }

        [Fact]
        public void Test_CreateDbContextAndBuildModel()
        {
            var options = InMemoryDbContextOptionsBuilder.GetDefault();
            var context = new MaplineDbContext(options);

            Assert.NotNull(context);
        }
    }
}
