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
        [SerializeField] private GameObject squarePrefab;
        [SerializeField] private Material darkSquare;
        [SerializeField] private Material lightSquare;
        [SerializeField] private Transform boardParent;

        // CTORs of this class
        public void Awake() 
        { 
            // Create a new Board object.
            board = new Board();

            // Now create a variable to store the color of the square.
            Material color;
            
            // Now let's iterate through the boards...
            for (int r = 0; r < board.Ranks; r++)
            {
                for (int f = 0; f < board.Files; f++)
                {
                    // Even squares are dark.
                    if ((r + f) % 2 == 0)
                    {
                        color = darkSquare;
                    }
                    // Odd squares are light.
                    else
                    {
                        color = lightSquare;
                    }
                    // Now instantiate that square.
                    GameObject newSquare = Instantiate(squarePrefab,
                        new Vector3(r, f, 0),
                        new Quaternion(),
                        boardParent);

                    // Grabs the SquareView component off of the GameObject.
                    SquareView view = newSquare.GetComponent<SquareView>();

                    // Get the SpriteRenderer as well, to change the material
                    SpriteRenderer renderer = newSquare.GetComponent<SpriteRenderer>();
                    renderer.material = color;
                }
            }
        }

        // METHODS of this class


    }
}
