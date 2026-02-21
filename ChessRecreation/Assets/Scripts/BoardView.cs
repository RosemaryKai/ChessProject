using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Chess
{
    /// <summary>
    /// The frontend facing Board for the game.
    /// </summary>
    internal class BoardView : MonoBehaviour
    {
        // FIELDS of this class
        private Board board;
        private Piece selectedPiece;
        [SerializeField] private GameObject squarePrefab;
        [SerializeField] private GameObject whiteRookPrefab;
        [SerializeField] private GameObject blackRookPrefab;
        [SerializeField] private Material darkSquare;
        [SerializeField] private Material lightSquare;
        [SerializeField] private Transform boardParent;

        // METHODS of this class
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
                    // SQUARE INSTANTIATION
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
                    SquareView sView = newSquare.GetComponent<SquareView>();
                    sView.Square = board[r, f];

                    // Get the SpriteRenderer as well, to change the material
                    SpriteRenderer renderer = newSquare.GetComponent<SpriteRenderer>();
                    renderer.material = color;

                    // PIECE INSTANTIATION
                    // If there's a piece on the square...
                    if (sView.Square.IsOccupied)
                    {
                        // Make an ambiguous piece variable that will equal whatever occupies it.
                        Piece piece = sView.Square.Piece;
                        GameObject newPiece;

                        // ROOK INSTANTIATION
                        if (piece is Rook)
                        {
                            // Determine which prefab the rook will use based on the color.
                            if(piece.Color == PieceColor.White)
                            {
                                newPiece = Instantiate(whiteRookPrefab,
                                new Vector3(r, f, -0.1f),
                                new Quaternion(),
                                boardParent);
                            }
                            else
                            {
                                newPiece = Instantiate(blackRookPrefab,
                                new Vector3(r, f, -0.1f),
                                new Quaternion(),
                                boardParent);
                            }

                            // Now get the component of the piece- and assign its piece to itself.
                            PieceView pView = newPiece.GetComponent<PieceView>();
                            pView.Piece = sView.Square.Piece;
                        }
                    }
                }
            }
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Click();
            }
            if (selectedPiece != null)
            {
                Debug.Log("Selected Piece: " + selectedPiece);
                selectedPiece = null;
            }
        }
        /// <summary>
        /// Clicking on GameObjects, for debug purposes.
        /// </summary>
        public void Click()
        {
            // Create a raycast.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // If the raycast hits something...
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Check if it's a piece first.
                PieceView pieceView = hit.collider.GetComponent<PieceView>();

                // If it is? Get that piece's data, select the piece and print
                // its data.
                if (pieceView != null)
                {
                    Debug.Log($"{pieceView.Piece}");
                    selectedPiece = pieceView.Piece;
                    return;
                }

                // If it's not a piece, it's a square. So get that sView..
                SquareView squareView = hit.collider.GetComponent<SquareView>();

                // If the squareview exists? Print that data and exit the method.
                if (squareView != null)
                {
                    Debug.Log($"{squareView.Square}");
                    return;
                }
            }
        }

    }
}
