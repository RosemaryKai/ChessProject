using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    /// <summary>
    /// The most powerful piece- a major piece that can attack horizontally and diagonally.
    /// </summary>
    internal class Queen : Piece
    {
        // FIELDS of this class


        // PROPERTIES of this class


        // CTORs of this class
        public Queen(Square location, PieceColor color) :
            base(location, color)
        {
            // Instantiation is handled mostly in the parent class.
            value = 9;
            pieceType = PieceType.Queen;
        }

        // METHODS of this class
        /// <summary>
        /// Where the Queen can move.
        /// </summary>
        /// <param name="board">The board.</param>
        /// <returns>The list of squares the Queen can move to.</returns>
        public override List<Square> Move(Board board)
        {
            List<Square> squares = new List<Square>();
            List<Square> newSquares = new List<Square>();

            // The queen attacks like a rook AND a bishop at the same time.
            // So she will have many calls of the GetDirection method.

            // = = = = = = = UP SQUARES = = = = = = = 
            newSquares = GetDirection(1, 0, board);
            for (int i = 0; i < newSquares.Count; i++)
            {
                squares.Add(newSquares[i]);
            }

            // = = = = = = = UP-RIGHT SQUARES = = = = = = = 
            newSquares = GetDirection(1, 1, board);
            for (int i = 0; i < newSquares.Count; i++)
            {
                squares.Add(newSquares[i]);
            }

            // = = = = = = = RIGHT SQUARES = = = = = = = 
            newSquares = GetDirection(0, 1, board);
            for (int i = 0; i < newSquares.Count; i++)
            {
                squares.Add(newSquares[i]);
            }

            // = = = = = = = DOWN-RIGHT SQUARES = = = = = = = 
            newSquares = GetDirection(-1, 1, board);
            for (int i = 0; i < newSquares.Count; i++)
            {
                squares.Add(newSquares[i]);
            }

            // = = = = = = = DOWN SQUARES = = = = = = = 
            newSquares = GetDirection(-1, 0, board);
            for (int i = 0; i < newSquares.Count; i++)
            {
                squares.Add(newSquares[i]);
            }

            // = = = = = = = DOWN-LEFT SQUARES = = = = = = = 
            newSquares = GetDirection(-1, -1, board);
            for (int i = 0; i < newSquares.Count; i++)
            {
                squares.Add(newSquares[i]);
            }

            // = = = = = = = LEFT SQUARES = = = = = = = 
            newSquares = GetDirection(0, -1, board);
            for (int i = 0; i < newSquares.Count; i++)
            {
                squares.Add(newSquares[i]);
            }

            // = = = = = = = UP-LEFT SQUARES = = = = = = = 
            newSquares = GetDirection(1, -1, board);
            for (int i = 0; i < newSquares.Count; i++)
            {
                squares.Add(newSquares[i]);
            }

            // Finally, return all the squares she can see!
            return squares;
        }
        /// <summary>
        /// The list of squares the Queen attacks.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public override List<Square> Attack(Board board)
        {
            return Move(board);
        }

    }
}
