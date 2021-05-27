using System;

namespace Minesweeper.Repository.Interfaces
{
    public interface IDimensionRepository
    {
        Dimension MakeDimension(string input);
    }
}