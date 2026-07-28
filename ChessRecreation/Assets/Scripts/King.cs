using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Chess.Game;
using Chess.GameBoard;

namespace Chess.Pieces
{
    internal class King : Piece
    {
        // FIELDS of this class
        private bool isChecked;

        // PROPERTIES of this class
        /// <summary>
        /// If the king is in check or not.
        /// </summary>
        public bool IsChecked
        {
            get
            {
                switch (color)
                {
                    case PieceColor.White:
                        return location.BlackSees;
                    case PieceColor.Black:
                        return location.WhiteSees;
                    default:
                        return false;
                }
            }
        }
        /// <summary>
        /// If the king can castle with a rook.
        /// </summary>
        public bool CanCastle
        {
            get { return !hasMoved; }
        }

        // CTORs of this class
        public King(Square location, PieceColor color) :
            base(location, color)
        {
            // Instantiation is handled in the base class!
            value = 0;
            pieceType = PieceType.King;
        }

        // METHODS of this class

        public void Update()
        {
            if (isChecked)
            {
                UnityEngine.Debug.Log("I am in check!!! " + color);
            }
        }

        public override List<Square> Move(Board board)
        {
            List<Square> squares = new List<Square>();
            List<Square> newSquares = new List<Square>();

            // The king is an odd piece. Its moves are limited based on other
            // pieces on the board, making it difficult to calculate where it can
            // and can't move. 
            // To solve this issue, we'll use a helper method that sees all possible 
            // directions the king could be checked from, returning every single square.
            // This will help determine if he is in check, or could potentially be in check 
            // if a piece moves, helping with pin logic later.

            // = = = = = = = UP SQUARE = = = = = = = 
            if (color == PieceColor.White && location.North != null && !location.North.BlackSees)
            {
                squares.Add(location.North);
            }
            if (color == PieceColor.Black && location.North != null && !location.North.WhiteSees)
            {
                squares.Add(location.North);
            }

            // = = = = = = = UP-RIGHT SQUARE = = = = = = = 
            if (color == PieceColor.White && location.NorthEast != null && !location.NorthEast.BlackSees)
            {
                squares.Add(location.NorthEast);
            }
            if (color == PieceColor.Black && location.NorthEast != null && !location.NorthEast.WhiteSees)
            {
                squares.Add(location.NorthEast);
            }

            // = = = = = = = RIGHT SQUARE = = = = = = = 
            if (color == PieceColor.White && location.East != null && !location.East.BlackSees)
            {
                squares.Add(location.East);
            }
            if (color == PieceColor.Black && location.East != null && !location.East.WhiteSees)
            {
                squares.Add(location.East);
            }

            // = = = = = = = DOWN-RIGHT SQUARE = = = = = = = 
            if (color == PieceColor.White && location.SouthEast != null && !location.SouthEast.BlackSees)
            {
                squares.Add(location.SouthEast);
            }
            if (color == PieceColor.Black && location.SouthEast != null && !location.SouthEast.WhiteSees)
            {
                squares.Add(location.SouthEast);
            }

            // = = = = = = = DOWN SQUARE = = = = = = = 
            if (color == PieceColor.White && location.South != null && !location.South.BlackSees)
            {
                squares.Add(location.South);
            }
            if (color == PieceColor.Black && location.South != null && !location.South.WhiteSees)
            {
                squares.Add(location.South);
            }

            // = = = = = = = DOWN-LEFT SQUARE = = = = = = = 
            if (color == PieceColor.White && location.SouthWest != null && !location.SouthWest.BlackSees)
            {
                squares.Add(location.SouthWest);
            }
            if (color == PieceColor.Black && location.SouthWest != null && !location.SouthWest.WhiteSees)
            {
                squares.Add(location.SouthWest);
            }

            // = = = = = = = LEFT SQUARE = = = = = = = 
            if (color == PieceColor.White && location.West != null && !location.West.BlackSees)
            {
                squares.Add(location.West);
            }
            if (color == PieceColor.Black && location.West != null && !location.West.WhiteSees)
            {
                squares.Add(location.West);
            }

            // = = = = = = = UP-LEFT SQUARE = = = = = = = 
            if (color == PieceColor.White && location.NorthWest != null && !location.NorthWest.BlackSees)
            {
                squares.Add(location.NorthWest);
            }
            if(color == PieceColor.Black && location.NorthWest != null && !location.NorthWest.WhiteSees)
            {
                squares.Add(location.NorthWest);
            }
            // If the king has not moved, check if it can truly castle.
            if (CanCastle)
            {
                List<Square> castleSquares = CastleCheck(board);
                for (int i = 0; i < castleSquares.Count; i++)
                {
                    squares.Add(castleSquares[i]);
                }
            }
            return squares;
        }

        public override List<Square> Attack(Board board)
        {
            // Make a list for the squares seen and new squares.
            List<Square> seenSquares = new List<Square>();

            // = = = = = = = UP SQUARE = = = = = = = 
            if (color == PieceColor.White && location.North != null)
            {
                seenSquares.Add(location.North);
            }
            if (color == PieceColor.Black && location.North != null)
            {
                seenSquares.Add(location.North);
            }

            // = = = = = = = UP-RIGHT SQUARE = = = = = = = 
            if (color == PieceColor.White && location.NorthEast != null)
            {
                seenSquares.Add(location.NorthEast);
            }
            if (color == PieceColor.Black && location.NorthEast != null)
            {
                seenSquares.Add(location.NorthEast);
            }

            // = = = = = = = RIGHT SQUARE = = = = = = = 
            if (color == PieceColor.White && location.East != null)
            {
                seenSquares.Add(location.East);
            }
            if (color == PieceColor.Black && location.East != null)
            {
                seenSquares.Add(location.East);
            }

            // = = = = = = = DOWN-RIGHT SQUARE = = = = = = = 
            if (color == PieceColor.White && location.SouthEast != null)
            {
                seenSquares.Add(location.SouthEast);
            }
            if (color == PieceColor.Black && location.SouthEast != null)
            {
                seenSquares.Add(location.SouthEast);
            }

            // = = = = = = = DOWN SQUARE = = = = = = = 
            if (color == PieceColor.White && location.South != null)
            {
                seenSquares.Add(location.South);
            }
            if (color == PieceColor.Black && location.South != null)
            {
                seenSquares.Add(location.South);
            }

            // = = = = = = = DOWN-LEFT SQUARE = = = = = = = 
            if (color == PieceColor.White && location.SouthWest != null)
            {
                seenSquares.Add(location.SouthWest);
            }
            if (color == PieceColor.Black && location.SouthWest != null)
            {
                seenSquares.Add(location.SouthWest);
            }

            // = = = = = = = LEFT SQUARE = = = = = = = 
            if (color == PieceColor.White && location.West != null)
            {
                seenSquares.Add(location.West);
            }
            if (color == PieceColor.Black && location.West != null)
            {
                seenSquares.Add(location.West);
            }

            // = = = = = = = UP-LEFT SQUARE = = = = = = = 
            if (color == PieceColor.White && location.NorthWest != null)
            {
                seenSquares.Add(location.NorthWest);
            }
            if (color == PieceColor.Black && location.NorthWest != null)
            {
                seenSquares.Add(location.NorthWest);
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

        public override string ToString()
        {
            return $"{color} {pieceType}; {location}, Has moved? {hasMoved}; Checked? {IsChecked}";
        }
        /// <summary>
        /// In cases that the King can castle, this method will add the square.
        /// </summary>
        /// <returns>Squares that the king could castle to.</returns>
        public List<Square> CastleCheck(Board board)
        {
            List<Square> squares = new List<Square>();
            List<Square> newSquares = new List<Square>();
            // = = = = = = = RIGHT SQUARES = = = = = = = 
            newSquares = GetRay(0, 1, board);
            if (newSquares.Count > 0)
            {
                // Make sure the squares in between the king and the rook/edge are not occupied.
                bool seesRook = true;
                for (int i = 0; i < newSquares.Count - 1; i++)
                {
                    if (newSquares[i].IsOccupied)
                    {
                        seesRook = false;
                    }
                }
                if(seesRook == true && !newSquares[newSquares.Count - 1].Piece.HasMoved)
                {
                    squares.Add(newSquares[newSquares.Count - 2]); // Add the square 2 away from the king to castle.
                    // The king will end up on that square upon a successful castle attempt.
                }
            }

            // = = = = = = = LEFT SQUARES = = = = = = = 
            newSquares = GetRay(0, -1, board);
            if (newSquares.Count > 0)
            {
                // Make sure the squares in between the king and the rook/edge are not occupied.
                bool seesRook = true;
                for (int i = 0; i < newSquares.Count - 1; i++)
                {
                    if (newSquares[i].IsOccupied)
                    {
                        seesRook = false;
                    }
                }
                if (seesRook == true && !newSquares[newSquares.Count - 1].Piece.HasMoved)
                {
                    squares.Add(newSquares[newSquares.Count - 3]); // Add the square 2 away from the king to castle.
                    // The king will end up on that square upon a successful castle attempt.
                }
            }
            return squares;
        }
    }
}
