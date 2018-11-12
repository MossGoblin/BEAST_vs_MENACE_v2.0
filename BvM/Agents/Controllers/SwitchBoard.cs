using System;
using System.Collections.Generic;
using System.Text;

namespace BvM
{
    /// <summary>
    /// Main agent in the system - keeps the data and calls Beast and Menace
    /// </summary>
    class SwitchBoard
    {
        /// <summary>
        /// Int array representing the current game state
        /// </summary>
        private int[] gameBoard;

        //// DISCONTINUED - Will be passed down from the caller (UI or the history) directly to the Menace agent
        ///// <summary>
        ///// Default value for the probability pool cells
        ///// </summary>
        //private int defProbIndex;

        /// <summary>
        /// History track for the menace moves
        /// </summary>
        private Queue<int> histRec;

        public Queue<int> HistRec
        {
            get { return histRec; }
            set { histRec = value; }
        }


        //public int DefProbIndex
        //{
        //    get { return defProbIndex; }
        //    set { defProbIndex = value; }
        //}

        public int[] GameBoard
        {
            get { return gameBoard; }
            set { gameBoard = value; }
        }
    }
}
