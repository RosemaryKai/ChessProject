using Chess.Game;
using Chess.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Chess.Cards
{
    /// <summary>
    /// When played on a piece, it will activate the piece's special ability.
    /// </summary>
    internal class CardPieceSpecials : AbilityCard
    {
        // FIELDS of this class

        // PROPREITES of this class

        // CTORs of this class
        public CardPieceSpecials(Player owner, string name, string description) : 
            base(owner, name, description)
        {
            cost = 4; 
        }
        // METHODS of this class
        protected override bool canPlay(GameStates gameState)
        {
            if(owner.Turn && gameState == GameStates.PlayState)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
