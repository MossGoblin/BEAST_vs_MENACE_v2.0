using System;
using System.Collections.Generic;
using System.Text;

namespace BvM
{
    /// <summary>
    /// Menace agent
    /// </summary>
    class Menace
    {
        /// <summary>
        /// Probability pool for the Menace agent
        /// </summary>
        private Dictionary<int[], List<int>> choisePool;

        /// <summary>
        /// Default value for the probability pool cells
        /// </summary>
        private int defProbIndex;

        public int DefProbIndex
        {
            get { return defProbIndex; }
            set { defProbIndex = value; }
        }


        public Dictionary<int[], List<int>> ChoisePool
        {
            get { return choisePool; }
            private set { choisePool = value; }
        }

        /// <summary>
        /// Selects a move, based on the current state, then returns it to the caller
        /// </summary>
        /// <param name="grid">An int array, representing the game board</param>
        /// <returns>Returns the next move, chosen by Beast</returns>
        public int NextMove(int[] grid)
        {
            // Extract the probability map
            List<int> probPool = ProbabilityMap(grid);


            // Get the max number for the selection - equals the last element of the prob pool
            int maxCount = probPool[probPool.Count-1];

            // Select a random cell according to the probability map

            Random random = new Random();
            int rndValue = random.Next(1, maxCount);

            int selectedCell = 0;
            for (int counter = 0; counter < probPool.Count; counter++)
            {
                if (probPool[counter] >= rndValue)
                {
                    selectedCell = counter;
                    break;
                }
            }

            return selectedCell;
        }

        public void SavePool(string path)
        {
            // TODO : MENACE : Save the pool into a file (path)
        }

        public Dictionary<int[], List<int>> LoadPool(string path)
        {
            // TODO : MENACE : Load the last pool from a file (path)
            return new Dictionary<int[], List<int>>();
        }

        /// <summary>
        /// Generates a probabilirt map, based on the pool list for the corresponding board state
        /// </summary>
        /// <param name="boardState">An int array, representing the board state</param>
        /// <returns></returns>
        private List<int> ProbabilityMap(int[] boardState)
        {

            // Get the probability map for the boardstate
            List<int> probList = ChoisePool[boardState];

            // Check if there are any available choises for the calculation of the probability pool
            bool hasChoises = false;
            for (int counter = 0; counter < probList.Count; counter++)
            {
                if (probList[counter] != 0)
                {
                    hasChoises = true;
                }
            }
            if (!hasChoises)
            {
                // RETURN SingleZero list if there are no available choices
                return new List<int> { 0 };
            }

            // If there are ANY available choices
            // Create a new probability pool, based on the probability list
            // Example input:  1, 2, 0, 3, 0, 2
            // Example output: 1, 3, 3, 6, 6, 8
            List<int> probPool = new List<int>();

            int accumulation = 0;
            for (int counter = 0; counter < probList.Count; counter++)
            {
                probPool[counter] = accumulation + probList[counter];
                accumulation = probPool[counter];
            }

            return probPool;
        }


    }
}
