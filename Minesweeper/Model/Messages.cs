using System.Collections.Generic;
using System;

namespace Minesweeper
{
    public enum GameState
    {
        PLAY, QUIT, LOSE, WIN
    }

    public static class Messages
    {
        public static Dictionary<GameState, string> gameResult = new Dictionary<GameState, string>() {
            { GameState.QUIT, "You have quit the game." },
            { GameState.WIN, "You've won the game :)" },
            { GameState.LOSE, "You've lost :(" }
        };
        public static string Welcome = "Welcome to Minesweeper!";
        public static string EnterDimension = "Please enter the dimensions of your field row,column: ";
        public static string EnterCoordinate = "Please enter a coordinate x,y or q to quit: ";
    }
}
