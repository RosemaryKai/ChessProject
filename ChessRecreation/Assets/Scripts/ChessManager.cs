using Chess.Pieces;
using System;
using UnityEngine;

namespace Chess.Game
{
    public class ChessManager : MonoBehaviour
    {
        // FIELDS of this class
        private PieceColor turn;
        public static event Action TurnFlipped;

        // PROPERTIES of this class
        public PieceColor Turn
        {
            get { return turn; }
            set { turn = value; }
        }

        // METHODS of this class

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            turn = PieceColor.White;
        }

        // Update is called once per frame
        void Update()
        {

        }
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

