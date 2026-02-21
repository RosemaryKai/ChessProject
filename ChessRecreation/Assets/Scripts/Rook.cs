using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine.UIElements;

namespace Chess
{
    /// <summary>
    /// The Rook - the piece that moves up, down, left, and right.
    /// </summary>
    internal class Rook : Piece
    {
        // FIELDS of this class


        // PROPERTIES of this class
        public bool CanCastle
        {
            get { return hasMoved; }
        }

        // CTORs of this class
        /// <summary>
        /// Creation of a new rook.
        /// </summary>
        /// <param name="location">The square the rook is on.</param>
        /// <param name="color">The color of the rook.</param>
        public Rook(Square location, PieceColor color) :
            base(location, color)
        {
            // The base class mostly handles instantiation.
            pieceType = PieceType.Rook;
        }

        // METHODS of this class
        /// <summary>
        /// Shows the vision of a chess piece moving through anything that blocks them.
        /// </summary>
        /// <param name="board">The board the piece is on.</param>
        /// <returns></returns>
        public override List<Square> Vision(Board board)
        {
            List<Square> seenSquares = new List<Square>();

            // = = = = = = = RIGHT SQUARES = = = = = = = 
            // Moving right from the current location of the rook to
            // add the squares it can see to the list.
            for (int i = location.Rank + 1; i < board.Ranks; i++)
            {
                // Save the current square- mainly for readability.

                Square currentSquare = board[location.File, i];
                if (!currentSquare.IsOccupied)
                    seenSquares.Add(currentSquare);

                // If the piece on the occupied square is the same color as this rook,
                // do not include the square. The rook cannot capture its own color.
                else if(currentSquare.Piece.Color == color)
                    break;

                // If it's not, include the square. That's an enemy piece and it can
                // be captured.
                else
                {
                    seenSquares.Add(currentSquare);
                    break;
                }
            }

            // = = = = = = = LEFT SQUARES = = = = = = = 
            // Moving left from the current location of the rook to
            // add the squares it can see to the list.
            for (int i = location.Rank - 1; i >= 0; i--)
            {
                Square currentSquare = board[location.File, i];

                if (!currentSquare.IsOccupied)
                    seenSquares.Add(currentSquare);
                // Same logic- same color pieces cannot capture each other.
                else if (currentSquare.Piece.Color == color)
                    break;
                else
                {
                    seenSquares.Add(currentSquare);
                    break;
                }
            }

            // = = = = = = = UP SQUARES = = = = = = = 
            // Moving up from the current location of the rook to
            // add the squares it can see to the list.
            for (int i = location.File + 1; i < board.Files; i++)
            {
                Square currentSquare = board[i, location.Rank];

                // Same logic for squares- nothing new. Add enemy occupied squares,
                // do not add friendly occupied squares.
                if (!currentSquare.IsOccupied)
                    seenSquares.Add(currentSquare);
                else if (currentSquare.Piece.Color == color)
                    break;
                else
                {
                    seenSquares.Add(currentSquare);
                    break;
                }
            }
            // = = = = = = = DOWN SQUARES = = = = = = = 
            // Moving down from the current location of the rook to
            // add the squares it can see to the list.
            for (int i = location.File - 1; i >= 0; i--)
            {
                Square currentSquare = board[i, location.Rank];

                // Again.. the same logic. Keep adding until we bump into
                // another piece (or edge), then check if it's friendly or not.
                if (!currentSquare.IsOccupied)
                    seenSquares.Add(currentSquare);
                else if (currentSquare.Piece.Color == color)
                    break;
                else
                {
                    seenSquares.Add(currentSquare);
                    break;
                }
            }

            // Finally, return the list of squares the rook can see!
            return seenSquares;
        }

        public override string ToString()
        {
            return $"{color} Rook; {location}";
        }
    }
}
