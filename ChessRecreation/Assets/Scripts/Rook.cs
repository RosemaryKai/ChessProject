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
    /// The major piece that attacks in all four cardinal directions.
    /// </summary>
    internal class Rook : Piece
    {
        // FIELDS of this class


        // PROPERTIES of this class
        /// <summary>
        /// If this rook can castle with the king.
        /// </summary>
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
            value = 5;
        }

        // METHODS of this class
        /// <summary>
        /// Shows the vision of a chess piece moving through anything that blocks them.
        /// </summary>
        /// <param name="board">The board the piece is on.</param>
        /// <returns></returns>
        public override List<Square> Move(Board board)
        {
            List<Square> seenSquares = new List<Square>();
            List<Square> newSquares = new List<Square>();

            // = = = = = = = RIGHT SQUARES = = = = = = = 
            // Have a list get the new squares.. then add it to seen squares.
            // This method repeats for every direction.
            newSquares = GetDirection(1, 0, board);
            for (int i = 0; i < newSquares.Count; i++)
            {
                seenSquares.Add(newSquares[i]);
            }

            // = = = = = = = LEFT SQUARES = = = = = = = 
            newSquares = GetDirection(-1, 0, board);
            for (int i = 0; i < newSquares.Count; i++)
            {
                seenSquares.Add(newSquares[i]);
            }

            // = = = = = = = UP SQUARES = = = = = = = 
            newSquares = GetDirection(0, 1, board);
            for (int i = 0; i < newSquares.Count; i++)
            {
                seenSquares.Add(newSquares[i]);
            }

            // = = = = = = = DOWN SQUARES = = = = = = = 
            newSquares = GetDirection(0, -1, board);
            for (int i = 0; i < newSquares.Count; i++)
            {
                seenSquares.Add(newSquares[i]);
            }

            // Finally, return the list of squares the rook can see!
            return seenSquares;
        }
        /// <summary>
        /// The squares the Rook attacks.
        /// </summary>
        /// <param name="board">The board.</param>
        /// <returns>A list of squares attacked by the Rook.</returns>
        public override List<Square> Attack(Board board)
        {
            return Move(board);
        }
    }
}
