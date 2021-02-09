using System;
using System.Linq;
using System.Threading.Tasks;
using Mapline.Web.Controllers;
using Mapline.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

// https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-5.0
namespace Mapline.Tests.WebTests.ControllerTests
{
    public class MapControllerTests
    {
        private IDbContextFactory<MaplineDbContext> contextFactory;

        public MapControllerTests()
        {
            this.contextFactory = new TestDbContextFactory();
        }

        [Theory]
        [InlineData(data: new object[] { "Test1", -2000 })] 
        [InlineData(data: new object[] { "Test2", -1000 })]
        public async Task Test_Get(string name, int year)
        {
            // Arrange
            var loggerMock = new Mock<ILogger<MapController>>();
            var controller = new MapController(loggerMock.Object, this.contextFactory);

            // Act
            var result = await controller.Get();
            var language = result.Where(l => l.Name == name).First();

            // Assert
            Assert.Equal(year, language.YearStart);
        }
    }
}
