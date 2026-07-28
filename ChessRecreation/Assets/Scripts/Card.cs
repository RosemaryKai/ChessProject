using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Chess.Unity;
using Chess.GameBoard;
using Chess.Game;

namespace Chess.Cards
{
    internal abstract class Card
    {
        // FIELDS of this class
        protected string name;
        protected string description;
        protected int cost;
        protected GameState gameState;

        // Events
        public static event Action CardPlayed;

        // PROPERTIES of this class
        /// <summary>
        /// The name of the card.
        /// </summary>
        public string Name
        {
            get { return name; }
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
        /// <param name="cost">How much the card costs to be played.</param>
        public Card(string name, int cost)
        {
            this.name = name;
            this.cost = cost;
        }
        // METHODS of this class
        /// <summary>
        /// If the card can be played or not.
        /// </summary>
        /// <param name="gameState">The current state of the game.</param>
        /// <returns>True if the card can be played, false otherwise.</returns>
        public abstract bool canPlay(GameState gameState);
    }
}
