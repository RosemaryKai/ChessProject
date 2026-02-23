using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Chess
{
    /// <summary>
    /// The Piece class to go on GameObjects. 
    /// </summary>
    internal class PieceView : MonoBehaviour
    {
        // FIELDS of this class
        private Piece piece;

        // PROPERTIES of this class
        /// <summary>
        /// The piece this PieceView is assigned to.
        /// </summary>
        public Piece Piece
        {
            get { return piece; }
            set { piece = value; }
        }

        // METHODS of this class
        public void Update()
        {
            if (piece.IsCaptured || piece.Location == null)
            {
                transform.position = new Vector3(0, 0, 0.2f);
            }
        }

    }
}
