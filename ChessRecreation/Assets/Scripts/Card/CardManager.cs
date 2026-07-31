using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Chess.Cards;
using Chess.Unity;

namespace Chess.Game
{
    internal class CardManager
    {
        // FIELDS of this class
        private Dictionary<Card, CardView> cardViews;
        private GameObject cardPrefab;
        // PROPERTIES of this class

        // CTORs of this class
        public CardManager(GameObject cardPrefab)
        {
            this.cardPrefab = cardPrefab;
        }

        // METHODS of this class
        public void InitialDraw()
        {

        }
    }
}
