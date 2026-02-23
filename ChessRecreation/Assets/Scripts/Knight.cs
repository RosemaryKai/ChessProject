using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    /// <summary>
    /// The only piece that can jump over others, a minor piece.
    /// </summary>
    internal class Knight : Piece
    {
        // FIELDS of this class


        // PROPERTIES of this class


        // CTORs of this class
        public Knight(Square location, PieceColor color) :
            base(location, color)
        {
            // Instantiation will mostly be handled by the parent class
            value = 3;
            pieceType = PieceType.Knight;
        }

        // METHODS of this class

        public override List<Square> Move(Board board)
        {
            List<Square> squares = new List<Square>();
            List<Square> newSquares = new List<Square>();

            // For the knight, we'd only ever need the first square
            // it returns. So, we'll just take those.
            // = = = = = = = UPPER T = = = = = = = 
            newSquares = GetDirection(2, 1, board);
            if (newSquares.Count != 0)
            {
                squares.Add(newSquares[0]);
            }
            newSquares = GetDirection(2, -1, board);
            if (newSquares.Count != 0)
            {
                squares.Add(newSquares[0]);
            }

            // = = = = = = = RIGHT T = = = = = = = 
            newSquares = GetDirection(1, 2, board);
            if (newSquares.Count != 0)
            {
                squares.Add(newSquares[0]);
            }
            newSquares = GetDirection(-1, 2, board);
            if (newSquares.Count != 0)
            {
                squares.Add(newSquares[0]);
            }

            // = = = = = = = LEFT T = = = = = = = 
            newSquares = GetDirection(1, -2, board);
            if (newSquares.Count != 0)
            {
                squares.Add(newSquares[0]);
            }
            newSquares = GetDirection(-1, -2, board);
            if (newSquares.Count != 0)
            {
                squares.Add(newSquares[0]);
            }

            // = = = = = = = LOWER T = = = = = = = 
            newSquares = GetDirection(-2, 1, board);
            if (newSquares.Count != 0)
            {
                squares.Add(newSquares[0]);
            }
            newSquares = GetDirection(-2, -1, board);
            if (newSquares.Count != 0)
            {
                squares.Add(newSquares[0]);
            }

            // Finally, return that list.
            return squares;
        }
        /// <summary>
        /// The squares the Knight attacks.
        /// </summary>
        /// <param name="board">The board.</param>
        /// <returns>A list of squares attacked by this Knight.</returns>
        public override List<Square> Attack(Board board)
        {
            return Move(board);
        }
    }
}
