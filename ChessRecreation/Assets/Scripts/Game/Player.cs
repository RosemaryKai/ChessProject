using Chess.Cards;
using Chess.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Game
{
    internal class Player
    {
        // FIELDS of this class
        private List<Card> hand;
        private List<Card> deck;
        private List<Piece> pieces;
        private int actionPoints;
        private PieceColor color;

        // PROPERTIES of this class
        /// <summary>
        /// How many points this player has available for moves and card plays.
        /// </summary>
        public int ActionPoints
        {
            get { return actionPoints; }
            set { actionPoints = value; }
        }
        /// <summary>
        /// If it's this player's turn or not.
        /// </summary>
        public bool Turn
        {
            get { return TurnManager.Turn == color; }
        }

        // CTORs of this class
        public Player(PieceColor color)
        {
            this.color = color;
        }
        // METHODS of this class

    }
}
