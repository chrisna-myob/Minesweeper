using System;

namespace Minesweeper
{
    public class SafeSquare : ISquare
    {
        private bool _canBeDisplayed = false;
        private string _hint = "0";

        public bool CanBeDisplayed => _canBeDisplayed;

        public void AddHint(int hint)
        {
            _hint = hint.ToString();
        }

        public void Uncover()
        {
            _canBeDisplayed = true;
        }

        public bool HasMine()
        {
            return false;
        }

        public string GetSquareValue()
        {
            return _hint;
        }
    }
}
