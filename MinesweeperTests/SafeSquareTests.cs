using System;
using Minesweeper;
using Xunit;
using Moq;

namespace MinesweeperTests
{
    public class SquareTests
    {
        [Fact]
        public void ShowSquare_ReturnFalse()
        {
            var square = new SafeSquare();

            var actual = square.CanShow;

            Assert.False(actual);
        }

        [Fact]
        public void ShowSquare_ReturnTrue()
        {
            var square = new SafeSquare();
            square.SetSquareToShow();

            var actual = square.CanShow;

            Assert.True(actual);
        }

        [Fact]
        public void HasMine_ReturnFalse()
        {
            var square = new SafeSquare();

            var actual = square.HasMine();

            Assert.False(actual);
        }

        [Fact]
        public void GetSquareValue_ReturnStringHint()
        {
            var square = new SafeSquare();
            square.AddHint(1);

            var actual = square.GetSquareValue();

            Assert.Equal("1", actual);
        }
    }
}
