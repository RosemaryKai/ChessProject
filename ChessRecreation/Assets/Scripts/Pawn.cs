using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Chess
{
    /// <summary>
    /// The building block of any chess position.
    /// </summary>
    internal class Pawn : Piece
    {
        // FIELDS of this class


        // PROPERTIES of this class


        // CTORs of this class
        public Pawn(Square location, PieceColor color) :
            base(location, color)
        {
            // Instantiation handled mostly in the base class.
            value = 1;
            pieceType = PieceType.Pawn;
        }

        // METHODS of this class

        public override List<Square> Move(Board board)
        {
            List<Square> squares = new List<Square>();
            List<Square> newSquares = new List<Square>();

            /* 
             * Pawns are weird- they move straight, but capture diagonally.
             * So their attack method will not call their move method at all-
             * as the two actions are distinct from each other for the piece.
             * So, this method will only handle forward movement.
            */

            // It also depends on which color they are- black pawns move down,
            // white pawns move up. So, we'll account for that as well.

            // White pawns
            if (color == PieceColor.White)
            {
                // = = = = = = = FORWARD SQUARES = = = = = = = 
                newSquares = GetDirection(1, 0, board);
                if (newSquares.Count > 0 && !newSquares[0].IsOccupied)
                {
                    squares.Add(newSquares[0]);
                    if (newSquares.Count > 1 && !newSquares[1].IsOccupied
                        && location.Rank == 1)
                    {
                        squares.Add(newSquares[1]);
                    }
                }
            }
            // Black pawns
            else
            {
                // = = = = = = = FORWARD SQUARES = = = = = = = 
                newSquares = GetDirection(-1, 0, board);
                if (newSquares.Count > 0 && !newSquares[0].IsOccupied)
                {
                    squares.Add(newSquares[0]);
                    if (newSquares.Count > 1 && !newSquares[1].IsOccupied
                        && location.Rank == 6)
                    {
                        squares.Add(newSquares[1]);
                    }
                }
            }
            return squares;
        }
        /// <summary>
        /// The diagonal attacks of a pawn.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public override List<Square> Attack(Board board)
        {
            List<Square> squares = new List<Square>();
            List<Square> newSquares = new List<Square>();

            // Now for the pawns diagonal attacks.

            // White pawns
            if(color == PieceColor.White)
            {
                newSquares = GetDirection(1, 1, board);
                if(newSquares.Count > 0 && newSquares[0].IsOccupied &&
                    newSquares[0].Piece.Color != color)
                {
                    squares.Add(newSquares[0]);
                }
                newSquares = GetDirection(1, -1, board);
                if (newSquares.Count > 0 && newSquares[0].IsOccupied &&
                    newSquares[0].Piece.Color != color)
                {
                    squares.Add(newSquares[0]);
                }
            }
            // Black pawns
            else
            {
                newSquares = GetDirection(-1, 1, board);
                if (newSquares.Count > 0 && newSquares[0].IsOccupied &&
                    newSquares[0].Piece.Color != color)
                {
                    squares.Add(newSquares[0]);
                }
                newSquares = GetDirection(-1, -1, board);
                if (newSquares.Count > 0 && newSquares[0].IsOccupied &&
                    newSquares[0].Piece.Color != color)
                {
                    squares.Add(newSquares[0]);
                }
            }
            // Finally, return the list.
            return squares;
        }

    }
}
