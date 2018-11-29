using System;
using System.Collections.Generic;

namespace BvM
{
    /// <summary>
    /// Beast agent - "BEst Available STrategy" implementation
    /// </summary>
    class Beast
    {
        bool gameOn = true;
        bool playerWon = false;
        bool gameWon = false;

        public string[] lines = new string[8];
        // Line definitions
        // 0 == 1, 2, 3
        // 1 == 4, 5, 6
        // 2 == 7, 8, 9
        // 3 == 1, 4, 7
        // 4 == 2, 5, 8
        // 5 == 3, 6, 9
        // 6 == 1, 5, 9
        // 7 == 3, 5, 7

        public int[,] lineDefs = {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 },
            { 1, 4, 7 },
            { 2, 5, 8 },
            { 3, 6, 9 },
            { 1, 5, 9 },
            { 3, 5, 7 },
            };

        /// <summary>
        /// Selects a move, based on the current state, then returns it to the caller
        /// </summary>
        /// <param name="grid">An int array, representing the game board</param>
        /// <returns>Returns the next move, chosen by Beast</returns>
        public int[] NextMove(int[] grid)
        {
            // TODO : BEAST : Game logic : Should Work
            int turnNumber = GetTurnNumber(grid);
            int[] newGrid = new int[9];
            switch (turnNumber)
            {
                case 1:
                    newGrid = ChooseFirstMove(grid);
                    break;
                case 2:
                    newGrid = ChooseSecondMove(grid);
                    break;
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    newGrid = ChooseResponse(grid);
                    break;
            }
            return newGrid;
        }

        private int[] ChooseResponse(int[] grid)
        {
            CalculateLineStates(grid);
            int nextMove = SelectNextMoveFromLines(grid);
            grid[nextMove] = 1;
            return grid;
        }

        private void CalculateLineStates(int[] grid)
        {
            for (int counter = 0; counter < 8; counter++)
            {
                lines[counter] = GetLineState(grid, counter);
            }
        }

        private int SelectNextMoveFromLines(int[] grid)
        {
            int[] newGrid = new int[9];
            int targetLine = 0;
            int targetCell = 0;
            bool targetFound = false;
            for (int counter = 0; counter <= 7; counter++)
            {
                if (lines[counter] == "own 2") // if Wining Line is found
                {
                    ProcessLine(grid, out newGrid, out targetLine, out targetCell, out targetFound, counter);
                    break;
                }
            }

            //Check 2 - find enemy 2
            if (targetFound == false)
            {
                for (int counter = 0; counter <= 7; counter++)
                {
                    if (lines[counter] == "enemy 2") // if the player is about to win
                    {
                        ProcessLine(grid, out newGrid, out targetLine, out targetCell, out targetFound, counter);
                        break;
                    }
                }
            }

            //Check 3 - find own 1
            if (targetFound == false)
            {
                for (int counter = 0; counter <= 7; counter++)
                {
                    if (lines[counter] == "own 1") // look for a line to develop
                    {
                        ProcessLine(grid, out newGrid, out targetLine, out targetCell, out targetFound, counter);
                        break;
                    }
                }
            }

            //Check 4 - find enemy 1
            if (targetFound == false)
            {
                for (int counter = 0; counter <= 7; counter++)
                {
                    if (lines[counter] == "enemy 1") // look for a developing line to disrupt
                    {
                        ProcessLine(grid, out newGrid, out targetLine, out targetCell, out targetFound, counter);
                        break;
                    }
                }
            }

            // Fill in a blank

            if (targetFound == false)
            {
                // TODO : RANDOM EMPTY CELL - Does this come into play??
                for (int counter = 0; counter <= 8; counter++)
                {
                    if (grid[counter] == 0) // look for the first empty cell
                    {
                        targetCell = counter;
                        targetFound = true;
                        newGrid = AddInput(grid, targetCell);
                        CalculateLineStates(grid);
                        break;
                    }
                }
            }

            //Check if the board is full
            int emptyCells = 0;
            for (int counter = 0; counter <= 8; counter++)
            {
                if (grid[counter] == 0) emptyCells = 1;
                continue;
            }
            if (emptyCells == 0 && gameWon == false && playerWon == false)
            {
                //Console.WriteLine("== And That's That! ==");
                //Console.WriteLine();
                gameOn = false;
            }
            return targetCell;
        }

        private void ProcessLine(int[] grid, out int[] newGrid, out int targetLine, out int targetCell, out bool targetFound, int counter)
        {
            targetLine = counter; // mark win line
            targetCell = ExtractTarget(grid, lineDefs, targetLine);
            targetFound = true;
            newGrid = AddInput(grid, targetCell);
            CalculateLineStates(grid);
        }

        private int ExtractTarget(int[] grid, int[,] lineDef, int targetLine)
        {
            if (grid[lineDef[targetLine, 0]] == 0) return lineDef[targetLine, 0];
            else if (grid[lineDef[targetLine, 1]] == 0) return lineDef[targetLine, 1];
            else return lineDef[targetLine, 2];
        }

        private int[] AddInput(int[] grid, int targetCell)
        {
            grid[targetCell] = 1;
            return grid;
        }

        private int[] ChooseSecondMove(int[] grid)
        {
            int firstMove = 0;
            for (int counter = 0; counter < 9; counter++)
            {
                if (grid[counter] == 2)
                {
                    firstMove = counter;
                    break;
                }
            }
            Dictionary<int, int> secondMoveMap = new Dictionary<int, int>()
            {
                { 0, 4 },
                { 1, 6 },
                { 2, 4 },
                { 3, 2 },
                { 4, 0 },
                { 5, 6 },
                { 6, 4 },
                { 7, 0 },
                { 8, 4 },
            };
            grid[secondMoveMap[firstMove]] = 1;
            return grid;
        }

        private int[] ChooseFirstMove(int[] grid)
        {
            Random rnd = new Random();
            int[] firstMoveGrid = new int[4] { 0, 2, 6, 8 };
            int rndFirstMoveIndex = rnd.Next(4);
            grid[firstMoveGrid[rndFirstMoveIndex]] = 1;
            return grid;
        }

        private int GetTurnNumber(int[] grid)
        {
            int turns = 0;
            for (int counter = 0; counter < 9; counter++)
            {
                if (grid[counter] > 0)
                {
                    turns++;
                }
            }
            return turns;
        }

        /// <summary>
        /// Calculates the sum of a given line in the board grid
        /// </summary>
        /// <param name="grid">An int array, representing the game board</param>
        /// <param name="lNum">a numerical index (0-8) for the line which sum is to be calculated</param>
        /// <returns>Returns the sum of a given line in the board grid</returns>
        // Retrieve the state of a line number
        public string GetLineState(int[] grid, int lNum)
        {
            int cellOne = this.lineDefs[lNum, 0];
            int cellTwo = this.lineDefs[lNum, 1];
            int cellThree = this.lineDefs[lNum, 2];
            int lineSum = grid[cellOne] + grid[cellTwo] + grid[cellThree];
            switch (lineSum)
            {
                case -6:
                    return "enemy 3";
                //break;
                case -4:
                    return "enemy 2";
                //break;
                case -3:
                    return "enemy 2 closed";
                //break;
                case -2:
                    return "enemy 1";
                //break;
                case -1:
                    return "1 mixed";
                //break;
                case 0:
                    {
                        if (cellOne == 0 && cellTwo == 0 && cellThree == 0)
                            return "clean";
                        else
                            return "own 2 closed";
                        //break;
                    }
                case 1:
                    return "own 1";
                //break;
                case 2:
                    return "own 2";
                //break;
                case 3:
                    return "own 3";
                //break;
                default:
                    throw new ArgumentException("Unexpected line sum");
            }
        }
    }
}
