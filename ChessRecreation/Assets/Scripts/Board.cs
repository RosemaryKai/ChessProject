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
            get 
            {
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
                    // WHITE PAWNS
                    if(i == 1)
                    {
                        Pawn newPawn = new Pawn(board[j, i], PieceColor.White);
                        pieces.Add(newPawn);
                        board[j, i].Piece = newPawn;
                    }
                    // WHITE ROOKS
                    if((j == 0 || j == 7) && i == 0)
                    {
                        Rook newRook = new Rook(board[j, i], PieceColor.White);
                        pieces.Add(newRook);
                        board[j, i].Piece = newRook;
                    }

                    // WHITE BISHOPS
                    if((j == 2 || j == 5) && i == 0)
                    {
                        Bishop newBishop = new Bishop(board[j, i], PieceColor.White);
                        pieces.Add(newBishop);
                        board[j, i].Piece = newBishop;
                    }

                    // WHITE KNIGHTS
                    if ((j == 1 || j == 6) && i == 0)
                    {
                        Knight newKnight = new Knight(board[j, i], PieceColor.White);
                        pieces.Add(newKnight);
                        board[j, i].Piece = newKnight;
                    }

                    // WHITE QUEEN
                    if (j == 3 && i == 0)
                    {
                        Queen newQueen = new Queen(board[j, i], PieceColor.White);
                        pieces.Add(newQueen);
                        board[j, i].Piece = newQueen;
                    }

                    // WHITE KING
                    if (j == 4 && i == 0)
                    {
                        King newKing = new King(board[j, i], PieceColor.White);
                        pieces.Add(newKing);
                        board[j, i].Piece = newKing;
                    }
                }
            }
            // Insantiation of the Black Pieces.
            for (int i = 7; i > 5; i--)
            {
                for (int j = 0; j < 8; j++)
                {
                    // BLACK PAWNS
                    if (i == 6)
                    {
                        Pawn newPawn = new Pawn(board[j, i], PieceColor.Black);
                        pieces.Add(newPawn);
                        board[j, i].Piece = newPawn;
                    }
                    // BLACK ROOKS
                    if ((j == 0 || j == 7) && i == 7)
                    {
                        Rook newRook = new Rook(board[j, i], PieceColor.Black);
                        pieces.Add(newRook);
                        board[j, i].Piece = newRook;
                    }

                    // BLACK BISHOPS
                    if ((j == 2 || j == 5) && i == 7)
                    {
                        Bishop newBishop = new Bishop(board[j, i], PieceColor.Black);
                        pieces.Add(newBishop);
                        board[j, i].Piece = newBishop;
                    }

                    // BLACK KNIGHTS
                    if ((j == 1 || j == 6) && i == 7)
                    {
                        Knight newKnight = new Knight(board[j, i], PieceColor.Black);
                        pieces.Add(newKnight);
                        board[j, i].Piece = newKnight;
                    }

                    // BLACK QUEEN
                    if (j == 3 && i == 7)
                    {
                        Queen newQueen = new Queen(board[j, i], PieceColor.Black);
                        pieces.Add(newQueen);
                        board[j, i].Piece = newQueen;
                    }

                    // BLACK KING
                    if (j == 4 && i == 7)
                    {
                        King newKing = new King(board[j, i], PieceColor.Black);
                        pieces.Add(newKing);
                        board[j, i].Piece = newKing;
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
            List<Square> squares = piece.Move(this);

            // If they're pawns, try to get their attacking squares.
            if (piece is Pawn)
            {
                List<Square> pawnAttacks = piece.Attack(this);
                if (pawnAttacks.Count > 0)
                {
                    for (int i = 0; i < pawnAttacks.Count; i++)
                    {
                        squares.Add(pawnAttacks[i]);
                    }
                }
            }

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
            // Let's check if we're moving to a square occupied by an enemy.
            if(square.Piece != null && square.Piece.Color != piece.Color)          // We'll try to capture the piece.
            {
                Piece capturedPiece = square.Piece;
                capturedPiece.Captured();
            }
            // So update the piece's location to match.
            piece.Location.Piece = null;                // Sets the piece's current location to null.
            piece.Location = square;                    // Sets the piece's location to the square.
            square.Piece = piece;                       // Sets the square's piece to that piece.

            return true;
        }
    }
}
