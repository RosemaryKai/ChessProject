using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Chess
{
    internal class BoardView : MonoBehaviour
    {
        // FIELDS of this class
        private Board board; 

        // CTORs of this class
        public void Awake() 
        { 
            board = new Board();
        }
    }
}
