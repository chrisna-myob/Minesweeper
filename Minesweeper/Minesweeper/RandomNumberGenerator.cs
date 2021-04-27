using System;
namespace Minesweeper
{
    public class RandomNumberGenerator : INumberGenerator
    {
        private Random _random;

        public RandomNumberGenerator()
        {
            _random = new Random();
        }

        public int GetRandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}
