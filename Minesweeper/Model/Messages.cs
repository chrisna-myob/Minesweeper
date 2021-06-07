﻿using System.Collections.Generic;
using System;

namespace Minesweeper
{
    public enum View
    {
        PLAYER, ADMIN
    }

    public enum GameState
    {
        PLAY, QUIT, LOSE, WIN
    }

    public enum DifficultyLevel
    {
        EASY, INTERMEDIATE, EXPERT
    }

    public static class Messages
    {
        public const string QUIT = "q";

        public static Dictionary<GameState, string> gameResult = new Dictionary<GameState, string>() {
            { GameState.QUIT, "You have quit the game.\n" },
            { GameState.WIN, "You've won the game :)\n" },
            { GameState.LOSE, "You've lost :(\n" }
        };

        public static Dictionary<DifficultyLevel, double> mineDifficultyPercentage = new Dictionary<DifficultyLevel, double>() {
            { DifficultyLevel.EASY, 0.1 },
            { DifficultyLevel.INTERMEDIATE, 0.15 },
            { DifficultyLevel.EXPERT, 0.17 }
        };

        public static string Welcome = "Welcome to Minesweeper\n";
        public static string EnterDimension = "Please enter the dimensions of your field row,column: ";
        public static string EnterCoordinate = "Please enter a coordinate x,y or q to quit: ";
        public static string Difficulty = "Please enter difficulty (EASY, INTERMEDIATE, EXPERT): ";
    }
}
