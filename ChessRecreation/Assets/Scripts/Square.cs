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
            get { return ToString(); }
        }
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
        /// A ToString of the square.
        /// </summary>
        /// <returns>The squares coordinates as a string.</returns>
        public override string ToString() 
        {
            return $"{XAxisConversion(x)}{y + 1}";
        }
    }
}
