using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using Chess.GameBoard;
using Chess.Cards;

namespace Chess.Pieces
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

            // For the pawns, we make a secondary list.
            // Why? Simple: We want the squares they can
            // attack to count as seen, but not be added 
            // to their possible moves. This is mainly to
            // prevent the king from walking into check...
            List<Square> seenSquares = new List<Square>();

            // Now for the pawns diagonal attacks.

            // White pawns
            if(color == PieceColor.White)
            {
                // Make sure that the square actually exists...
                if(location.NorthWest != null)
                {
                    seenSquares.Add(location.NorthWest);
                    // If there's an enemy piece there, we can add it to the attack list.
                    if(location.NorthWest.IsOccupied && location.NorthWest.Piece.Color != color)
                    {
                        squares.Add(location.NorthWest);
                    }
                }
                // Now we do the same, but with NorthEast.
                if (location.NorthEast != null)
                {
                    seenSquares.Add(location.NorthEast);
                    // If there's an enemy piece there, we can add it to the attack list.
                    if (location.NorthEast.IsOccupied && location.NorthEast.Piece.Color != color)
                    {
                        squares.Add(location.NorthEast);
                    }
                }
            }
            // Black pawns
            else
            {
                // Make sure that the square actually exists...
                if (location.SouthWest != null)
                {
                    seenSquares.Add(location.SouthWest);
                    // If there's an enemy piece there, we can add it to the attack list.
                    if (location.SouthWest.IsOccupied && location.SouthWest.Piece.Color != color)
                    {
                        squares.Add(location.SouthWest);
                    }
                }
                // Now we do the same, but with SouthEast.
                if (location.SouthEast != null)
                {
                    seenSquares.Add(location.SouthEast);
                    // If there's an enemy piece there, we can add it to the attack list.
                    if (location.SouthEast.IsOccupied && location.SouthEast.Piece.Color != color)
                    {
                        squares.Add(location.SouthEast);
                    }
                }
            }

            // In the attack method, we will update the squares the
            // piece sees so that they know they are seen by the color.
            switch (color)
            {
                case PieceColor.White:
                    for (int i = 0; i < seenSquares.Count; i++)
                    {
                        seenSquares[i].WhiteSees = true;
                    }
                    break;
                case PieceColor.Black:
                    for (int i = 0; i < seenSquares.Count; i++)
                    {
                        seenSquares[i].BlackSees = true;
                    }
                    break;
            }

            // Finally, return the list of squares.
            return squares;
        }

    }
}
