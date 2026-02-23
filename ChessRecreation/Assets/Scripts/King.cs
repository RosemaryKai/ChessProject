using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    internal class King : Piece
    {
        // FIELDS of this class
        private bool isChecked;

        // PROPERTIES of this class
        /// <summary>
        /// If the king is in check or not.
        /// </summary>
        public bool IsChecked
        {
            get { return IsChecked; }
        }
        /// <summary>
        /// If the king can castle with a rook.
        /// </summary>
        public bool CanCastle
        {
            get { return hasMoved; }
        }

        // CTORs of this class
        public King(Square location, PieceColor color) :
            base(location, color)
        {
            // Instantiation is handled in the base class!
            value = 0;
            pieceType = PieceType.King;
        }

        // METHODS of this class

        public override List<Square> Move(Board board)
        {
            List<Square> squares = new List<Square>();
            List<Square> newSquares = new List<Square>();

            // The king is an odd piece. Its moves are limited based on other
            // pieces on the board, making it difficult to calculate where it can
            // and can't move. 
            // To solve this issue, we'll use a helper method that sees all possible 
            // directions the king could be checked from, returning every single square.
            // This will help determine if he is in check, or could potentially be in check 
            // if a piece moves, helping with pin logic later.

            // = = = = = = = UP SQUARE = = = = = = = 
            newSquares = GetDirection(1, 0, board);
            if(newSquares.Count > 0)
            {
                squares.Add(newSquares[0]);
            }

            // = = = = = = = UP-RIGHT SQUARE = = = = = = = 
            newSquares = GetDirection(1, 1, board);
            if (newSquares.Count > 0)
            {
                squares.Add(newSquares[0]);
            }

            // = = = = = = = RIGHT SQUARE = = = = = = = 
            newSquares = GetDirection(0, 1, board);
            if (newSquares.Count > 0)
            {
                squares.Add(newSquares[0]);
            }

            // = = = = = = = DOWN-RIGHT SQUARE = = = = = = = 
            newSquares = GetDirection(-1, 1, board);
            if (newSquares.Count > 0)
            {
                squares.Add(newSquares[0]);
            }

            // = = = = = = = DOWN SQUARE = = = = = = = 
            newSquares = GetDirection(-1, 0, board);
            if (newSquares.Count > 0)
            {
                squares.Add(newSquares[0]);
            }

            // = = = = = = = DOWN-LEFT SQUARE = = = = = = = 
            newSquares = GetDirection(-1, -1, board);
            if (newSquares.Count > 0)
            {
                squares.Add(newSquares[0]);
            }

            // = = = = = = = LEFT SQUARE = = = = = = = 
            newSquares = GetDirection(0, -1, board);
            if (newSquares.Count > 0)
            {
                squares.Add(newSquares[0]);
            }

            // = = = = = = = UP-LEFT SQUARE = = = = = = = 
            newSquares = GetDirection(1, -1, board);
            if (newSquares.Count > 0)
            {
                squares.Add(newSquares[0]);
            }

            return squares;
        }

        public override List<Square> Attack(Board board)
        {
            return Move(board);
        }

    }
}
