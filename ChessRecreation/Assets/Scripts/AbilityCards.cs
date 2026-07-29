using Chess.Game;
using Chess.Pieces;
using Chess.GameBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Chess.Cards
{
    /// <summary>
    /// Ability cards will empower allied pieces.
    /// </summary>
    internal abstract class AbilityCard : Card
    {
        // FIELDS of this class
        protected PieceType targetPieceType;

        // PROPERTIES of this class
        /// <summary>
        /// The piece type this specific ability targets.
        /// </summary>
        public PieceType TargetPieceType
        {
            get { return targetPieceType; }
        }
        // CTORs of this class
        public AbilityCard(string name, string description, PieceType targetPieceType) :
            base(name, description)
        {
            this.targetPieceType = targetPieceType;
        }

        // METHODS of this class
        protected abstract override bool canPlay(GameState gameState);
    }
}
