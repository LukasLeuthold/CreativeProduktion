using UnityEngine;
using UnityEngine.Audio;

namespace AutoDefense
{
    /// <summary>
    /// simple audio event listener; listens to event and plays audio clip when raised
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class AudioEventListener : MonoBehaviour
    {
        /// <summary>
        /// the event the listener is registering to
        /// </summary>
        [SerializeField] private AUDIOScriptableEvent Event;
        /// <summary>
        /// audiosource the clip gets played on
        /// </summary>
        [SerializeField] AudioSource source;

        /// <summary>
        /// plays the clip accordingly
        /// </summary>
        public void OnEventRaised(AudioClip _clip, AudioPlayMode _mode,float _volume, AudioMixerGroup _mixer)
        {
            if (source.isPlaying)
            {
                source.Stop();
            }
            source.volume = _volume;
            source.outputAudioMixerGroup = _mixer;

            switch (_mode)
            {
                case AudioPlayMode.ONESHOT:
                    source.PlayOneShot(_clip);
                    break;
            }
        }
        /// <summary>
        /// on enable registers to event
        /// </summary>
        private void OnEnable()
        {
            if (Event == null )
            {
                return;
            }
            Event.RegisterListener(this);
            if (source == null)
            {
                source = GetComponent<AudioSource>();
            }
        }
        /// <summary>
        /// on disable unregisters from event
        /// </summary>
        private void OnDisable()
        {
            if (Event == null)
            {
                return;
            }
            Event.UnregisterListener(this);
        }
    }
}
