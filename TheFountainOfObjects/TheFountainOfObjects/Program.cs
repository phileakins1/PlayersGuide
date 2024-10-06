// See https://aka.ms/new-console-template for more information

// Ignore Spelling: Amarok Amarok

// Experimental static usings.
using TheFountainOfObjects.GameObjects;
using TheFountainOfObjects.GamePlay;
using TheFountainOfObjects.Utilities;

// Using The Book with the C# 11 Expansion
// C# Players Guide Fountain of Objects Challenge. - 1000 XP at stake

// Basic challenge completed; plus:
// Expansion 1 - Three sizes of playing board - completed
// Expansion 2 - Pit challenge (accommodating different size boards) - completed
// Expansion 3 - Maelstrom challenge (accommodating different size boards) - completed
// Expansion 4 - Amarok challenge (accommodating different size boards) - completed
// Expansion 5 - Bows and arrows - completed for 4 cardinal points
// Expansion 6 - Instructions and help - Completed ...

// Refactored Version 30th September 2024.

// TODO Refactor the program into separate class based files / name spaces / directory structure

// Create the game board
(int _row, int _columns) = SelectCavernSize.GetCavernSize();
ICave[,] _caves = new ICave[_row, _columns];

// Initialize the board positions sending the _caves array in a class declaration parameter
BoardObjectPositions playArea = new(_caves);

// Window title and Instructions
ScreenAndInstructions.StartupScreens();

// Fill the array with background cave objects
_caves = playArea.FillEmptyCaves();

// Create the game board sending a parameter of the array containing  'empty' caves
PlayTheGame play = new(_caves);

// Start the game loop
play.GetPlayerChoice();

