# Minesweeper

Minesweeper is a single-player puzzle game where the aim is to find all the mines within an XxY field with the aid of hints.

## Requirements

- Game can take player input via the console
- Player can specify the dimension of a 2D field (row, column)
- Dimension of field is valid such that 1 <= row <= 100 and 1 <= y <= 100
- All squares are initially represented by “.”
- A square is either a:
    - safe square which is blank or represented by a numerical hint
    - a mine which is represented by “*”
- Mines are placed randomly on the field
- Player can input a coordinate (e.g. 1,3) to reveal a square
- Coordinate (x,y) is valid such that 1 <= x <= row and 1 <= y <= column and
- If input is invalid, print an error message and ask the player to input again
- Player can quit 
- Uncovered board can be shown
- Satisfies rules for the game
ADDITIONAL: Difficulty Level

## Rules

-   Player loses if they uncover a mine
-   Player wins if all remaining covered squares are mines
-   Game continues if square displays either a hint or if the square is blank it also shows adjacent squares that are also blank

## Running the Application

