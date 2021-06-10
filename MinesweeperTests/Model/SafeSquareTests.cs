using System;
using Minesweeper;
using Xunit;
using Moq;

namespace MinesweeperTests
{
    public class SquareTests
    {
        [Fact]
        public void HasBeenUncovered_ReturnFalse()
        {
            var square = new SafeSquare();

            var actual = square.HasBeenUncovered;

            Assert.False(actual);
        }

        [Fact]
        public void HasBeenUncovered_ReturnTrue()
        {
            var square = new SafeSquare();
            square.Uncover();

            var actual = square.HasBeenUncovered;

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

        [Fact]
        public void GetSquareAsString_InputPlayerViewWithCoveredSquare_ReturnValidString()
        {
            var square = new SafeSquare();

            var actual = square.GetSquareAsString(View.PLAYER);

            Assert.Equal(" . |", actual);
        }

        [Fact]
        public void GetSquareAsString_InputPlayerViewWithUncoveredSquare_ReturnValidString()
        {
            var square = new SafeSquare();
            square.Uncover();

            var actual = square.GetSquareAsString(View.PLAYER);

            Assert.Equal("   |", actual);
        }

        [Fact]
        public void GetSquareAsString_InputPlayerViewWithUncoveredSquareAndHintOfOne_ReturnValidString()
        {
            var square = new SafeSquare();
            square.AddHint(1);
            square.Uncover();

            var actual = square.GetSquareAsString(View.PLAYER);

            Assert.Equal(" 1 |", actual);
        }

        [Fact]
        public void GetSquareAsString_InputAdminViewWWithSquareWithoutHint_ReturnValidString()
        {
            var square = new SafeSquare();

            var actual = square.GetSquareAsString(View.ADMIN);

            Assert.Equal("   |", actual);
        }

        [Fact]
        public void GetSquareAsString_InputAdminViewWWithSquareWithHint_ReturnValidString()
        {
            var square = new SafeSquare();
            square.AddHint(1);

            var actual = square.GetSquareAsString(View.ADMIN);

            Assert.Equal(" 1 |", actual);
        }
    }
}
