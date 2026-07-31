using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Chess.Unity
{
    /// <summary>
    /// Displays all the cards by doing positional calculations
    /// </summary>
    internal class HandView : MonoBehaviour
    {
        // FIELDS of  this class
        private List<CardView> cardViews;

        // PROPERTIES of this class
        private float Offset
        {
            get { return ((cardViews.Count - 1) * 5 / 2); }
        }

        // METHODS of this class
    }
}
