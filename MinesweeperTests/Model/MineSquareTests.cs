using System;
using Xunit;
using Minesweeper;

namespace MinesweeperTests
{
    public class MineTrialTests
    {
        private const string MINE_CHARACTER = "*";

        [Fact]
        public void CanBeDisplayed_ReturnFalse()
        {
            var square = new MineSquare();

            var actual = square.HasBeenUncovered;

            Assert.False(actual);
        }

        [Fact]
        public void CanBeDisplayed_ReturnTrue()
        {
            var square = new MineSquare();
            square.Uncover();

            var actual = square.HasBeenUncovered;

            Assert.True(actual);
        }

        [Fact]
        public void HasMine_ReturnTrue()
        {
            var square = new MineSquare();

            var actual = square.HasMine();

            Assert.True(actual);
        }

        [Fact]
        public void GetSquareValue_ReturnMineCharacter()
        {
            var square = new MineSquare();

            var actual = square.GetSquareValue();

            Assert.Equal(MINE_CHARACTER, actual);
        }

        [Fact]
        public void GetSquareAsString_InputPlayerViewWithCoveredSquare_ReturnValidString()
        {
            var square = new MineSquare();

            var actual = square.GetSquareAsString(View.PLAYER);

            Assert.Equal(" . |", actual);
        }

        [Fact]
        public void GetSquareAsString_InputPlayerViewWithUncoveredSquare_ReturnValidString()
        {
            var square = new MineSquare();
            square.Uncover();

            var actual = square.GetSquareAsString(View.PLAYER);

            Assert.Equal(" * |", actual);
        }

        [Fact]
        public void GetSquareAsString_InputAdminView_ReturnValidString()
        {
            var square = new MineSquare();

            var actual = square.GetSquareAsString(View.ADMIN);

            Assert.Equal(" * |", actual);
        }
    }
}
