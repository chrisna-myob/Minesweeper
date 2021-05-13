using System;
namespace Minesweeper
{
    public class SafeSquare : ISquare
    {
        private bool _show = false;
        private string _hint = "0";

        public bool CanShow => _show;

        public void AddHint(int hint)
        {
            _hint = hint.ToString();
        }

        public void SetSquareToShow()
        {
            _show = true;
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
