﻿using Minesweeper.Factory;
using Minesweeper.Repository;
using Minesweeper.Service;

namespace Minesweeper
{
    public class GameService
    {
        private readonly IIO _io;
        private FieldService _fieldService;
        private DimensionFactory _dimensionFactory;
        private CoordinateFactory _coordinateFactory;
        private Validation _validation;
        private MineCoordinateFactory _mineCoordinateFactory;
        private GridFactory _gridFactory;
        private CoordinateService _coordinateService;

        public GameService(FieldService fieldService, IIO io, DimensionFactory dimensionFactory, CoordinateFactory coordinateFactory, Validation validation, MineCoordinateFactory mineCoordinateFactory, GridFactory gridFactory, CoordinateService coordinateService)
        {
            _io = io;
            _dimensionFactory = dimensionFactory;
            _coordinateFactory = coordinateFactory;
            _validation = validation;
            _mineCoordinateFactory = mineCoordinateFactory;
            _fieldService = fieldService;
            _gridFactory = gridFactory;
            _coordinateService = coordinateService;
        }

        public DifficultyLevel GetDifficulty(string input)
        {
            _validation.IsDifficultyLevelValid(input);

            switch (input)
            {
                case "EASY":
                    return DifficultyLevel.EASY;
                case "INTERMEDIATE":
                    return DifficultyLevel.INTERMEDIATE;
                case "EXPERT":
                    return DifficultyLevel.EXPERT;
                default:
                    return DifficultyLevel.EASY;
            }
        }

        public string GetTrimmedUserInput()
        {
            var input = _io.GetUserInput();
            return input.Trim();
        }

        public void SetUpField(DifficultyLevel difficulty, string userDimension)
        {
            var dimension = _dimensionFactory.MakeDimension(userDimension, _validation);

            var mineCoordinates = _mineCoordinateFactory.MakeUniqueMineCoordinates(difficulty, dimension);

            var grid = _gridFactory.MakeGrid(dimension, mineCoordinates);

            var _field = new Field(dimension, mineCoordinates, grid);

            _fieldService.SetField(_field);
        }

        public GameState GameRound(string userInput)
        {
            switch(userInput)
            {
                case GlobalGameVariables.QUIT:
                    return GameState.QUIT;
                case GlobalGameVariables.ADMIN:
                    return GameState.ADMIN;
                default:
                    HandleInput(userInput);
                    return GetGameStatus();
            }
        }

        public void DisplayMessage(string message)
        {
            _io.Write(message);
        }

        public void DisplayBoard(GameState state)
        {
            var view = (state == GameState.ADMIN ? View.ADMIN : View.PLAYER);
            _io.DisplayBoard(_fieldService.BoardToString(view));
        }

        private void HandleInput(string userInput)
        {
            var coordinate = MakeCoordinate(userInput);
            _fieldService.SetAdjacentCoordinatesToBeUncovered(coordinate, _coordinateService);
        }

        private Coordinate MakeCoordinate(string input)
        {
            var dimension = _fieldService.GetDimension();
            var coord = _coordinateFactory.MakeCoordinate(dimension, input, _validation);
            _fieldService.CoordinateHasAlreadyBeenUncovered(coord);
            return coord;
        }

        private GameState GetGameStatus()
        {
            if (_fieldService.HasWon()) return GameState.WIN;
            else if (_fieldService.HasLost()) return GameState.LOSE;
            return GameState.PLAY;
        }
    }
}