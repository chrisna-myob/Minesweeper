using System;
using Minesweeper;
using Xunit;
namespace MinesweeperTests
{
    public class RandomNumberGeneratorTests
    {
        [Fact]
        public void GetRandomNumber_InputNumber_ReturnNumberBetweenOneAndInputtedNumber()
        {
            var rng = new RandomNumberGenerator();
            var min = 1;
            var max = 5;

            var actual = rng.GetRandomNumber(min, max);

            Assert.True(actual >= min);
            Assert.True(actual <= max);
        }
    }
}
