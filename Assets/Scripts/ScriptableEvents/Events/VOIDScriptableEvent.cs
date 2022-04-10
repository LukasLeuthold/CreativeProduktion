using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// simple void event object
    /// </summary>
    [CreateAssetMenu(fileName = "newVoidEvent", menuName = "ScriptableEvents/VOIDEvent", order = 1)]
    public class VOIDScriptableEvent : ScriptableObject
    {
        /// <summary>
        /// list of registered listeners
        /// </summary>
        private List<VOIDEventListener> listeners = new List<VOIDEventListener>();
        /// <summary>
        /// Raises the event notifying all the listeners
        /// </summary>
        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised();
            }
        }
        /// <summary>
        /// registers an event listener to the event
        /// </summary>
        /// <param name="_listener">listener to register</param>
        public void RegisterListener(VOIDEventListener _listener)
        {
            listeners.Add(_listener);
        }
        /// <summary>
        /// unregisters the listener from the event
        /// </summary>
        /// <param name="_listener">listener to unregister</param>
        public void UnregisterListener(VOIDEventListener _listener)
        {
            if (listeners.Contains(_listener))
            {
                listeners.Remove(_listener);
            }
        }
    }
}
