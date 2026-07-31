using Chess.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Cards
{
    /// <summary>
    /// A card that will affect large parts of the board, usually targetting squares.
    /// </summary>
    internal abstract class GlobalCard : Card
    {
        // FIELDS of this class

        // PROPERTIES of this class

        // CTORs of this class
        /// <summary>
        /// Constructor for a Global Card Card.
        /// </summary>
        public GlobalCard(Player owner, string name, string description) :
            base(owner, name, description)
        {

        }
        // METHODS of this class
        protected abstract override bool canPlay(GameStates gameState);

    }
}
