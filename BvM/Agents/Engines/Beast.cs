using System;
using System.Collections.Generic;

namespace BvM
{
    /// <summary>
    /// Beast agent - "BEst Available STrategy" implementation
    /// </summary>
    class Beast
    {

        // Line definitions
        // 1, 2, 3
        // 4, 5, 6
        // 7, 8, 9
        // 1, 4, 7
        // 2, 5, 8
        // 3, 6, 9
        // 1, 5, 9
        // 3, 5, 7

        private int[,] lineDefs = {
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
            throw new NotImplementedException();
            // TODO : BEAST : Game logic
        }

        /// <summary>
        /// Calculates the sum of a given line in the board grid
        /// </summary>
        /// <param name="grid">An int array, representing the game board</param>
        /// <param name="lNum">a numerical index (0-8) for the line which sum is to be calculated</param>
        /// <returns>Returns the sum of a given line in the board grid</returns>
        // Retrieve the state of a line number
        public int GetLineSum(int[] grid, int lNum)
        {
            int cellOne = this.lineDefs[lNum, 0];
            int cellTwo = this.lineDefs[lNum, 1];
            int cellThree = this.lineDefs[lNum, 2];
            return grid[cellOne] + grid[cellTwo] + grid[cellThree];
        }
    }
}
