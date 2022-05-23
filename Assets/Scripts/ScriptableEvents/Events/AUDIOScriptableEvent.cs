using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Audio;
using UnityEngine.Audio;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "newAudioEvent", menuName = "ScriptableEvents/AudioEvent")]
    public class AUDIOScriptableEvent : ScriptableObject
    {
        [SerializeField] AudioClip clip;
        [SerializeField,Range(0,1)] float volume;
        [SerializeField] AudioMixerGroup mixer;
        [SerializeField] AudioPlayMode mode = AudioPlayMode.ONESHOT;

        /// <summary>
        /// list of registered listeners
        /// </summary>
        private List<AudioEventListener> listeners = new List<AudioEventListener>();
        /// <summary>
        /// Raises the event notifying all the listeners
        /// </summary>
        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised(clip,mode,volume,mixer);
            }
        }
        /// <summary>
        /// registers an event listener to the event
        /// </summary>
        /// <param name="_listener">listener to register</param>
        public void RegisterListener(AudioEventListener _listener)
        {
            listeners.Add(_listener);
        }
        /// <summary>
        /// unregisters the listener from the event
        /// </summary>
        /// <param name="_listener">listener to unregister</param>
        public void UnregisterListener(AudioEventListener _listener)
        {
            if (listeners.Contains(_listener))
            {
                listeners.Remove(_listener);
            }
        }
    }

    public enum AudioPlayMode
    {
        ONESHOT,
        //LOOP,
    }
}
