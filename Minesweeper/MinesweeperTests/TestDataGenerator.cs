using Minesweeper;
using System.Collections.Generic;
using System.Collections;

namespace MinesweeperTests
{
    public class TestDataGenerator : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { };
        }

        public static IEnumerable<object[]> GetWinningCoordinatesFromDataGenerator()
        {
            yield return new object[]
            {
                1, new Queue<int>(new[] { 0, 0}), 1, 1, new List<Coordinate> { new Coordinate(0,0) }
            };

            yield return new object[]
            {
                2, new Queue<int>(new[] { 0, 0, 1, 0 }), 2, 2, new List<Coordinate> { new Coordinate(0,0), new Coordinate(1, 0) }
            };

            yield return new object[]
            {
                3, new Queue<int>(new[] { 0, 0, 1, 0, 0, 0, 1, 1 }), 2, 2, new List<Coordinate> { new Coordinate(0,0), new Coordinate(1, 0), new Coordinate(1, 1) }
            };

        }

        public static IEnumerable<object[]> GetFieldFromDataGenerator()
        {
            yield return new object[]
            {
                new ISquare[,] { { } }
            };

        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}