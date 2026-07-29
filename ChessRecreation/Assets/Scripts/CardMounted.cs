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
    /// When played on a Knight, this card will cause the Knight to deal damage in the 3 squares nearest to the square it landed (based on direction). 
    /// </summary>
    internal class CardMounted : AbilityCard
    {
        // FIELDS of this class

        // PROPREITES of this class

        // CTORs of this class
        public CardMounted(string name, string description, PieceType targetPieceType) : 
            base(name, description, targetPieceType)
        {
            cost = 2; 
        }
        // METHODS of this class
        protected override bool canPlay(GameState gameState)
        {
            throw new NotImplementedException();
        }
    }
}
