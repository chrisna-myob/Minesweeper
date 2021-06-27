using System;
using Xunit;
using Minesweeper;

namespace MinesweeperTests
{
    public class MineSquareTests
    {
        private const string MINE_CHARACTER = "*";
        private readonly MineSquare _square;

        public MineSquareTests()
        {
            _square = new MineSquare();
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
        public void HasMine_ReturnTrue()
        {
            var actual = _square.HasMine();

            Assert.True(actual);
        }

        [Fact]
        public void GetSquareValue_ReturnMineCharacter()
        {
            var actual = _square.GetSquareValue();

            Assert.Equal(MINE_CHARACTER, actual);
        }

        [Theory]
        [InlineData(View.PLAYER, " . |")]
        [InlineData(View.ADMIN, " * |")]
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

            Assert.Equal(" * |", actual);
        }

    }
}
