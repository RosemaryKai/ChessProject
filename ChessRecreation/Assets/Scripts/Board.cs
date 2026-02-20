using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    /// <summary> 
    /// The board which pieces and squares are on. 
    /// </summary> 
    internal class Board
    {
        // FIELDS of this class
        private Square[,] board;
        private List<Piece> pieces;

        // PROPERTIES of this class
        /// <summary> 
        /// The number of pieces on the board. 
        /// </summary> 
        public int NumberOfPieces
        {
            get { return pieces.Count; }
        }

        // CTORs of this class
        public Board()
        {
            // Creation of a new board. 
            // Filling the values with new squares.
            board = new Square[8, 8];
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = new Square(i, j);
                }
            }
            StartingPosition();
        }
        // METHODS of this class
        private void StartingPosition()
        {
            pieces = new List<Piece>();
            // Insantiation of the White Pieces.
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // TODO: Add once piece logic is actually created.
                }
            }
            // Insantiation of the Black Pieces.
            for (int i = 8; i > 6; i--)
            {
                for (int j = 0; j < 8; j++)
                {
                    // TODO: Add once piece logic is actually created.
                }
            }
        }
    }
}
