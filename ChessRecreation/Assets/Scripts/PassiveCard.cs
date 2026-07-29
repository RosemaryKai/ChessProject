using Chess.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Cards
{
    /// <summary>
    /// Passive Cards will only bring short term affects or buff allied pieces.
    /// </summary>
    internal abstract class PassiveCard : Card
    {
        // FIELDS of this class

        // PROPERTIES of this class

        // CTORs of this class
        /// <summary>
        /// Constructor for a Passive Card.
        /// </summary>
        public PassiveCard(string name, string description) : 
            base(name, description)
        {
            
        }
        // METHODS of this class
        protected abstract override bool canPlay(GameState gameState);

    }
}
