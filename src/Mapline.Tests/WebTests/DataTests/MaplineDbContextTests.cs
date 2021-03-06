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
            // TODO: Replace with the actual data?
            this.factory = new InMemoryContextFactory();
        }

        [Fact]
        public void Test_CreateDbContextAndBuildModel()
        {
            var context = this.factory.CreateDbContext();

            Assert.NotNull(context);
        }
    }
}
