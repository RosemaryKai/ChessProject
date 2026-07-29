using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using Chess.Game;
using Chess.Pieces;

namespace Chess.GameBoard
{
    /// <summary> 
    /// The board which pieces and squares are on. 
    /// </summary> 
    internal class Board
    {
        // FIELDS of this class
        private Square[,] board;
        private List<Piece> blackPieces;
        private List<Piece> whitePieces;
        private King blackKing;
        private King whiteKing;
        private Square previousBlackKingLocation;
        private Square previousWhiteKingLocation;

        // Backend Event
        public static event Action<Piece, Square> KingCastled;

        // PROPERTIES of this class
        /// <summary> 
        /// The number of pieces on the board. 
        /// </summary> 
        public int NumberOfPieces
        {
            get { return whitePieces.Count + 
                    blackPieces.Count; }
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
                        ($"ERROR: Provided indecies not on the board. \nIndecies provided: {x}, {y}");
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

            // After all squares are made, we want to set their neighbors!
            // This is so it's easy to access neighboring squares through the
            // locations of pieces, which is important for some cards!
            /* Movements in directions form squares:
            North: board[i, j + 1]
            South: board[i, j - 1]
            East: board[i + 1, j]
            West: board[i - 1, j]
            NorthEast: board[i + 1, j + 1]
            SouthEast: board[i + 1, j - 1]
            NorthWest: board[i - 1, j + 1]
            SouthWest: board[i - 1, j - 1]
            */
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Square currentSquare = board[i, j];
                    #region corners
                    // The A1 Square. 
                    if (i == 0 && j == 0)
                    {
                        currentSquare.SetNeighbors(board[i, j + 1], null, board[i + 1, j], null, null, null, null, board[i + 1, j + 1]);
                    }
                    // The A8 Square.
                    else if (i == 0 && j == board.GetLength(1) - 1)
                    {
                        currentSquare.SetNeighbors(null, board[i, j - 1], board[i + 1, j], null, null, null, board[i + 1, j - 1], null);
                    }
                    // The H1 Square.
                    else if(i == board.GetLength(0) - 1 && j == 0)
                    {
                        currentSquare.SetNeighbors(board[i, j + 1], null, null, board[i - 1, j], board[i - 1, j + 1], null, null, null);
                    }
                    // The H8 Square.
                    else if (i == board.GetLength(0) - 1 && j == board.GetLength(1) - 1)
                    {
                        currentSquare.SetNeighbors(null, board[i, j - 1], null, board[i - 1, j], null, board[i - 1, j - 1], null, null);
                    }
                    #endregion
                    #region edges
                    // Squares on the first file (A file). 
                    else if (i == 0 && j != board.GetLength(1) - 1)
                    {
                        currentSquare.SetNeighbors(board[i, j + 1], board[i, j - 1], board[i + 1, j], null, null, null, board[i + 1, j - 1], board[i + 1, j + 1]);
                    }
                    // Squares on the first rank (1st rank).
                    else if (j == 0 && i != board.GetLength(0) - 1)
                    {
                        currentSquare.SetNeighbors(board[i, j + 1], null, board[i + 1, j], board[i - 1, j], board[i - 1, j + 1], null, null, board[i + 1, j + 1]);
                    }
                    // Squares on the final file (H file).
                    else if (i == board.GetLength(0) - 1 && j != board.GetLength(1) - 1)
                    {
                        currentSquare.SetNeighbors(board[i, j + 1], board[i, j - 1], null, board[i - 1, j], board[i - 1, j + 1], board[i - 1, j - 1], null, null);
                    }
                    // Squares on the final rank (8th rank).
                    else if (j == board.GetLength(1) - 1 && i != board.GetLength(0) - 1)
                    {
                        currentSquare.SetNeighbors(null, board[i, j - 1], board[i + 1, j], board[i - 1, j], null, board[i - 1, j - 1], board[i + 1, j - 1], null);
                    }
                    #endregion
                    // Every other square.
                    else
                    {
                        currentSquare.SetNeighbors(board[i, j + 1], board[i, j - 1], board[i + 1, j], board[i - 1, j],
                            board[i - 1, j + 1], board[i - 1, j - 1], board[i + 1, j - 1], board[i + 1, j + 1]);
                    }
                }
            }

            // Now set up methods! Subscribing to events
            // and setting up the starting position.
            TurnManager.TurnFlipped += UpdateSquareData;
            StartingPosition();
        }
        // METHODS of this class
        public void Update()
        {
            // Empty for now
        }
        /// <summary>
        /// Sets the starting position of the board.
        /// </summary>
        private void StartingPosition()
        {
            whitePieces = new List<Piece>();
            blackPieces = new List<Piece>();
            // Insantiation of the White Pieces.
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // WHITE PAWNS
                    if(i == 1)
                    {
                        Pawn newPawn = new Pawn(board[j, i], PieceColor.White);
                        whitePieces.Add(newPawn);
                        board[j, i].Piece = newPawn;
                    }
                    // WHITE ROOKS
                    if((j == 0 || j == 7) && i == 0)
                    {
                        Rook newRook = new Rook(board[j, i], PieceColor.White);
                        whitePieces.Add(newRook);
                        board[j, i].Piece = newRook;
                    }

                    // WHITE BISHOPS
                    if((j == 2 || j == 5) && i == 0)
                    {
                        Bishop newBishop = new Bishop(board[j, i], PieceColor.White);
                        whitePieces.Add(newBishop);
                        board[j, i].Piece = newBishop;
                    }

                    // WHITE KNIGHTS
                    if ((j == 1 || j == 6) && i == 0)
                    {
                        Knight newKnight = new Knight(board[j, i], PieceColor.White);
                        whitePieces.Add(newKnight);
                        board[j, i].Piece = newKnight;
                    }

                    // WHITE QUEEN
                    if (j == 3 && i == 0)
                    {
                        Queen newQueen = new Queen(board[j, i], PieceColor.White);
                        whitePieces.Add(newQueen);
                        board[j, i].Piece = newQueen;
                    }

                    // WHITE KING
                    if (j == 4 && i == 0)
                    {
                        King newKing = new King(board[j, i], PieceColor.White);
                        whitePieces.Add(newKing);
                        board[j, i].Piece = newKing;
                        whiteKing = newKing;
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
                        blackPieces.Add(newPawn);
                        board[j, i].Piece = newPawn;
                    }
                    // BLACK ROOKS
                    if ((j == 0 || j == 7) && i == 7)
                    {
                        Rook newRook = new Rook(board[j, i], PieceColor.Black);
                        blackPieces.Add(newRook);
                        board[j, i].Piece = newRook;
                    }

                    // BLACK BISHOPS
                    if ((j == 2 || j == 5) && i == 7)
                    {
                        Bishop newBishop = new Bishop(board[j, i], PieceColor.Black);
                        blackPieces.Add(newBishop);
                        board[j, i].Piece = newBishop;
                    }

                    // BLACK KNIGHTS
                    if ((j == 1 || j == 6) && i == 7)
                    {
                        Knight newKnight = new Knight(board[j, i], PieceColor.Black);
                        blackPieces.Add(newKnight);
                        board[j, i].Piece = newKnight;
                    }

                    // BLACK QUEEN
                    if (j == 3 && i == 7)
                    {
                        Queen newQueen = new Queen(board[j, i], PieceColor.Black);
                        blackPieces.Add(newQueen);
                        board[j, i].Piece = newQueen;
                    }

                    // BLACK KING
                    if (j == 4 && i == 7)
                    {
                        King newKing = new King(board[j, i], PieceColor.Black);
                        blackPieces.Add(newKing);
                        board[j, i].Piece = newKing;
                        blackKing = newKing;
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
            // Now let's check if it's a king- this is for castling.
            // It'll be done in a helper method, so it's easier to find later.
            Castling(piece, square);

            // Now update the piece's location to match.
            piece.Location.Piece = null;                // Sets the piece's current location to null.
            piece.Location = square;                    // Sets the piece's location to the square.
            square.Piece = piece;                       // Sets the square's piece to that piece.

            return true;
        }
        /// <summary>
        /// Resets every square to its base value.
        /// </summary>
        private void ResetSquares()
        {
            for (int r = 0; r < board.GetLength(0); r++)
            {
                for (int f = 0; f < board.GetLength(1); f++)
                {
                    board[r, f].SeenReset();
                }
            }
        }
        /// <summary>
        /// Draws the attack map of every piece on the board.
        /// </summary>
        private void GetAttackMap()
        {
            // Read through each piece to get the square it attacks.
            for (int i = 0; i < blackPieces.Count; i++)
            {
                if (!blackPieces[i].IsCaptured)
                    blackPieces[i].Attack(this);
            }
            for (int i = 0; i < whitePieces.Count; i++)
            {
                if (!whitePieces[i].IsCaptured)
                    whitePieces[i].Attack(this);
            }
        }

        private void Castling(Piece piece, Square square)
        {
            // First, we have to make sure that the piece is a king (possibly including rooks later down the line).
            // If it isn't, just exit the method.
            if(piece is not King king)
            {
                return;
            }

            // Now, being able to access it, let's make sure it can castle.
            if (king.CanCastle)
            {
                if (piece.Color == PieceColor.White) // We must check for both colors, as they castle to different coordinates.
                {
                    // Save both G1 and C1 to their own variables, as they are castling squares.
                    Square G1 = board[6, 0];
                    Square C1 = board[2, 0];
                    // Now we have to check if the king is trying to move to one of those two squares.
                    if(square == G1 && !G1.East.Piece.HasMoved)
                    {
                        UnityEngine.Debug.Log("Moved king to G1");
                        Rook rook = (Rook)G1.East.Piece;
                        // Now we move the Rook.
                        rook.Location.Piece = null;
                        rook.Location = G1.West;   
                        G1.West.Piece = rook;

                        // Finally, invoke the Castled method!
                        KingCastled?.Invoke(rook, G1.West);
                    }
                    else if(square == C1 && !C1.West.West.Piece.HasMoved)
                    {
                        Rook rook = (Rook)C1.West.West.Piece;
                        // Now we move the Rook.
                        rook.Location.Piece = null;
                        rook.Location = C1.East;
                        C1.East.Piece = rook;

                        // Finally, invoke the Castled method!
                        KingCastled?.Invoke(rook, C1.East);
                    }
                }
                else // For the black king!
                {
                    // Save both G8 and C8 to their own variables, as they are castling squares.
                    Square G8 = board[6, 7];
                    Square C8 = board[2, 7];
                    // Now we have to check if the king is trying to move to one of those two squares.
                    if (square == G8 && !G8.East.Piece.HasMoved)
                    {
                        UnityEngine.Debug.Log("Moved king to G8");
                        Rook rook = (Rook)G8.East.Piece;
                        // Now we move the Rook.
                        rook.Location.Piece = null;
                        rook.Location = G8.West;
                        G8.West.Piece = rook;

                        // Finally, invoke the Castled method!
                        KingCastled?.Invoke(rook, G8.West);
                    }
                    else if (square == C8 && !C8.West.West.Piece.HasMoved)
                    {
                        Rook rook = (Rook)C8.West.West.Piece;
                        // Now we move the Rook.
                        rook.Location.Piece = null;
                        rook.Location = C8.East;
                        C8.East.Piece = rook;

                        // Finally, invoke the Castled method!
                        KingCastled?.Invoke(rook, C8.East);
                    }
                }
            }
        }

        /// <summary>
        /// Updates the data of squares using two other methods.
        /// </summary>
        public void UpdateSquareData()
        {
            ResetSquares();
            GetAttackMap();
        }
        
    }
}
