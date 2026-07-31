using Chess.Pieces;
using System;
using UnityEngine;

namespace Chess.Game
{
    public class TurnManager
    {
        // FIELDS of this class
        private static PieceColor turn;
        public static event Action TurnFlipped;

        // PROPERTIES of this class
        public static PieceColor Turn
        {
            get { return turn; }
            set { turn = value; }
        }

        // METHODS of this class
        /// <summary>
        /// Flips the colors turns.
        /// </summary>
        public void FlipTurn()
        {
            if (turn == PieceColor.White)
            {
                turn = PieceColor.Black;
            }
            else
            {
                turn = PieceColor.White;
            }
            // This event will mainly be for UI
            TurnFlipped?.Invoke();
        }
    }
}

