using System.Collections.Generic;
using Xunit;
using Minesweeper;
using Moq;

namespace MinesweeperTests
{
    public class GameRoundTests
    {
        [Fact]
        public void Round_InputQ_ReturnQuit()
        {
            var field = new Field(null, 0, null, null);

            var actual = GameRound.Round("q", field);

            Assert.Equal(GameStatus.QUIT, actual);
        }

        [Fact]
        public void Round_ReturnLost()
        {
            var dimension = new Dimension(1, 2);
            var rng = new Mock<INumberGenerator>();
            rng.Setup(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
               .Returns(new Queue<int>(new[] { 1, 0, 0 }).Dequeue);
            var builder = new FieldBuilder(rng.Object);

            var field = builder.CreateField(dimension);

            var actual = GameRound.Round("1,1", field);

            Assert.Equal(GameStatus.LOSE, actual);
        }

        [Fact]
        public void Round_ReturnWin()
        {
            var dimension = new Dimension(1, 2);
            var rng = new Mock<INumberGenerator>();
            rng.Setup(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
               .Returns(new Queue<int>(new[] { 1, 0, 0 }).Dequeue);
            var builder = new FieldBuilder(rng.Object);

            var field = builder.CreateField(dimension);

            var actual = GameRound.Round("1,2", field);

            Assert.Equal(GameStatus.WIN, actual);
        }
    }
}
