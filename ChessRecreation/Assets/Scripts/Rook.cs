using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using Chess.GameBoard;

namespace Chess.Pieces
{
    /// <summary>
    /// The major piece that attacks in all four cardinal directions.
    /// </summary>
    internal class Rook : Piece
    {
        /// <summary>
        /// Special abilities of a Rook.
        /// </summary>
        public enum RookAbilities
        {
            SeigeTower,
            TrueCastle
        }
        // FIELDS of this class


        // PROPERTIES of this class
        /// <summary>
        /// If this rook can castle with the king.
        /// </summary>
        public bool CanCastle
        {
            get { return !hasMoved; }
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
            newSquares = GetDirection(0, 1, board);
            for (int i = 0; i < newSquares.Count; i++)
            {
                seenSquares.Add(newSquares[i]);
            }

            // = = = = = = = LEFT SQUARES = = = = = = = 
            newSquares = GetDirection(0, -1, board);
            for (int i = 0; i < newSquares.Count; i++)
            {
                seenSquares.Add(newSquares[i]);
            }

            // = = = = = = = UP SQUARES = = = = = = = 
            newSquares = GetDirection(1, 0, board);
            for (int i = 0; i < newSquares.Count; i++)
            {
                seenSquares.Add(newSquares[i]);
            }

            // = = = = = = = DOWN SQUARES = = = = = = = 
            newSquares = GetDirection(-1, 0, board);
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
            // Make a list for the squares seen and new squares.
            List<Square> seenSquares = new List<Square>();
            List<Square> newSquares = new List<Square>();
            // = = = = = = = RIGHT SQUARES = = = = = = = 
            newSquares = GetSeenSquares(1, 0, board);
            for (int i = 0; i < newSquares.Count; i++)
            {
                seenSquares.Add(newSquares[i]);
            }

            // = = = = = = = LEFT SQUARES = = = = = = = 
            newSquares = GetSeenSquares(-1, 0, board);
            for (int i = 0; i < newSquares.Count; i++)
            {
                seenSquares.Add(newSquares[i]);
            }

            // = = = = = = = UP SQUARES = = = = = = = 
            newSquares = GetSeenSquares(0, 1, board);
            for (int i = 0; i < newSquares.Count; i++)
            {
                seenSquares.Add(newSquares[i]);
            }

            // = = = = = = = DOWN SQUARES = = = = = = = 
            newSquares = GetSeenSquares(0, -1, board);
            for (int i = 0; i < newSquares.Count; i++)
            {
                seenSquares.Add(newSquares[i]);
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
