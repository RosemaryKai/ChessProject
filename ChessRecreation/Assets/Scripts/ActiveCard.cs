using Chess.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Cards
{
    /// <summary>
    /// Cards that will often be aggressive or confrontational, affecting enemy pieces.
    /// </summary>
    internal abstract class ActiveCard : Card
    {
        // FIELDS of this class

        // PROPERTIES of this class

        // CTORs of this class
        /// <summary>
        /// Constructor for an Active Card.
        /// </summary>
        public ActiveCard(string name, string description) :
            base(name, description)
        {

        }
        // METHODS of this class
        protected abstract override bool canPlay(GameState gameState);

    }
}
