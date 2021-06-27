using System;

namespace Minesweeper
{
    public class SafeSquare : ISquare
    {
        private bool _hasBeenUncovered = false;
        private string _hint = " ";

        public bool HasBeenUncovered => _hasBeenUncovered;

        public void AddHint(int hint)
        {
            _hint = hint.ToString();
        }

        public void Uncover()
        {
            _hasBeenUncovered = true;
        }

        public bool HasMine()
        {
            return false;
        }

        public string GetSquareValue()
        {
            return _hint;
        }

        public string GetSquareAsString(View view)
        {
            var squareAsString = "";
            switch (view)
            {
                case View.ADMIN:
                    squareAsString += $" {_hint} |";
                    break;
                case View.PLAYER:
                    squareAsString += _hasBeenUncovered ? $" {_hint} |" : $" . |";
                    break;
            }
            return squareAsString;
        }
    }
}
