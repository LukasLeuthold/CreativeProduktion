using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AutoDefense
{
    public class GameOverMenuManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private GameObject quitScreen;
        [SerializeField] private int mainMenuSceneIndex;

        [SerializeField] private AUDIOScriptableEvent GameOver_Won;
        [SerializeField] private AUDIOScriptableEvent GameOver_Lost;

        void Start()
        {
            quitScreen.SetActive(false);
        }

        public void SetGameOverScreen(bool _isWinner)
        {
            if (_isWinner)
            {
                titleText.text = "winner";
                GameOver_Won?.Raise();
            }
            else
            {
                titleText.text = "looser";
                GameOver_Lost?.Raise();
            }
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
                OpenGameOverScreen();
            }
        }
        public void OpenGameOverScreen()
        {
            quitScreen.SetActive(false);
            gameOverScreen.SetActive(true);
        }
        public void OpenQuitScreen()
        {
            quitScreen.SetActive(true);
            gameOverScreen.SetActive(false);
        }
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public void GoToMainMenu()
        {
            SceneManager.LoadScene(mainMenuSceneIndex);
        }
    }
}
