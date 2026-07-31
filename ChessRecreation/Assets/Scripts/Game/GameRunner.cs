using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Chess.Game
{
    internal class GameRunner : MonoBehaviour
    {
        // FIELDS of this class
        private Game game;
        [SerializeField] private GameObject cardPrefab;
        // METHODS of this class
        private void Start()
        {
            game = new Game(cardPrefab);
        }
    }
}
