using Chess.Game;
using Chess.GameBoard;
using Chess.Pieces;
using Chess.Unity;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Chess.Cards
{
    /// <summary>
    /// The Card class is the basis of every card in the game.
    /// </summary>
    internal abstract class Card
    {
        // FIELDS of this class
        protected Player owner;
        protected List<Piece> affectedPieces;
        protected List<Square> affectedSquares;
        protected string name;
        protected string description;
        protected int cost;
        protected GameState gameState;

        // Events
        public static event Action CardPlayed;

        // PROPERTIES of this class
        /// <summary>
        /// The owner of the card.
        /// </summary>
        public Player Owner
        {
            get { return owner; }
        }
        /// <summary>
        /// The name of the card.
        /// </summary>
        public string Name
        {
            get { return name; }
        }
        /// <summary>
        /// The description of the card.
        /// </summary>
        public string Description
        {
            get { return description; }
        }
        /// <summary>
        /// The amount the card costs to be played.
        /// </summary>
        public int Cost
        {
            get { return cost; }
        }
        /// <summary>
        /// If the card can be played.
        /// </summary>
        public bool CanPlay
        {
            get { return canPlay(gameState); }
        }
        // CTORs of this class
        /// <summary>
        /// A card that can be played.
        /// </summary>
        /// <param name="name">The name of the card.</param>
        public Card(Player owner, string name, string description)
        {
            this.owner = owner;
            this.name = name;
            this.description = description;
        }
        // METHODS of this class
        /// <summary>
        /// If the card can be played or not.
        /// </summary>
        /// <param name="gameState">The current state of the game.</param>
        /// <returns>True if the card can be played, false otherwise.</returns>
        protected abstract bool canPlay(GameState gameState);
    }
}
