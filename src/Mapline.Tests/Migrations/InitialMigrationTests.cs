using Mapline.Migrations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

// https://docs.microsoft.com/en-us/ef/core/testing/
namespace Mapline.Tests.Migrations
{
    // Unit testing conventions:
    // https://docs.microsoft.com/en-us/visualstudio/test/unit-test-basics?view=vs-2019

    [TestClass]
    public class InitialMigrationTests
    {
        private InitialMigration initialMigration;

        [TestInitialize]
        public void Initialize()
        {
            this.initialMigration = new InitialMigration();
        }

        [TestMethod]
        public void Test_Up()
        {
            // Arrange

            // Act
            var up = this.initialMigration.UpOperations.Last();

            // Assert
            Assert.IsNotNull(up);

        }

        [TestMethod]
        public void Test_Down()
        {
            // Arrange

            // Act
            var down = this.initialMigration.DownOperations.Last();

            // Assert
            Assert.IsNotNull(down);




        }
    }
}
