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

        [Header("Audio")]
        [SerializeField] private AudioSource _mainMenuMusicSource;
        [SerializeField] private AudioClip _mainMenuMusicIntro;
        [SerializeField] private AudioClip _mainMenuMusicLoop;


        void Start()
        {
            OpenMainMenuScreen();
            _mainMenuMusicSource.clip = _mainMenuMusicIntro;
            _mainMenuMusicSource.Play();
        }
        private void Update()
        {
            if (!_mainMenuMusicSource.isPlaying)
            {
                _mainMenuMusicSource.clip = _mainMenuMusicLoop;
                _mainMenuMusicSource.loop = true;
                _mainMenuMusicSource.Play();
            }
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
        public void HandleLeaveScreenInput(bool _isQuitting)
        {
            if (_isQuitting)
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
