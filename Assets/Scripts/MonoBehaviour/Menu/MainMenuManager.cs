using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AutoDefense
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenuScreen;
        [SerializeField] private GameObject leaveScreen;
        [SerializeField] private int levelSceneIndex = 1;


        void Start()
        {
            OpenMainMenuScreen();
        }
        public void Play()
        {
            SceneManager.LoadScene(levelSceneIndex);
        }
        
        public void OpenLeaveScreen()
        {
            mainMenuScreen.SetActive(false);
            leaveScreen.SetActive(true);
        }
        public void OpenMainMenuScreen()
        {
            mainMenuScreen.SetActive(true);
            leaveScreen.SetActive(false);
        }
        public void HandleLeaveScreenInput(bool _value)
        {
            if (_value)
            {
                Debug.LogError("Quitting the Game");
                Application.Quit();
            }
            else
            {
                OpenMainMenuScreen();
            }
        }
    }
}
