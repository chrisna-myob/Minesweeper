using System;

namespace Minesweeper
{
    public interface INumberGenerator
    {
        int GetRandomNumber(int min, int max);
    }
}
