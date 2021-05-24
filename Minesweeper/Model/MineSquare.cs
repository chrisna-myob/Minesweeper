using System;

namespace Minesweeper
{
    public class MineSquare : ISquare
    {
        private string _character = "*";
        private bool _show = false;

        public bool CanShow => _show;

        public void SetSquareToShow()
        {
            _show = true;
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
    }
}
