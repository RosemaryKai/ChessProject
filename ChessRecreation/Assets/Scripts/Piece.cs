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
    /// <summary> 
    /// A piece on the chess board.
    /// </summary> 
    internal abstract class Piece 
    {
        // FIELDS of this class
        protected Square location; 
        protected bool isCaptured;
        private PieceColor color;
        protected bool hasMoved;

        // PROPERTIES of this class 
        /// <summary> 
        /// If the piece is captured or not. 
        /// </summary> 
        public bool IsCaputed 
        { 
            get { return isCaptured; }
        }

        public Square Location
        {
            get { return location; }
            set { location = value; }
        }

        // CTORS of this class 
        public Piece(Square startingLocation, PieceColor color)
        { 
            location = startingLocation;
            isCaptured = false;
            this.color = color;
        }
    }
}
