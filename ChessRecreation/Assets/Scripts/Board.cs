using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    /// <summary> 
    /// The board which pieces and squares are on. 
    /// </summary> 
    internal class Board
    {
        // FIELDS of this class
        private Square[,] board;
        private List<Piece> pieces;

        // PROPERTIES of this class
        /// <summary> 
        /// The number of pieces on the board. 
        /// </summary> 
        public int NumberOfPieces
        {
            get { return pieces.Count; }
        }
        /// <summary>
        /// Read-Only property for the ranks of the board.
        /// </summary>
        public int Ranks
        {
            get { return board.GetLength(0); }
        }
        /// <summary>
        /// Read-Only proeprty for the files of the board.
        /// </summary>
        public int Files
        {
            get { return board.GetLength(1); }
        }

        /// <summary>
        /// Indexer for squares of the board.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Square this[int x, int y]
        {
            get {
                if(x >= board.GetLength(0) || x < 0 ||
                    y >= board.GetLength(1) || y < 0)
                {
                    throw new IndexOutOfRangeException
                        ("ERROR: Provided indecies not on the board.");
                }
                else
                {
                    return board[x, y];
                }
                }
        }

        // CTORs of this class
        public Board()
        {
            // Creation of a new board. 
            // Filling the values with new squares.
            board = new Square[8, 8];
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = new Square(i, j);
                }
            }
            StartingPosition();
        }
        // METHODS of this class
        /// <summary>
        /// Sets the starting position of the board.
        /// </summary>
        private void StartingPosition()
        {
            pieces = new List<Piece>();
            // Insantiation of the White Pieces.
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if((j == 0 || j == 7) && i == 0)
                    {
                        Rook newRook = new Rook(board[j, i], PieceColor.White);
                        pieces.Add(newRook);
                        board[j, i].Piece = newRook;
                    }
                }
            }
            // Insantiation of the Black Pieces.
            for (int i = 7; i > 5; i--)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((j == 0 || j == 7) && i == 7)
                    {
                        Rook newRook = new Rook(board[j, i], PieceColor.Black);
                        pieces.Add(newRook);
                        board[j, i].Piece = newRook;
                    }
                }
            }

        }
        /// <summary>
        /// Checks the validity for move legality.
        /// </summary>
        /// <param name="piece">The piece being moved.</param>
        /// <param name="square">The square it is being moved ot.</param>
        /// <returns>A boolean based on if the move is possible.</returns>
        public bool CanMoveTo(Piece piece, Square square)
        {
            // Make sure parameters aren't null. We need them!
            // If they are null? Well, then it's impossible.
            if (piece == null || square == null)
            {
                return false;
            }

            // Get the piece's vision of squares.
            List<Square> squares = piece.Vision(this);

            // Iterate through the piece's vision. 
            for (int i = 0; i < squares.Count; i++)
            {
                // If it can see the square?
                if (squares[i] == square)
                {
                    // The move is possible.
                    return true;
                }
            }
            // If not? The move is NOT possible.
            return false;
        }
        /// <summary>
        /// Moves a piece to a square.
        /// </summary>
        /// <param name="piece">The piece being moved.</param>
        /// <param name="square">The square it's being moved to.</param>
        /// <returns>A boolean based on if the move was made.</returns>
        public bool TryMove(Piece piece, Square square)
        {
            // Make sure parameters aren't null. We need them!
            // If they are null? Well, then it's impossible.
            if (piece == null || square == null)
            {
                return false;
            }

            // Now make sure the move is even possible.
            // If it isn't? Exit the method.
            if(!CanMoveTo(piece, square))
            {
                return false;
            }

            // Now if we made it down here, that means the move is possible.
            // So update the piece's location to match.
            piece.Location.Piece = null;
            piece.Location = square;
            square.Piece = piece;

            return true;
        }

        public bool TryCapture(Piece piece, Square square,
            Dictionary<Piece, PieceView> pieceViews, Dictionary<Square, SquareView> squareViews)
        {
            // First make sure nothing is null. If anything is null, we can't do anything.
            if(piece == null ||square == null ||
                pieceViews == null || squareViews == null)
            {
                return false;
            }

            // Now make sure that we can actually move to that square.
            if(!CanMoveTo(piece, square))
            {
                return false;
            }

            // With all of that out of the way, we can move on to captures.
            // First, let's set the piece on the target square to captured.
            square.Piece.IsCaptured = true;     // This will disable it, making it invisible.
            square.Piece = null;                // Make the square's piece null as well,
                                                // making room for the next one.

            // Now, we'll technically move the piece to that square.
            TryMove(piece, square);

            // Now visually move that piece to the formerly occupied square.
            PieceView pieceView = pieceViews[piece];        // Grab the PieceView of the piece.
            SquareView squareView = squareViews[square];    // Grab the SquareView of the square.
            pieceView.transform.position = squareView.transform.position +
                new UnityEngine.Vector3(0, 0, -0.1f);       // Move the piece prefab to that square.

            // Return true, as the capture has succeeded.
            return true;
        }
    }
}
