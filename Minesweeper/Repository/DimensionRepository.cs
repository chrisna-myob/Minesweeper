using System;
using Microsoft.Extensions.Logging;
using Minesweeper.Repository.Interfaces;

namespace Minesweeper.Model
{
    public class DimensionRepository : IDimensionRepository
    {
        public Dimension MakeDimension(string input)
        {
            Validate.IsFieldDimensionInputValid(input);

            var dimensionArray = input.Split(',');

            var row = Int32.Parse(dimensionArray[0]);
            var column = Int32.Parse(dimensionArray[1]);

            return new Dimension(row, column);
        }
    }
}
