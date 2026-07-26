using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    /// <summary>
    /// A square on the chessboard.
    /// </summary>
    internal class Square
    { 
        // FIELDS of this class
        private int x; 
        private int y;
        private Piece piece;
        private bool hasHighlight;
        private bool seenByBlack;
        private bool seenByWhite;

        #region neighbor fields
        // FIELDS - These will be the neighboring squares of this square in all 8 directions (if possible). 
        private Square north;
        private Square south;
        private Square east;
        private Square west;
        private Square northwest;
        private Square northeast;
        private Square southwest;
        private Square southeast;
        #endregion

        // PROPERTIES of this class 
        /// <summary> 
        /// A get-only property for if there is a piece on this square. 
        /// </summary> 
        public bool IsOccupied 
        { 
            get { return piece != null; } 
        }
        /// <summary>
        /// The Y-Value of the square.
        /// </summary>
        public int Rank
        {
            get { return y; }
        }
        /// <summary>
        /// The X-Value of the square.
        /// </summary>
        public int File
        {
            get { return x; }
        }
        /// <summary>
        /// Property for the piece on the square.
        /// </summary>
        public Piece Piece
        {
            get { return piece; }
            set { piece = value; }
        }
        /// <summary>
        /// The name of a square, made by naming its file then rank (ex: A1).
        /// </summary>
        public string Name
        {
            get { return $"{XAxisConversion(x)}{y + 1}"; }
        }
        /// <summary>
        /// Property for if the square currently has an active highlight.
        /// </summary>
        public bool HasHighlight
        {
            get { return hasHighlight; }
            set { hasHighlight = value; }
        }
        /// <summary>
        /// If the black pieces see the square or not.
        /// </summary>
        public bool BlackSees
        {
            get { return seenByBlack; }
            set { seenByBlack = value; }
        }
        /// <summary>
        /// If the white pieces see this square or not.
        /// </summary>
        public bool WhiteSees
        {
            get { return seenByWhite; }
            set { seenByWhite = value; }
        }

        #region neighbor properties
        // PROPERTIES - These properties represent public access to their neighbors.
        // Do note this is from White's perspective. (South = North from black's perspective).
        /// <summary>
        /// The square to the north of this one.
        /// </summary>
        public Square North
        {
            get { return north; }
            private set { north = value; }
        }
        /// <summary>
        /// The square to the south of this one.
        /// </summary>
        public Square South
        {
            get { return south; }
            private set { south = value; }
        }
        /// <summary>
        /// The square to the east of this one.
        /// </summary>
        public Square East
        {
            get { return east; }
            private set { east = value; }
        }
        /// <summary>
        /// The square to the west of this one.
        /// </summary>
        public Square West
        {
            get { return west; }
            private set { west = value; }
        }
        /// <summary>
        /// The square to the northeast of this one.
        /// </summary>
        public Square NorthEast
        {
            get { return northeast; }
            private set { northeast = value; }
        }
        /// <summary>
        /// The square to the northwest of this one.
        /// </summary>
        public Square NorthWest
        {
            get { return northwest; }
            private set { northwest = value; }
        }
        /// <summary>
        /// The square to the southeast of this one.
        /// </summary>
        public Square SouthEast
        {
            get { return southeast; }
            private set { southeast = value; }
        }
        /// <summary>
        /// The square to the southwest of this one.
        /// </summary>
        public Square SouthWest
        {
            get { return southwest; }
            private set { southwest = value; }
        }
        #endregion

        // CTORS of this class
        public Square(int x, int y)
        { 
            this.x = x; 
            this.y = y;
        } 
        // METHODS of this class 
        /// <summary> 
        /// Converts the X axis to letters, as per usual chess boards. 
        /// </summary> 
        /// <param name="x">The number on the x-axis.</param> 
        /// <returns>The letter it was converted to.</returns> 
        /// <exception cref="IndexOutOfRangeException">If the board is larger than 8x8.</exception> 
        private char XAxisConversion(int x) 
        { 
            char letter; 
            // Determines which X value will be mapped to a letter on the chess board.
            switch (x) 
            { 
                case 0: 
                    letter = 'A'; 
                    break;
                case 1:
                    letter = 'B';
                    break; 
                case 2:
                    letter = 'C';
                    break; 
                case 3:
                    letter = 'D';
                    break; 
                case 4: 
                    letter = 'E'; 
                    break;
                case 5: 
                    letter = 'F'; 
                    break; 
                case 6: 
                    letter = 'G';
                    break; 
                case 7: 
                    letter = 'H';
                    break; 
                    // An exception will be thrown if the value is out of range.
                default:
                    throw new IndexOutOfRangeException("ERROR: Board cannot be larger than 8x8.");
            }
            return letter; 
        }
        /// <summary>
        /// A method to set the neighbors of the square.
        /// </summary>
        /// <param name="north">The square to the north of this one.</param>
        /// <param name="south">The square to the south of this one.</param>
        /// <param name="east">The square to the east of this one.</param>
        /// <param name="west">The square to the west of this one.</param>
        /// <param name="northwest">The square to the northwest of this one.</param>
        /// <param name="southwest">The square to the southwest of this one.</param>
        /// <param name="southeast">The square to the southeast of this one.</param>
        /// <param name="northeast">The square to the northeast of this one.</param>
        public void SetNeighbors(Square north, Square south, Square east, Square west,
            Square northwest, Square southwest, Square southeast, Square northeast)
        {
            this.north = north;
            this.south = south;
            this.east = east;
            this.west = west;
            this.northwest = northwest;
            this.southwest = southwest;
            this.northeast = northeast;
            this.southeast = southeast;
        }
        /// <summary>
        /// Resets the square to no longer be seen by either color.
        /// </summary>
        public void SeenReset()
        {
            seenByBlack = false;
            seenByWhite = false;
        }
        /// <summary>
        /// A ToString of the square.
        /// </summary>
        /// <returns>The squares coordinates as a string.</returns>
        public override string ToString() 
        {
            return $"{Name}";
        }
    }
}
