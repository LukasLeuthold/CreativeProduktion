using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// handles game state logic
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// gameobject of the active level
        /// </summary>
        [SerializeField] private GameObject levelGO;
        /// <summary>
        /// game object of the gameover screen
        /// </summary>
        [SerializeField] private GameObject gameOverGO;
        /// <summary>
        /// ressources of the player
        /// </summary>
        [SerializeField] private PlayerRessources playerRessources;
        /// <summary>
        /// info object of the level
        /// </summary>
        [SerializeField] private LevelInfo levelInfo;

        /// <summary>
        /// sets default values
        /// </summary>
        void Start()
        {
            levelGO.SetActive(true);
            gameOverGO.SetActive(false);
        }
        /// <summary>
        /// sets gameover state either with win or loose
        /// </summary>
        /// <param name="_isWinner">flag if game is lost or won</param>
        public void GameOver(bool _isWinner)
        {
            Time.timeScale = 0;
            levelGO.SetActive(false);
            gameOverGO.SetActive(true);
            gameOverGO.GetComponent<GameOverMenuManager>().SetGameOverScreen(_isWinner);
        }
        /// <summary>
        /// rewards the player after each wave
        /// </summary>
        /// <param name="_waveNumber">current wavecount</param>
        public void RewardPlayer(int _waveNumber)
        {
            playerRessources.PlayerMoney += levelInfo.GoldPerWave + playerRessources.Interest;
            playerRessources.CurrXP += levelInfo.XpPerWave;
        }
    }
}
