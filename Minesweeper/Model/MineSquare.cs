using System;

namespace Minesweeper
{
    public class MineSquare : ISquare
    {
        private string _character = "*";
        private bool _hasBeenUncovered = false;

        public bool HasBeenUncovered => _hasBeenUncovered;

        public void Uncover()
        {
            _hasBeenUncovered = true;
        }

        public bool HasMine()
        {
            return true;
        }

        public void AddHint(int hint) { }

        public string GetSquareValue()
        {
            return _character;
        }

        public string GetSquareAsString(View view)
        {
            var squareAsString = "";
            switch(view)
            {
                case View.ADMIN:
                    squareAsString += $" {_character} |";
                    break;
                case View.PLAYER:
                    squareAsString += _hasBeenUncovered ? $" {_character} |" : $" . |";
                    break;
            }
            return squareAsString;
        }
    }
}
