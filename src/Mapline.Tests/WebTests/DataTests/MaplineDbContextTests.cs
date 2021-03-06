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
        private readonly TestDbContextFactory factory;

        public MaplineDbContextTests()
        {
            // TODO: Replace with the actual data?
            this.factory = new TestDbContextFactory();
        }

        [Fact]
        public void Test_CreateDbContextAndBuildModel()
        {
            var context = this.factory.CreateDbContext();

            Assert.NotNull(context);
        }
    }
}
