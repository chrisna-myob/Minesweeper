using System;

namespace Minesweeper.Model
{
    public class DimensionRepository
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
