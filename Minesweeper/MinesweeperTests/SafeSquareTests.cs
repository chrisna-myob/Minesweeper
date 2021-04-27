using System;
using Minesweeper;
using Xunit;
using Moq;

namespace MinesweeperTests
{
    public class SquareTests
    {
        private const string INITIAL_CHARACTER = ".";

        [Fact]
        public void SafeSquare_ReturnInititalCharacter()
        {
            var square = new SafeSquare();

            var actual = square.InitialCharacter;

            Assert.Equal(INITIAL_CHARACTER, actual);
        }

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
        public void GetSquareCharacter_ReturnStringHint()
        {
            var square = new SafeSquare();
            square.AddHint(1);

            var actual = square.RevealSquare();

            Assert.Equal("1", actual);
        }
    }
}
