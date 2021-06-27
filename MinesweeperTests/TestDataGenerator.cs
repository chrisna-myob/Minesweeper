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

        public static IEnumerable<object[]> GetAdjacentCoordinates()
        {
            yield return new object[]
            {
                new Coordinate(1, 1), new List<Coordinate> { new Coordinate(0, 0), new Coordinate(0, 1), new Coordinate(0, 2), new Coordinate(1, 0), new Coordinate(1, 2), new Coordinate(2, 0), new Coordinate(2, 1), new Coordinate(2, 2) }
            };

            yield return new object[]
            {
                new Coordinate(0, 0), new List<Coordinate> { new Coordinate(0, 1), new Coordinate(1, 0), new Coordinate(1, 1) }
            };

            yield return new object[]
            {
                new Coordinate(0, 2), new List<Coordinate> { new Coordinate(0, 1), new Coordinate(1, 1), new Coordinate(1, 2) }
            };

            yield return new object[]
            {
                new Coordinate(2, 0), new List<Coordinate> { new Coordinate(1, 0), new Coordinate(1, 1), new Coordinate(2, 1) }
            };

            yield return new object[]
            {
                new Coordinate(2, 2), new List<Coordinate> { new Coordinate(1, 1), new Coordinate(1, 2), new Coordinate(2, 1) }
            };

            yield return new object[]
            {
                new Coordinate(0, 1), new List<Coordinate> { new Coordinate(0, 0), new Coordinate(0, 2), new Coordinate(1, 0), new Coordinate(1, 1), new Coordinate(1, 2) }
            };

            yield return new object[]
            {
                new Coordinate(1, 0), new List<Coordinate> { new Coordinate(0, 0), new Coordinate(0, 1), new Coordinate(1, 1), new Coordinate(2, 0), new Coordinate(2, 1) }
            };

            yield return new object[]
            {
                new Coordinate(1, 2), new List<Coordinate> { new Coordinate(0, 1), new Coordinate(0, 2), new Coordinate(1, 1), new Coordinate(2, 1), new Coordinate(2, 2) }
            };

            yield return new object[]
            {
                new Coordinate(2, 1), new List<Coordinate> { new Coordinate(1, 0), new Coordinate(1, 1), new Coordinate(1, 2), new Coordinate(2, 0), new Coordinate(2, 2) }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}