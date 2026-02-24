using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chess
{
    public class GameManager : MonoBehaviour
    {
        // FIELDS of this class
        private static GameManager instance;
        private PieceColor playerChoice;
        [SerializeField] private GameObject colorMenu;

        // PROPERTIES of this class
        /// <summary>
        /// The color chosen by hthe player.
        /// </summary>
        public PieceColor PlayerChoice
        {
            get { return playerChoice; }
        }

        public static GameManager Instance
        {
            get { return instance; }
        }
        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(this);
        }


        void Update()
        {

        }

        public void OpenColorMenu()
        {
            colorMenu.SetActive(true);
        }
        public void CloseColorMenu()
        {
            colorMenu.SetActive(false);
        }
        public void WhitePicked()
        {
            playerChoice = PieceColor.White;
            SceneManager.LoadScene("GameScene");
        }
        public void BlackPicked()
        {
            playerChoice = PieceColor.Black;
            SceneManager.LoadScene("GameScene");
        }
    }
}

