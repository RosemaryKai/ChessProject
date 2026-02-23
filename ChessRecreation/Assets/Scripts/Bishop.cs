using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    /// <summary>
    /// The diagonal attacking minor piece.
    /// </summary>
    internal class Bishop : Piece
    {
        // FIELDS of this class


        // PROPERTIES of this class


        // CTORs of this class
        public Bishop(Square startingLocation, PieceColor color) :
            base(startingLocation, color)
        {
            // Instantiation will mostly be in the base class.
            pieceType = PieceType.Bishop;
            value = 3;

        }

        // METHODS of this class
        /// <summary>
        /// Movement for the bishop.
        /// </summary>
        /// <param name="board">The board the bishop is on.</param>
        /// <returns>A list of squares the bishop can see.</returns>
        public override List<Square> Move(Board board)
        {
            // List of squares- first one to return, second one
            // to get vision.
            List<Square> seenSquares = new List<Square>();
            List<Square> newSquares = new List<Square>();

            // = = = = = = = UPPER-LEFT SQUARES = = = = = = = 
            newSquares = GetDirection(-1, 1, board);
            for (int i = 0; i < newSquares.Count; i++)
            {
                seenSquares.Add(newSquares[i]);
            }

            // = = = = = = = UPPER-RIGHT SQUARES = = = = = = = 
            newSquares = GetDirection(1, 1, board);
            for (int i = 0; i < newSquares.Count; i++)
            {
                seenSquares.Add(newSquares[i]);
            }

            // = = = = = = = LOWER-LEFT SQUARES = = = = = = = 
            newSquares = GetDirection(1, -1, board);
            for (int i = 0; i < newSquares.Count; i++)
            {
                seenSquares.Add(newSquares[i]);
            }

            // = = = = = = = LOWER-RIGHT SQUARES = = = = = = = 
            newSquares = GetDirection(-1, -1, board);
            for (int i = 0; i < newSquares.Count; i++)
            {
                seenSquares.Add(newSquares[i]);
            }

            // Finally, return the squares the Bishop can see.
            return seenSquares;
        }
        /// <summary>
        /// The squares the Bishop attacks.
        /// </summary>
        /// <param name="board">The board.</param>
        /// <returns>A list of squares attacked by this Bishop.</returns>
        public override List<Square> Attack(Board board)
        {
            return Move(board);
        }

    }
}
