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
        public const string QUIT = "q";

        public static Dictionary<GameState, string> gameResult = new Dictionary<GameState, string>() {
            { GameState.QUIT, "You have quit the game.\n" },
            { GameState.WIN, "You've won the game :)\n" },
            { GameState.LOSE, "You've lost :(\n" }
        };

        public static Dictionary<string, double> mineDifficultyPercentage = new Dictionary<string, double>() {
            { "EASY", 0.1 }
        };

        public static string Welcome = "Welcome to Minesweeper\n";
        public static string EnterDimension = "Please enter the dimensions of your field row,column: ";
        public static string EnterCoordinate = "Please enter a coordinate x,y or q to quit: ";
        public static string Difficulty = "Please enter difficulty(EASY, INTERMEDIATE, EXPERT) : ";
    }
}
