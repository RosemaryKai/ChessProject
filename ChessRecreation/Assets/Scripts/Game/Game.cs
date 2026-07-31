using Chess.GameBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Chess.Game
{
    internal class Game
    {
        // FIELDS of this class
        private static Board board;
        private Player whitePlayer;
        private Player blackPlayer;
        private static TurnManager turnManager;
        private CardManager cardManager;
        private GameState gameState;
        // PROPERTIES of this class
        /// <summary>
        /// The player in charge of the white pieces; the white player.
        /// </summary>
        public Player WhitePlayer
        {
            get { return whitePlayer; }
        }
        /// <summary>
        /// The player in charge of the black pieces; the second player.
        /// </summary>
        public Player BlackPlayer
        {
            get { return blackPlayer; }
        }
        /// <summary>
        /// The board the entire game happens on.
        /// </summary>
        public static Board Board
        {
            get { return board; }
        }
        // CTORs of this class
        public Game(GameObject cardPrefab)
        {
            // The two players
            whitePlayer = new Player(Pieces.PieceColor.White);
            blackPlayer = new Player(Pieces.PieceColor.Black);

            board = new Board();
            // Turn Manager - Turns always start with White
            turnManager = new TurnManager();
            TurnManager.Turn = Pieces.PieceColor.White;

            // Card Manager
            cardManager = new CardManager(cardPrefab);
        }

        // METHODS of this class
        public static void FlipTurn()
        {
            turnManager.FlipTurn();
        }
    }
}
