using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// simple bool event object
    /// </summary>
    [CreateAssetMenu(fileName = "newBoolEvent", menuName = "ScriptableEvents/BoolEvent")]
    public class BOOLScriptableEvent : ScriptableObject
    {
        /// <summary>
        /// list of registered listeners
        /// </summary>
        private List<BOOLEventListener> listeners = new List<BOOLEventListener>();
        /// <summary>
        /// value to manualy raise the event with
        /// </summary>
        [SerializeField] private bool manualRaiseValue;

        /// <summary>
        /// Raises the event notifying all the listeners
        /// </summary>
        public void Raise(bool _value)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised(_value);
            }
        }
        /// <summary>
        /// Raises the event manualy using the manual raise value as the value
        /// </summary>
        public void RaiseManualy()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised(manualRaiseValue);
            }
        }
        /// <summary>
        /// registers an event listener to the event
        /// </summary>
        /// <param name="_listener">listener to register</param>
        public void RegisterListener(BOOLEventListener _listener)
        {
            listeners.Add(_listener);
        }
        /// <summary>
        /// unregisters the listener from the event
        /// </summary>
        /// <param name="_listener">listener to unregister</param>
        public void UnregisterListener(BOOLEventListener _listener)
        {
            if (listeners.Contains(_listener))
            {
                listeners.Remove(_listener);
            }
        }
    }
}
