using System;
using Xunit;
using Minesweeper;

namespace MinesweeperTests
{
    public class MineTrialTests
    {
        private const string MINE_CHARACTER = "*";

        [Fact]
        public void ShowSquare_ReturnFalse()
        {
            var square = new MineSquare();

            var actual = square.CanShow;

            Assert.False(actual);
        }

        [Fact]
        public void ShowSquare_ReturnTrue()
        {
            var square = new MineSquare();
            square.SetSquareToShow();

            var actual = square.CanShow;

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
        public void GetSquareCharacter_ReturnMineCharacter()
        {
            var square = new MineSquare();

            var actual = square.GetSquareValue();

            Assert.Equal(MINE_CHARACTER, actual);
        }
    }
}
