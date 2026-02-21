using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public PieceType PieceType
        {
            get { return pieceType; }
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
        public abstract List<Square> Vision(Board board);

        public override string ToString()
        {
            return $"{color} {pieceType}; {location}";
        }
    }
}
