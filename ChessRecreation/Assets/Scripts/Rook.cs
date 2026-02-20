using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace Chess
{
    internal class Rook : Piece
    {
        // FIELDS of this class


        // PROPERTIES of this class


        // CTORs of this class
        public Rook(Square location, PieceColor color) :
            base(location, color)
        {
            // The base class mostly handles instantiation.
        }

        // METHODS of this class
        /// <summary>
        /// Shows the vision of a chess piece moving through anything that blocks them.
        /// </summary>
        /// <param name="board">The board the piece is on.</param>
        /// <returns></returns>
        public override List<Square> UnblockedVision(Board board)
        {
            List<Square> seenSquares = new List<Square>(0);
            // Add squares to the right.
            for (int i = location.X + 1; i < 8; i++)
            {
                seenSquares.Add(board[i, location.Y]);
            }
            // Now squares to the left.
            for (int i = location.X - 1; i >= 0; i--)
            {
                seenSquares.Add(board[i, location.Y]);
            }
            // Now squares above...
            for (int i = location.Y + 1; i < 8; i++)
            {
                seenSquares.Add(board[location.X, i]);
            }
            // Now the squares below.
            for (int i = location.Y - 1; i >= 0; i--)
            {
                seenSquares.Add(board[location.X, i]);
            }
            return seenSquares;
        }
    }
}
