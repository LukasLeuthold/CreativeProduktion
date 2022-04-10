using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// simple int event object
    /// </summary>
    [CreateAssetMenu(fileName = "newIntEvent", menuName = "ScriptableEvents/INTEvent", order = 2)]
    public class INTScriptableEvent : ScriptableObject
    {
        /// <summary>
        /// list of registered listeners
        /// </summary>
        private List<INTEventListener> listeners = new List<INTEventListener>();
        /// <summary>
        /// value to manualy raise the event with
        /// </summary>
        [SerializeField]private int manualRaiseValue;

        /// <summary>
        /// Raises the event notifying all the listeners
        /// </summary>
        public void Raise(int _value)
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
        public void RegisterListener(INTEventListener _listener)
        {
            listeners.Add(_listener);
        }
        /// <summary>
        /// unregisters the listener from the event
        /// </summary>
        /// <param name="_listener">listener to unregister</param>
        public void UnregisterListener(INTEventListener _listener)
        {
            if (listeners.Contains(_listener))
            {
                listeners.Remove(_listener);
            }
        }
    }
}
