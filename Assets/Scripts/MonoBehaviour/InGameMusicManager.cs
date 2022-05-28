using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// manages ingame music logic
    /// </summary>
    public class InGameMusicManager : MonoBehaviour
    {
        /// <summary>
        /// audiosource for the intro part
        /// </summary>
        [SerializeField] private AudioSource introSource;
        /// <summary>
        /// audiosource for the fast part
        /// </summary>
        [SerializeField] private AudioSource fastSource;
        /// <summary>
        /// audiosource for the slow part
        /// </summary>
        [SerializeField] private AudioSource slowSource;
        /// <summary>
        /// speed in which the sources get blended
        /// </summary>
        [SerializeField] private float blendSpeed;

        /// <summary>
        /// flag if it is edit time or not 
        /// </summary>
        public bool isEditTime;
        /// <summary>
        /// flag if it is in the intro part or not
        /// </summary>
        bool isIntro;
        /// <summary>
        /// starts playing intro part and sets default values
        /// </summary>
        private void Start()
        {
            introSource.Play();
            introSource.volume = 0.2f;
            isIntro = true;
            fastSource.volume = 0;
            slowSource.volume = 1;
            fastSource.loop = true;
            slowSource.loop = true;
        }
        /// <summary>
        /// blends clips when gamestate changes
        /// </summary>
        private void Update()
        {
            if (isIntro )
            {
                if (!introSource.isPlaying)
                {
                    fastSource.Play();
                    slowSource.Play();
                    isIntro = false;
                }
                else
                {
                    introSource.volume += blendSpeed;
                }
            }
            if (isEditTime)
            {
                fastSource.volume -= blendSpeed;
                slowSource.volume += blendSpeed;
            }
            else
            {
                fastSource.volume += blendSpeed;
                slowSource.volume -= blendSpeed;
            }
        }
        /// <summary>
        /// sets isedittime to true
        /// </summary>
        public void ActivateEditTime()
        {
            isEditTime = true;
        }
        /// <summary>
        /// sets isedittime to false
        /// </summary>
        public void DeactivateEditTime()
        {
            isEditTime = false;
        }
    }
}
