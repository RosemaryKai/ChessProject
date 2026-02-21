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
        /// <summary>
        /// Read-Only property for the ranks of the board.
        /// </summary>
        public int Ranks
        {
            get { return board.GetLength(0); }
        }
        /// <summary>
        /// Read-Only proeprty for the files of the board.
        /// </summary>
        public int Files
        {
            get { return board.GetLength(1); }
        }

        /// <summary>
        /// Indexer for squares of the board.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Square this[int x, int y]
        {
            get {
                if(x >= board.GetLength(0) || x < 0 ||
                    y >= board.GetLength(1) || y < 0)
                {
                    throw new IndexOutOfRangeException
                        ("ERROR: Provided indecies not on the board.");
                }
                else
                {
                    return board[x, y];
                }
                }
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
