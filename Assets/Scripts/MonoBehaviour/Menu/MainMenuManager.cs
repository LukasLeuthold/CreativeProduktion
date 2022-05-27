using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AutoDefense
{
    /// <summary>
    /// class to handle main menu logic
    /// </summary>
    public class MainMenuManager : MonoBehaviour
    {
        /// <summary>
        /// main menu screen gameobject
        /// </summary>
        [SerializeField] private GameObject mainMenuScreen;
        /// <summary>
        /// leave screen gameobject
        /// </summary>
        [SerializeField] private GameObject leaveScreen;
        /// <summary>
        /// index of the level scene
        /// </summary>
        [SerializeField] private int levelSceneIndex = 1;

        /// <summary>
        /// audio source the menu music is played on
        /// </summary>
        [Header("Audio")]
        [SerializeField] private AudioSource _mainMenuMusicSource;
        /// <summary>
        /// intro clip to the menu music 
        /// </summary>
        [SerializeField] private AudioClip _mainMenuMusicIntro;
        /// <summary>
        /// loop clip of the menu music
        /// </summary>
        [SerializeField] private AudioClip _mainMenuMusicLoop;

        /// <summary>
        /// sets default values
        /// </summary>
        void Start()
        {
            OpenMainMenuScreen();
            _mainMenuMusicSource.clip = _mainMenuMusicIntro;
            _mainMenuMusicSource.Play();
        }
        /// <summary>
        /// handles changing from intro to loop
        /// </summary>
        private void Update()
        {
            if (!_mainMenuMusicSource.isPlaying)
            {
                _mainMenuMusicSource.clip = _mainMenuMusicLoop;
                _mainMenuMusicSource.loop = true;
                _mainMenuMusicSource.Play();
            }
        }
        /// <summary>
        /// changes to the levelscene
        /// </summary>
        public void Play()
        {
            SceneManager.LoadScene(levelSceneIndex);
        }
        /// <summary>
        /// opens the leavescreen
        /// </summary>
        public void OpenLeaveScreen()
        {
            mainMenuScreen.SetActive(false);
            leaveScreen.SetActive(true);
        }
        /// <summary>
        /// opens the main menu screen
        /// </summary>
        public void OpenMainMenuScreen()
        {
            mainMenuScreen.SetActive(true);
            leaveScreen.SetActive(false);
        }
        /// <summary>
        /// handles leave screen input
        /// </summary>
        /// <param name="_isQuitting"></param>
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
