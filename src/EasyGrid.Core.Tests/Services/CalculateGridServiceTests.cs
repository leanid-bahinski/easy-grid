using EasyGrid.Common.Models;
using EasyGrid.Core.Services;
using EasyGrid.Core.Services.Implementation;

namespace EasyGrid.Core.Tests.Services
{
    public class CalculateGridServiceTests
    {
        private const double Eps = 0.00000001;
        private readonly ICalculateGridService _sut = new CalculateGridService();

        [Test]
        public void CreateGrid_ThrowsArgumentException_WhenSquareSizeLessThan1()
        {
            // Arrange
            const double minLat = 51.1112298300;
            const double minLon = 17.0246413490;
            const double maxLat = 51.1146470049;
            const double maxLon = 17.0245422900;
            const int squareSize = 0;

            // Act and Assert
            Assert.Throws<ArgumentException>(() => _sut.CreateGrid(minLat, minLon, maxLat, maxLon, squareSize));
        }

        [Test]
        [TestCase(51.1146470049, 17.0246413490, 51.1146470049, 17.0389497218, 500, TestName = "Same Latitude")]
        [TestCase(51.1080690485, 17.0246413490, 51.1146470049, 17.0246413490, 500, TestName = "Same Longitude")]
        [TestCase(51.1133639100, 17.0246413490, 51.1146470049, 17.0265593100, 500, TestName = "Grid Does Not Fit")]
        [TestCase(51.1134719600, 17.0246413490, 51.1146470049, 17.0317520700, 500, TestName = "Grid Does Not Fit By Latitude")]
        [TestCase(51.1112298300, 17.0246413490, 51.1146470049, 17.0245422900, 500, TestName = "Grid Does Not Fit By Longitude")]
        [TestCase(51.1101222300, 17.0246413490, 51.1146470049, 17.0153584000, 500, TestName = "Min.Lon more than Max.Lon")]
        [TestCase(51.1197655500, 17.0246413490, 51.1146470049, 17.0316233200, 500, TestName = "Min.Lat more than Max.Lat")]
        public void CreateGrid_WhenGridDoesNotFit_ReturnsEmptyGrid(double minLat, double minLon, double maxLat, double maxLon, int squareSize)
        {
            // Act
            var grid = _sut.CreateGrid(minLat, minLon, maxLat, maxLon, squareSize);

            // Assert
            Assert.IsNotNull(grid);
            Assert.IsEmpty(grid);
        }

        [Test]
        [TestCase(51.1080690485, 17.0246413490, 51.1146470049, 17.0389497218, 500, 3, 2, TestName = "Fits perfectly (500)")]
        [TestCase(51.1080690485, 17.0246413490, 51.1146470049, 17.0389497218, 250, 5, 4, TestName = "Fits perfectly (250)")]
        [TestCase(51.1101509390, 17.0246413490, 51.1146470049, 17.0366873296, 500, 3, 2, TestName = "Fits correctly in the wrong bounds (500)")]
        [TestCase(51.0899062424, 16.9842876943, 51.1312700490, 17.0887865763, 100, 74, 47, TestName = "Fits correctly in the wrong bounds (100)")]
        public void CreateGrid_WhenGridIsValid_ReturnsCorrectGridSize(double minLat, double minLon, double maxLat, double maxLon, int squareSize, int expectedWidth, int expectedHeight)
        {
            // Act
            var grid = _sut.CreateGrid(minLat, minLon, maxLat, maxLon, squareSize);

            // Assert
            Assert.IsNotNull(grid);
            Assert.That(grid.GetLength(0), Is.EqualTo(expectedWidth));
            Assert.That(grid.GetLength(1), Is.EqualTo(expectedHeight));
        }

        [Test]
        public void CreateGrid_WhenGridIsValid_ReturnsCorrectGrid()
        {
            // Arrange
            const double minLat = 51.1101509390014;
            const double minLon = 17.0246413490296;
            const double maxLat = 51.1146470049437;
            const double maxLon = 17.038951114034532;
            const int squareSize = 500;
            var expectedGrid = new GeoPoint[,]
            {
                { new (51.1146470049437, 17.0246413490296), new (51.1101509390014, 17.0246413490296) },
                { new (51.1146470049437, 17.031796231532066), new (51.1101509390014, 17.031795535442786) },
                { new (51.1146470049437, 17.038951114034532), new (51.1101509390014, 17.03894972185597) }
            };

            // Act
            var grid = _sut.CreateGrid(minLat, minLon, maxLat, maxLon, squareSize);

            // Assert
            Assert.IsNotNull(grid);
            for (var j = 0; j < grid.GetLength(1); j++)
            {
                for (var i = 0; i < grid.GetLength(0); i++)
                {
                    Assert.That(Math.Abs(expectedGrid[i, j].Lat - grid[i, j].Lat), Is.LessThan(Eps), $"Latitude values are different: {expectedGrid[i, j].Lat} != {grid[i, j].Lat}");
                    Assert.That(Math.Abs(expectedGrid[i, j].Lon - grid[i, j].Lon), Is.LessThan(Eps), $"Longitude values are different: {expectedGrid[i, j].Lon} != {grid[i, j].Lon}");
                }
            }
        }
    }
}