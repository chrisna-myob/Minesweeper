
namespace Minesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = ClassInstanceFactory.GetGameControllerInstance();
            game.Run();
        }
    }
}
