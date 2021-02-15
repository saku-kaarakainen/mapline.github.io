using Mapline.Migrations;
using System.Linq;
using Xunit;

namespace Mapline.Tests.Migration
{
    public class InitialMigrationTests
    {
        private readonly InitialMigration initialMigration;

        public InitialMigrationTests()
        {
            // Arrange
            this.initialMigration = new InitialMigration();
        }

        [Fact]
        public void Test_Up()
        {
            // Act
            var up = this.initialMigration.UpOperations.Last();

            // Assert
            Assert.NotNull(up);
        }

        [Fact]
        public void Test_Down()
        {
            // Act
            var down = this.initialMigration.DownOperations.Last();

            // Assert
            Assert.NotNull(down);
        }
    }
}
