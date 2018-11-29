using System;
using System.Collections.Generic;
using System.Text;

namespace BvM.Controllers
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

        /// <summary>
        /// History track for the menace moves
        /// </summary>
        private Queue<int> histRec;

        private InterfaceManager iManager;

        public SwitchBoard(InterfaceManager iManager)
        {
            this.IManager = iManager;
        }

        public Queue<int> HistRec
        {
            get { return histRec; }
            set { histRec = value; }
        }

        public int[] GameBoard
        {
            get { return gameBoard; }
            set { gameBoard = value; }
        }

        public InterfaceManager IManager { get => iManager; set => iManager = value; }
    }
}
