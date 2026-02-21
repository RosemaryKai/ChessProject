using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Chess
{
    /// <summary>
    /// A front facing square for GameObjects to hold.
    /// </summary>
    internal class SquareView : MonoBehaviour
    {
        // FIELDS of this class
        private Square square;

        // PROPERTIES of this class
        /// <summary>
        /// A get-only property for the square this GameObject occupies.
        /// </summary>
        public Square Square
        {
            get { return square; }
        }
        // AWAKE of this class
        public void Awake()
        {

        }

        // METHODS of this class
    }
}
