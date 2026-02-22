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
        private Piece previouslySelectedPiece;

        private List<GameObject> activeHighlights;

        // Dictionaries for matching pieces/squares to their MonoBehaviour scripts!
        private Dictionary<Piece, PieceView> pieceViews;
        private Dictionary<Square, SquareView> squareViews;

        // Objects and fields required from Unity.
        [SerializeField] private GameObject squarePrefab;
        [SerializeField] private GameObject whiteRookPrefab;
        [SerializeField] private GameObject blackRookPrefab;
        [SerializeField] private GameObject highlightPrefab;
        [SerializeField] private GameObject pieceHighlightPrefab;
        [SerializeField] private Material darkSquare;
        [SerializeField] private Material lightSquare;
        [SerializeField] private Transform boardParent;

        // METHODS of this class
        public void Awake() 
        { 
            // Create a new Board object.
            board = new Board();

            // Instantiate the dictionaries & list.
            pieceViews = new Dictionary<Piece, PieceView>();
            squareViews = new Dictionary<Square, SquareView>();
            activeHighlights = new List<GameObject>();

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
                    // Also make it active so it can be seen!
                    SquareView sView = newSquare.GetComponent<SquareView>();
                    sView.Square = board[r, f];
                    newSquare.SetActive(true);

                    // Get the SpriteRenderer as well, to change the material
                    SpriteRenderer renderer = newSquare.GetComponent<SpriteRenderer>();
                    renderer.material = color;

                    // Now add that square and squareview to the dictionary.
                    squareViews.Add(sView.Square, sView);

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
                                newPiece = Instantiate(whiteRookPrefab,     // Prefab for the rook
                                new Vector3(r, f, -0.1f),                   // Rooks location
                                new Quaternion(),                           // Rooks rotation (0)
                                boardParent);                               // Rooks transform parent
                            }
                            else
                            {
                                newPiece = Instantiate(blackRookPrefab,     // Prefab for the rook
                                new Vector3(r, f, -0.1f),                   // Rooks location
                                new Quaternion(),                           // Rooks rotation (0)
                                boardParent);                               // Rooks transform parent
                            }

                            // Now get the component of the piece- and assign its piece to itself.
                            // Also make it active so it can be seen!
                            PieceView pView = newPiece.GetComponent<PieceView>();
                            pView.Piece = sView.Square.Piece;
                            newPiece.SetActive(true);

                            // Finally, add that piece and its pieceview to the dictionary.
                            pieceViews.Add(pView.Piece, pView);
                        }
                    }
                }
            }
        }

        public void Update()
        {
            // If there's a click, enter the click method
            if (Input.GetMouseButtonDown(0))
            {
                OnClick();
            }
            // If a a piece was recently selected, print it to the debug window.
            if (selectedPiece != null && previouslySelectedPiece != selectedPiece)
            {
                Debug.Log("Selected Piece: " + selectedPiece);
                previouslySelectedPiece = selectedPiece;
            }
            // If there is no piece selected, clear highlighted squares.
            if (selectedPiece == null)
            {
                ClearHighlights();
            }
            
        }
        /// <summary>
        /// Clicking on GameObjects, for moving.
        /// </summary>
        public void OnClick()
        {
            // Create a raycast.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Create a pieceview and a squareview.
            SquareView squareView;
            PieceView pieceView;

            // If the raycast hits something...
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // ============ PIECES ============
                // Check if it's a piece by getting its Pieceview...
                pieceView = hit.collider.GetComponent<PieceView>();

                // If the pieceview isn't null? It's a piece.
                if (pieceView != null)
                {
                    // Now check that there isn't already a piece selected- this is for captures.
                    // There is a selected piece...
                    if(selectedPiece != null)
                    {
                        // Get the square that piece is on.
                        Square square = pieceView.Piece.Location;

                        // Now check the team of that piece. 
                        // If it's the same color? Select the piece.
                        if(pieceView.Piece.Color == selectedPiece.Color)
                        {
                            selectedPiece = pieceView.Piece;

                            // Also update the highlights to match!
                            ClearHighlights();
                            HighlightMoves(selectedPiece);
                            return;
                        }
                        // if it's not? It's an enemy- we capture it.
                        else
                        {
                            // Capture it using logic in the Board class.
                            bool capture = board.TryCapture(selectedPiece, square, pieceViews, squareViews);

                            // If the capture was succesful, move the selected piece.
                            if (capture)
                            {
                                // Now visually move that piece to the formerly occupied square.
                                pieceView = pieceViews[selectedPiece];        // Grab the PieceView of the piece.
                                squareView = squareViews[square];             // Grab the SquareView of the square.
                                pieceView.transform.position = squareView.transform.position +
                                    new UnityEngine.Vector3(0, 0, -0.1f);     // Move the piece prefab to that square.
                            }
                            Debug.Log($"Did it capture? {capture}");

                            // No matter what, nullify the selected piece.
                            selectedPiece = null;
                        }
                    }
                    // If we don't have a selected piece? Select the piece we clicked on. 
                    else
                    {
                        Debug.Log($"{pieceView.Piece}");
                        selectedPiece = pieceView.Piece;
                        HighlightMoves(selectedPiece);
                        return;
                    }
                }

                // ============ SQUARES ============
                // Now check if it's a square.
                squareView = hit.collider.GetComponent<SquareView>();

                // If it is a square, squareView won't be null.
                if (squareView != null)
                {
                    Debug.Log($"{squareView.Square}");

                    // Now let's move a piece- if it's selected.
                    if (selectedPiece != null)
                    {
                        bool moved = board.TryMove(selectedPiece, squareView.Square);
                        Debug.Log($"Did it move? {moved}");

                        // Move the prefab to the new square- if the move was successful.
                        if (moved)
                        {
                            PieceView pView = pieceViews[selectedPiece];
                            pView.transform.position = squareView.transform.position
                                + new Vector3(0, 0, -0.1f);
                        }

                        // Finally, de-select the piece.
                        selectedPiece = null;
                    }
                    // If it's not a move? Just de-select the selected piece.
                    else
                    {
                        selectedPiece = null;
                    }
                        return;
                }
            }
        }

        public void HighlightMoves(Piece piece)
        {
            // If the piece sent in is null, immediately exit the method.
            if(piece == null)
            {
                return;
            }

            // Now get the list of squares that the piece can see.
            List<Square> vision = piece.Vision(board);

            // Now that we have the piece's vision, we need to instantiate highlights
            // on those squares.
            // To do that, we need the position of said squares- we can access that
            // using its SquareView from the dictionary.
            for (int i = 0; i < vision.Count; i++)
            {
                GameObject highlight;
                // If the square is empty, use the empty square highlight.
                if (!vision[i].IsOccupied)
                {
                    highlight = Instantiate(highlightPrefab,
                        new Vector3(vision[i].File, vision[i].Rank, -0.1f),
                        new Quaternion(),
                        squareViews[vision[i]].transform);

                    // Add the highlight to the list.
                    activeHighlights.Add(highlight);
                }
                // If the piece occupying the square is the color of the one selected?
                // Do NOT highlight the square.
                else if (vision[i].Piece.Color == piece.Color)
                {
                    continue;
                }
                // If the piece is an enemy piece? Highlight that square, but with
                // a different prefab.
                else
                {
                    highlight = Instantiate(pieceHighlightPrefab,
                        new Vector3(vision[i].File, vision[i].Rank, -0.3f),
                        new Quaternion(),
                        squareViews[vision[i]].transform);

                    // Add the highlight to the list.
                    activeHighlights.Add(highlight);
                }

            }
        }

        public void ClearHighlights()
        {
            for (int i = 0; i < activeHighlights.Count; i++)
            {
                Destroy(activeHighlights[i]);
            }
            activeHighlights.Clear();
        }

    }
}
