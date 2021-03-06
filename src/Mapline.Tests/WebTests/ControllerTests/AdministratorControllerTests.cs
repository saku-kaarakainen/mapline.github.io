using Mapline.Tests.Helpers;
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
        private const string polygonPath = "./../../../data/";
        private readonly Mock<ILanguageHelper> helperMock;
        private readonly Mock<ILogger<AdministratorController>> loggerMock;
        private readonly TestDbContextFactory contextFactory;

        public AdministratorControllerTests()
        {
            this.contextFactory = new TestDbContextFactory();
            this.loggerMock = new Mock<ILogger<AdministratorController>>();
            this.helperMock = new Mock<ILanguageHelper>();
            this.helperMock
                .Setup(h => h.LanguagePathFolder)
                .Returns("./../../../../../data/Language/");
        }

        // Uses "production" data as in-memory
        [Theory]
        [InlineData(data: new object[] { "feature.geojson", -350, 1750, "test" })]
        public async Task Test_Post_SaveToDatabase(string filename, int? yearStart, int? yearEnd, string name)
        {
            // Arrange
            var language = new SaveLanguageModel
            {
                GeoJson = System.IO.File.ReadAllText($"{polygonPath}/{filename}"),
                YearStart = yearStart,
                YearEnd = yearEnd,
                Name = name
            };

            var contextFactory = new InMemoryContextFactory();
            var controller = new AdministratorController(this.loggerMock.Object, contextFactory, this.helperMock.Object);

            // Act
            var result = await controller.SaveToDatabase(language);

            // Assert
            var viewResult = Assert.IsType<OkResult>(result);
        }

        [Theory]
        [InlineData(data: new object[] { "feature.geojson", -350, 1750, "test" })]
        public async Task Test_Post_SaveToFile(string filename, int? yearStart, int? yearEnd, string name)
        {
            // Arrange
            var language = new SaveLanguageModel
            {
                GeoJson = System.IO.File.ReadAllText($"{polygonPath}/{filename}"),
                YearStart = yearStart,
                YearEnd = yearEnd,
                Name = name
            };

            var controller = new AdministratorController(this.loggerMock.Object, this.contextFactory, this.helperMock.Object);

            // Act
            var result = await controller.SaveToFile(language);

            // Assert
            var viewResult = Assert.IsType<OkResult>(result);
        }
    }
}
