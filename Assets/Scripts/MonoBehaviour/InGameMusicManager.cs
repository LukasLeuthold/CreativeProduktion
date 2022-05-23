using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class InGameMusicManager : MonoBehaviour
    {
        [SerializeField] private AudioSource introSource;
        [SerializeField] private AudioSource fastSource;
        [SerializeField] private AudioSource slowSource;
        [SerializeField] private float blendSpeed;


        public bool isEditTime;
        bool isIntro;

        private void Start()
        {
            introSource.Play();
            isIntro = true;
            fastSource.volume = 0;
            slowSource.volume = 1;
            fastSource.loop = true;
            slowSource.loop = true;
        }
        private void Update()
        {
            Debug.Log(introSource.isPlaying);
            Debug.Log(fastSource.isPlaying + " , " + fastSource.volume);
            Debug.Log(slowSource.isPlaying + " , " + slowSource.volume);
            if (isIntro &&!introSource.isPlaying)
            {
                fastSource.Play();
                slowSource.Play();
                isIntro = false;
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

        public void ActivateEditTime()
        {
            isEditTime = true;
        }
        public void DeactivateEditTime()
        {
            isEditTime = false;
        }
    }
}
