using UnityEngine;
using UnityEngine.SceneManagement;

namespace AutoDefense
{
    /// <summary>
    /// manages pause menu logic
    /// </summary>
    public class PauseMenuManager : MonoBehaviour
    {
        /// <summary>
        /// pause menu
        /// </summary>
        [SerializeField] private GameObject pausedGO;
        /// <summary>
        /// main pause menu screen object
        /// </summary>
        [SerializeField] private GameObject pauseMenuScreen;
        /// <summary>
        /// quit screen go
        /// </summary>
        [SerializeField] private GameObject quitScreen;
        /// <summary>
        /// index of the main menu scene
        /// </summary>
        [SerializeField] private int mainMenuSceneIndex;
        /// <summary>
        /// flag if game is paused or not
        /// </summary>
        private bool pauseFlag;

        // Start is called before the first frame update
        void Start()
        {
            pauseFlag = false;
            SetPauseState(pauseFlag);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseFlag = !pauseFlag;
                SetPauseState(pauseFlag);
            }
        }

        public void SetPauseState(bool _isPauseState)
        {
            if (_isPauseState)
            {
                Time.timeScale = 0;
                pauseFlag = true;
                pausedGO.SetActive(true);
                pauseMenuScreen.SetActive(true);
                quitScreen.SetActive(false);
            }
            else
            {
                pauseFlag = false;
                pausedGO.SetActive(false);
                Time.timeScale = 1;
            }
        }
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        public void HandleQuitScreen(bool _isQuitting)
        {
            if (_isQuitting)
            {
                Debug.LogError("Quitting the Game");
                Application.Quit();
            }
            else
            {
                SetPauseState(true);
            }
        }
        public void OpenQuitScreen()
        {
            quitScreen.SetActive(true);
            pauseMenuScreen.SetActive(false);
        }
        public void GoToMainMenu()
        {
            SceneManager.LoadScene(mainMenuSceneIndex);
        }

    }
}
