# Minesweeper

### Requirements

-	The game can take player input via the console 
-	Player can specify the size of a 2D field (row, column)
-	All squares are initially represented by “.” 
-	A square is either a safe square which is represented by a numerical hint or a mine which is represented by “*”
-	Mines are placed randomly on the field 
-	Player can input a coordinate (e.g. 1,3) to reveal the square
-	Coordinate (x,y) is valid such that 1 <= x <= row and 1 <= y <= column 
-	If coordinate is invalid, print an error message and ask the player to input again
-	When revealing the square, it will either reveal hints or a mine 
-	If the square revealed has a hint of 0, it will reveal all the other squares adjacent that also have a hint of 0 and stop when it reaches a square with a hint larger than "0"
-	If the player reveals a mine square, they lose the game 
-	If the only remaining squares that have not been revealed are equal to the original number of mines, the player wins
