using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AutoDefense
{
    /// <summary>
    /// class handling game over screen logic
    /// </summary>
    public class GameOverMenuManager : MonoBehaviour
    {
        /// <summary>
        /// game over screen go
        /// </summary>
        [SerializeField] private GameObject gameOverScreen;
        /// <summary>
        /// game over screen background image
        /// </summary>
        [SerializeField] private Image gameOverScreenImage;
        /// <summary>
        /// quit screen go
        /// </summary>
        [SerializeField] private GameObject quitScreen;
        /// <summary>
        /// index of the main menu scene
        /// </summary>
        [SerializeField] private int mainMenuSceneIndex;

        /// <summary>
        /// sprite for when the game is won
        /// </summary>
        [SerializeField] private Sprite winSprite;
        /// <summary>
        /// sprite for when the game is lost
        /// </summary>
        [SerializeField] private Sprite looseSprite;

        /// <summary>
        /// gets played when game is won
        /// </summary>
        [SerializeField] private AUDIOScriptableEvent GameOver_Won;
        /// <summary>
        /// gets played when game is lost
        /// </summary>
        [SerializeField] private AUDIOScriptableEvent GameOver_Lost;

        /// <summary>
        /// sets default values
        /// </summary>
        void Start()
        {
            quitScreen.SetActive(false);
        }
        /// <summary>
        /// sets game over screen for either winning or loosing
        /// </summary>
        /// <param name="_isWinner"></param>
        public void SetGameOverScreen(bool _isWinner)
        {
            if (_isWinner)
            {
                gameOverScreenImage.sprite = winSprite;
                 GameOver_Won?.Raise();
            }
            else
            {
                gameOverScreenImage.sprite = looseSprite;
                GameOver_Lost?.Raise();
            }
        }
        /// <summary>
        /// handles quit screen logic
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
                OpenGameOverScreen();
            }
        }
        /// <summary>
        /// opens game over screen
        /// </summary>
        public void OpenGameOverScreen()
        {
            quitScreen.SetActive(false);
            gameOverScreen.SetActive(true);
        }
        /// <summary>
        /// opens the quit screen
        /// </summary>
        public void OpenQuitScreen()
        {
            quitScreen.SetActive(true);
            gameOverScreen.SetActive(false);
        }
        /// <summary>
        /// restarts levelscene
        /// </summary>
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        /// <summary>
        /// goes to main menu scene
        /// </summary>
        public void GoToMainMenu()
        {
            SceneManager.LoadScene(mainMenuSceneIndex);
        }
    }
}
