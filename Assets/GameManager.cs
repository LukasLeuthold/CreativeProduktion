using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject levelGO;
        [SerializeField] private GameObject gameOverGO;

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
    }
}
