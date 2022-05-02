using UnityEngine;
using UnityEngine.SceneManagement;

namespace AutoDefense
{
    public class PauseMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject pausedGO;
        [SerializeField] private GameObject unpausedGO;
        [SerializeField] private GameObject pauseMenuScreen;
        [SerializeField] private GameObject quitScreen;
        [SerializeField] private int mainMenuSceneIndex;
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
                unpausedGO.SetActive(false);
                pausedGO.SetActive(true);
                pauseMenuScreen.SetActive(true);
                quitScreen.SetActive(false);
            }
            else
            {
                pauseFlag = false;
                pausedGO.SetActive(false);
                unpausedGO.SetActive(true);
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
