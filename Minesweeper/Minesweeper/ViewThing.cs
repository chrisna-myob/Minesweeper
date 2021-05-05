using System;
namespace Minesweeper
{
    public class ViewThing
    {
        public void WelcomeToGameMessage(IIO io, string message)
        {
            io.WriteLine(message);
        }
    }
}
