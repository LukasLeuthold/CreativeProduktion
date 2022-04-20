using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// simple int,int event object
    /// </summary>
    [CreateAssetMenu(fileName = "newInt2Event", menuName = "ScriptableEvents/INT2Event")]
    public class INT2ScriptableEvent : ScriptableObject
    {
        /// <summary>
        /// list of registered listeners
        /// </summary>
        private List<INT2EventListener> listeners = new List<INT2EventListener>();
        /// <summary>
        /// value to manualy raise the event with
        /// </summary>
        [SerializeField] private int manualRaiseValueA;
        /// <summary>
        /// value to manualy raise the event with
        /// </summary>
        [SerializeField] private int manualRaiseValueB;

        /// <summary>
        /// Raises the event notifying all the listeners
        /// </summary>
        public void Raise(int _valueA,int _valueB)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised(_valueA,_valueB);
            }
        }
        /// <summary>
        /// Raises the event manualy using the manual raise value as the value
        /// </summary>
        public void RaiseManualy()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised(manualRaiseValueA,manualRaiseValueB);
            }
        }
        /// <summary>
        /// registers an event listener to the event
        /// </summary>
        /// <param name="_listener">listener to register</param>
        public void RegisterListener(INT2EventListener _listener)
        {
            listeners.Add(_listener);
        }
        /// <summary>
        /// unregisters the listener from the event
        /// </summary>
        /// <param name="_listener">listener to unregister</param>
        public void UnregisterListener(INT2EventListener _listener)
        {
            if (listeners.Contains(_listener))
            {
                listeners.Remove(_listener);
            }
        }
    }
}

