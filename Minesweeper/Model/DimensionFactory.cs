using System;

namespace Minesweeper.Model
{
    public class DimensionFactory
    {
        public Dimension MakeDimension(string input, Validation validation)
        {
            validation.IsFieldDimensionInputValid(input);

            var dimensionArray = input.Split(',');

            var row = Int32.Parse(dimensionArray[0]);
            var column = Int32.Parse(dimensionArray[1]);

            return new Dimension(row, column);
        }
    }
}
