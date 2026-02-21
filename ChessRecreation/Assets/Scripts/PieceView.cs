using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Chess
{
    internal class PieceView : MonoBehaviour
    {
        // FIELDS of this class
        private Piece piece;

        // PROPERTIES of this class
        public Piece Piece
        {
            get { return piece; }
            set { piece = value; }
        }

        // METHODS of this class
        public void Update()
        {

        }

    }
}
