using EasyGrid.Common.Models;
using EasyGrid.Utils.Converters.Implementation;

namespace EasyGrid.Utils.Tests.Converters
{
    public class GeoPointsToGpxConverterTests
    {
        [Test]
        public void ConvertToGpx_ShouldReturnCorrectGpx()
        {
            // Arrange
            var grid = new GeoPoint[,]
            {
                { new (51.1146470049437, 17.0246413490296), new (51.1101509390014, 17.0246413490296) },
                { new (51.1146470049437, 17.031796231532066), new (51.1101509390014, 17.031795535442786) },
                { new (51.1146470049437, 17.038951114034532), new (51.1101509390014, 17.03894972185597) }
            };
            var expected = "";

            // Act
            var actual = GeoPointsToGpxConverterWrapper.ConvertToGpx(grid);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}