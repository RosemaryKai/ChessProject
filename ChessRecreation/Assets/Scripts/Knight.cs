using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.GameBoard;

namespace Chess.Pieces
{
    /// <summary>
    /// The only piece that can jump over others, a minor piece.
    /// </summary>
    internal class Knight : Piece
    {
        /// <summary>
        /// The special abilities of a Knight.
        /// </summary>
        public enum KnightAbilities
        {
            Mounted,
            Cavalry
        }
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
            // Make a list for the squares seen and new squares.
            List<Square> seenSquares = new List<Square>();
            List<Square> newSquares = new List<Square>();
            newSquares = GetSeenSquares(2, 1, board);
            if (newSquares.Count != 0)
            {
                seenSquares.Add(newSquares[0]);
            }
            newSquares = GetSeenSquares(2, -1, board);
            if (newSquares.Count != 0)
            {
                seenSquares.Add(newSquares[0]);
            }

            // = = = = = = = RIGHT T = = = = = = = 
            newSquares = GetSeenSquares(1, 2, board);
            if (newSquares.Count != 0)
            {
                seenSquares.Add(newSquares[0]);
            }
            newSquares = GetSeenSquares(-1, 2, board);
            if (newSquares.Count != 0)
            {
                seenSquares.Add(newSquares[0]);
            }

            // = = = = = = = LEFT T = = = = = = = 
            newSquares = GetSeenSquares(1, -2, board);
            if (newSquares.Count != 0)
            {
                seenSquares.Add(newSquares[0]);
            }
            newSquares = GetSeenSquares(-1, -2, board);
            if (newSquares.Count != 0)
            {
                seenSquares.Add(newSquares[0]);
            }

            // = = = = = = = LOWER T = = = = = = = 
            newSquares = GetSeenSquares(-2, 1, board);
            if (newSquares.Count != 0)
            {
                seenSquares.Add(newSquares[0]);
            }
            newSquares = GetSeenSquares(-2, -1, board);
            if (newSquares.Count != 0)
            {
                seenSquares.Add(newSquares[0]);
            }
            // Then, in the attack method, we will update the squares
            // the piece sees so that they know they are seen by the
            // color.
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

            // Then, we return the list.
            return seenSquares;
        }
    }
}
