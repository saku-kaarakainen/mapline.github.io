using Mapline.Web.Controllers;
using Mapline.Web.Data;
using Mapline.Web.Models;
using Mapline.Web.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mapline.Tests.WebTests.ControllerTests
{
    public class AdministratorControllerTests
    {
        [Theory]
        [InlineData(data: new object[] { "simplepolygon.geojson", -350, 1750, "test" })]
        public async Task Test_Post_SaveToDatabase(string filename, int? yearStart, int? yearEnd, string name)
        {
            // Arrange
            var language = new SaveLanguageModel
            {
                Area = DataHelper.CreateFromFile(filename).ToGeometry(),
                YearStart = yearStart,
                YearEnd = yearEnd,
                Name = name
            };
            var contextFactory = new TestDbContextFactory();
            var loggerMock = new Mock<ILogger<AdministratorController>>();
            var helperMock = new Mock<ILanguageHelper>();
            var controller = new AdministratorController(loggerMock.Object, contextFactory, helperMock.Object);

            // Act
            var result = await controller.SaveToDatabase(language);

            // Assert
            var viewResult = Assert.IsType<OkResult>(result);

            // Teardown
            contextFactory.Dispose();
        }

        [Theory]
        [InlineData(data: new object[] { "simplepolygon.geojson", -350, 1750, "test" })]
        public async Task Test_Post_SaveToFile(string filename, int? yearStart, int? yearEnd, string name)
        {
            // Arrange
            var language = new SaveLanguageModel
            {
                GeoJsonFeatures = DataHelper.CreateFromFile(filename),
                YearStart = yearStart,
                YearEnd = yearEnd,
                Name = name
            };

            var contextFactory = new TestDbContextFactory();
            var loggerMock = new Mock<ILogger<AdministratorController>>();
            var helperMock = new Mock<ILanguageHelper>();
            var controller = new AdministratorController(loggerMock.Object, contextFactory, helperMock.Object);

            // Act
            var result = await controller.SaveToFile(language);

            // Assert
            var viewResult = Assert.IsType<OkResult>(result);
        }
    }
}
