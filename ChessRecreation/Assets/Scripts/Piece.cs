using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Chess
{
    // An enumeration for the two different colors in chess.
    public enum PieceColor 
    {
        White,
        Black 
    } 

    // An enumeration for the pieces of chess.
    public enum PieceType
    {
        Pawn,
        Rook,
        Knight,
        Bishop,
        Queen,
        King
    }
    /// <summary> 
    /// A piece on the chess board.
    /// </summary> 
    internal abstract class Piece 
    {
        // FIELDS of this class
        protected int value;
        protected Square location; 
        protected bool isCaptured;
        protected PieceColor color;
        protected PieceType pieceType;
        protected bool hasMoved;

        // PROPERTIES of this class 
        /// <summary> 
        /// If the piece is captured or not. 
        /// </summary> 
        public bool IsCaptured 
        { 
            get { return isCaptured; }
            set { isCaptured = value; }
        }
        /// <summary>
        /// The location of the piece.
        /// </summary>
        public Square Location
        {
            get { return location; }
            set { location = value; }
        }
        /// <summary>
        /// Read-Only property for the color of the piece.
        /// </summary>
        public PieceColor Color
        {
            get { return color; }
        }
        /// <summary>
        /// Read-Only property for the piece's type.
        /// </summary>
        public PieceType PieceType
        {
            get { return pieceType; }
        }
        /// <summary>
        /// Read-Only property for the value of a piece.
        /// </summary>
        public int Value
        {
            get { return value; }
        }
        /// <summary>
        /// If the piece has moved or not.
        /// </summary>
        public bool HasMoved
        {
            get { return hasMoved; }
            set { hasMoved = value; }
        }

        // CTORS of this class 
        public Piece(Square startingLocation, PieceColor color)
        { 
            location = startingLocation;
            this.color = color;
            isCaptured = false;
            hasMoved = false;
        }

        // METHODS of this class
        /// <summary>
        /// What a piece could see if they were not blocked.
        /// </summary>
        /// <returns>A list of squares the piece could see.</returns>
        public abstract List<Square> Move(Board board);
        /// <summary>
        /// The squares the piece attacks.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public abstract List<Square> Attack(Board board); // In pretty much every attack method, the piece in question will use
        // the GetSeenSquare method to check every square until it runs into something it cannot move through. 

        public override string ToString()
        {
            return $"{color} {pieceType}; {location}, Has moved? {hasMoved}";
        }
        /// <summary>
        /// Draws a line of every square the piece could move to from its current location if there were nothing in the way.
        /// </summary>
        /// <param name="rankStep">How far the piece moves on the rank.</param>
        /// <param name="fileStep">How far the piece moves on the file.</param>
        /// <param name="board">The board.</param>
        /// <returns>The drawn ray in form of a list of squares.</returns>
        /// <exception cref="Exception">If both the Rank and the File steps are 0.</exception>
        protected List<Square> GetRay(int rankStep, int fileStep, Board board)
        {
            // Make sure the piece is actually taking steps.
            // If it's not? Throw an exception.
            if (fileStep == 0 && rankStep == 0)
            {
                throw new Exception("ERROR: Both steps cannot be zero.");
            }

            // Create a new list of squares to be returned.
            List<Square> squares = new List<Square>();

            // Also create a current square variable, starting at the piece's location.
            // We mainly need it to get its values.
            Square currentSquare = location;
            int currentFile = currentSquare.File;
            int currentRank = currentSquare.Rank;

            // First deal with diagonals.
            if (rankStep != 0 && fileStep != 0)
            {
                // Keep adding until we cannot anymore!!!
                while (currentSquare.Rank + rankStep >= 0 && currentSquare.Rank + rankStep < board.Ranks
                    && currentSquare.File + fileStep >= 0 && currentSquare.File + fileStep < board.Files)
                {
                    // Increment based on the values we're given.
                    currentRank += rankStep;
                    currentFile += fileStep;
                    // Then find that square, and add it to the list.
                    currentSquare = board[currentFile, currentRank];

                    // Unlike the upcoming method, we don't need to check for anything.
                    // Every square will be added.
                }
            }

            // Now begin to add the squares based on the direction asked for.
            // We'll deal with files next...
            if (fileStep != 0)
            {
                while (currentFile + fileStep < board.Files && currentFile + fileStep >= 0)
                {
                    currentFile += fileStep;
                    currentSquare = board[currentFile, currentRank];

                    // If the square is not occupied? Add it to the list.
                    if (!currentSquare.IsOccupied)
                    {
                        squares.Add(currentSquare);
                    }
                }
            }

            // Now deal with ranks.
            if (rankStep != 0)
            {
                while (currentRank + rankStep < board.Ranks && currentRank + rankStep >= 0)
                {
                    currentRank += rankStep;
                    currentSquare = board[currentFile, currentRank];

                    // If the square is not occupied? Add it to the list.
                    if (!currentSquare.IsOccupied)
                    {
                        squares.Add(currentSquare);
                    }
                }
            }
            // Return the list of squares.
            return squares;
        }
        /// <summary>
        /// Draws a raycast through squares with a given direction.
        /// </summary>
        /// <param name="rankStep">How far we step in the ranks.</param>
        /// <param name="fileStep">How far we step in the files.</param>
        /// <param name="board">The board.</param>
        /// <returns>A list of the squares.</returns>
        /// <exception cref="Exception">If rank & file are both zero.</exception>
        protected List<Square>GetDirection(int rankStep, int fileStep, Board board)
        {
            // Make sure the piece is actually taking steps.
            // If it's not? Throw an exception.
            if(fileStep == 0 && rankStep == 0)
            {
                throw new Exception("ERROR: Both steps cannot be zero.");
            }

            // Create a new list of squares to be returned.
            List<Square> squares = new List<Square>();

            // Also create a current square variable, starting at the piece's location.
            // We mainly need it to get its values.
            Square currentSquare = location;
            int currentFile = currentSquare.File;
            int currentRank = currentSquare.Rank;

            // First deal with diagonals.
            if(rankStep != 0 && fileStep != 0)
            {
                // Keep adding until we cannot anymore!!!
                while(currentSquare.Rank + rankStep >= 0 && currentSquare.Rank + rankStep < board.Ranks
                    && currentSquare.File + fileStep >= 0 && currentSquare.File + fileStep < board.Files)
                {
                    // Increment based on the values we're given.
                    currentRank += rankStep;
                    currentFile += fileStep;
                    // Then find that square, and add it to the list.
                    currentSquare = board[currentFile, currentRank];

                    // If the square is not occupied? Add it to the list.
                    if (!currentSquare.IsOccupied)
                    {
                        squares.Add(currentSquare);
                    }
                    // If it is occupied, but there's a friendly piece there? Don't add it.
                    else if (currentSquare.Piece.Color == color)
                        break;
                    // If there's an enemy piece there? Add it, but stop.
                    else
                    {
                        squares.Add(currentSquare);
                        break;
                    }
                }
                // Finally, return the list.
                return squares;
            }

            // Now begin to add the squares based on the direction asked for.
            // We'll deal with files next...
            if(fileStep != 0)
            {
                while (currentFile + fileStep < board.Files && currentFile + fileStep >= 0)
                {
                    currentFile += fileStep;
                    currentSquare = board[currentFile, currentRank];

                    // If the square is not occupied? Add it to the list.
                    if (!currentSquare.IsOccupied)
                    {
                        squares.Add(currentSquare);
                    }
                    // If it is occupied, but there's a friendly piece there? Don't add it.
                    else if (currentSquare.Piece.Color == color)
                        break;
                    // If there's an enemy piece there? Add it, but stop.
                    else
                    {
                        squares.Add(currentSquare);
                        break;
                    }
                }
            }
            
            // Now deal with ranks.
            if(rankStep != 0)
            {
                while(currentRank + rankStep < board.Ranks && currentRank + rankStep >= 0)
                {
                    currentRank += rankStep;
                    currentSquare = board[currentFile, currentRank];

                    // If the square is not occupied? Add it to the list.
                    if (!currentSquare.IsOccupied)
                    {
                        squares.Add(currentSquare);
                    }
                    // If it is occupied, but there's a friendly piece there? Don't add it.
                    else if (currentSquare.Piece.Color == color)
                        break;
                    // If there's an enemy piece there? Add it, but stop.
                    else
                    {
                        squares.Add(currentSquare);
                        break;
                    }
                }
            }
            // Return the list of squares.
            return squares;
        }
        /// <summary>
        /// Draws a line stopping on the square of anything that gets in its way.
        /// </summary>
        /// <param name="rankStep">How far it moves on the rank.</param>
        /// <param name="fileStep">How far it moves on the file.</param>
        /// <param name="board">The board.</param>
        /// <returns>The squares it found.</returns>
        protected List<Square> GetSeenSquares(int rankStep, int fileStep, Board board)
        {
            // Make sure the piece is actually taking steps.
            // If it's not? Throw an exception.
            if (fileStep == 0 && rankStep == 0)
            {
                throw new Exception("ERROR: Both steps cannot be zero.");
            }

            // Create a new list of squares to be returned.
            List<Square> squares = new List<Square>();

            // Also create a current square variable, starting at the piece's location.
            // We mainly need it to get its values.
            Square currentSquare = location;
            int currentFile = currentSquare.File;
            int currentRank = currentSquare.Rank;

            // First deal with diagonals.
            if (rankStep != 0 && fileStep != 0)
            {
                // Keep adding until we cannot anymore!!!
                while (currentSquare.Rank + rankStep >= 0 && currentSquare.Rank + rankStep < board.Ranks
                    && currentSquare.File + fileStep >= 0 && currentSquare.File + fileStep < board.Files)
                {
                    // Increment based on the values we're given.
                    currentRank += rankStep;
                    currentFile += fileStep;
                    // Then find that square, and add it to the list.
                    currentSquare = board[currentFile, currentRank];

                    // If the square is not occupied? Add it to the list.
                    if (!currentSquare.IsOccupied)
                    {
                        squares.Add(currentSquare);
                    }
                    // We, however, stop at a piece. We still include its square.
                    else
                    {
                        squares.Add(currentSquare);
                        break;
                    }
                }
                // Finally, return the list.
                return squares;
            }

            // Now begin to add the squares based on the direction asked for.
            // We'll deal with files next...
            if (fileStep != 0)
            {
                while (currentFile + fileStep < board.Files && currentFile + fileStep >= 0)
                {
                    currentFile += fileStep;
                    currentSquare = board[currentFile, currentRank];

                    // If the square is not occupied? Add it to the list.
                    if (!currentSquare.IsOccupied)
                    {
                        squares.Add(currentSquare);
                    }
                    // Add the square, even if it is occupied, but stop.
                    else
                    {
                        squares.Add(currentSquare);
                        break;
                    }
                }
            }

            // Now deal with ranks.
            if (rankStep != 0)
            {
                while (currentRank + rankStep < board.Ranks && currentRank + rankStep >= 0)
                {
                    currentRank += rankStep;
                    currentSquare = board[currentFile, currentRank];

                    // If the square is not occupied? Add it to the list.
                    if (!currentSquare.IsOccupied)
                    {
                        squares.Add(currentSquare);
                    }
                    // Stop when an occupied square is found, but still add it.
                    else
                    {
                        squares.Add(currentSquare);
                        break;
                    }
                }
            }
            // Return the list of squares.
            return squares;
        }
        /// <summary>
        /// Sets a pieces values when it's captured.
        /// </summary>
        public void Captured()
        {
            isCaptured = true;
            location.Piece = null;
            location = null;
        }
    }
}
