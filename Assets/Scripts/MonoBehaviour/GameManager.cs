using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject levelGO;
        [SerializeField] private GameObject gameOverGO;

        [SerializeField] private PlayerRessources playerRessources;
        [SerializeField] private LevelInfo levelInfo;

        // Start is called before the first frame update
        void Start()
        {
            levelGO.SetActive(true);
            gameOverGO.SetActive(false);
        }

        public void GameOver(bool _isWinner)
        {
            Time.timeScale = 0;
            levelGO.SetActive(false);
            gameOverGO.SetActive(true);
            gameOverGO.GetComponent<GameOverMenuManager>().SetGameOverScreen(_isWinner);
        }

        public void RewardPlayer(int _waveNumber)
        {
            playerRessources.PlayerMoney += levelInfo.GoldPerWave + playerRessources.Interest;
            playerRessources.CurrXP += levelInfo.XpPerWave;
        }
    }
}
