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

        // Test that data is saved.
        [Theory]
        [InlineData(data: new object[] { 1800, 2021, "Test" })]
        public async Task Test_CreateDbContextAndBuildModel(int start, int end, string name)
        {
            var options = InMemoryDbContextOptionsBuilder.GetDefault();
            var data = TestDataBuilder.CreateDefault();

            using var context = new MaplineDbContext(options) 
            {
                DataBuilder = data 
            };

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Languages.Add(new Language
            {
                YearStart = start,
                YearEnd = end,
                Name = name
            });

            var saveChangesResult = await context.SaveChangesAsync();

            Assert.Equal(expected: 1, actual: saveChangesResult);
        }
    }
}
