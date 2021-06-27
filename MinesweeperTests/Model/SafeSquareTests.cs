using System;
using Minesweeper;
using Xunit;
using Moq;

namespace MinesweeperTests
{
    public class SafeSquareTests
    {
        private readonly SafeSquare _square;

        public SafeSquareTests()
        {
            _square = new SafeSquare();
        }

        [Fact]
        public void HasBeenUncovered_ReturnFalse()
        {
            var actual = _square.HasBeenUncovered;

            Assert.False(actual);
        }

        [Fact]
        public void HasBeenUncovered_ReturnTrue()
        {
            _square.Uncover();

            var actual = _square.HasBeenUncovered;

            Assert.True(actual);
        }

        [Fact]
        public void HasMine_ReturnFalse()
        {
            var actual = _square.HasMine();

            Assert.False(actual);
        }

        [Fact]
        public void GetSquareValue_ReturnStringHint()
        {
            _square.AddHint(1);

            var actual = _square.GetSquareValue();

            Assert.Equal("1", actual);
        }

        [Theory]
        [InlineData(View.PLAYER, " . |")]
        [InlineData(View.ADMIN, "   |")]
        public void GetSquareAsString_InputViewWithCoveredSquare_ReturnValidString(View view, string expected)
        {
            var actual = _square.GetSquareAsString(view);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetSquareAsString_InputPlayerViewWithUncoveredSquare_ReturnValidString()
        {
            _square.Uncover();

            var actual = _square.GetSquareAsString(View.PLAYER);

            Assert.Equal("   |", actual);
        }

        [Fact]
        public void GetSquareAsString_InputPlayerViewWithUncoveredSquareAndHintOfOne_ReturnValidString()
        {
            _square.AddHint(1);
            _square.Uncover();

            var actual = _square.GetSquareAsString(View.PLAYER);

            Assert.Equal(" 1 |", actual);
        }

        [Fact]
        public void GetSquareAsString_InputAdminViewWWithSquareWithHint_ReturnValidString()
        {
            _square.AddHint(1);

            var actual = _square.GetSquareAsString(View.ADMIN);

            Assert.Equal(" 1 |", actual);
        }
    }
}
