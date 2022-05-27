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

        /// <summary>
        /// sets default values
        /// </summary>
        void Start()
        {
            pauseFlag = false;
            SetPauseState(pauseFlag);
        }
        /// <summary>
        /// checks for esc button press to un/pause
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseFlag = !pauseFlag;
                SetPauseState(pauseFlag);
            }
        }

        /// <summary>
        /// sets that pause state
        /// </summary>
        /// <param name="_isPauseState">bool if it is paused or unpaused</param>
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
        /// <summary>
        /// restarts the level
        /// </summary>
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        /// <summary>
        /// handles quit screen input
        /// </summary>
        /// <param name="_isQuitting"></param>
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
        /// <summary>
        /// opens the quit screen
        /// </summary>
        public void OpenQuitScreen()
        {
            quitScreen.SetActive(true);
            pauseMenuScreen.SetActive(false);
        }
        /// <summary>
        /// goes to the main menu
        /// </summary>
        public void GoToMainMenu()
        {
            SceneManager.LoadScene(mainMenuSceneIndex);
        }

    }
}
