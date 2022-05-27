using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AutoDefense
{
    /// <summary>
    /// simple event listener to int,int events
    /// </summary>
    public class INT2EventListener : MonoBehaviour
    {
        /// <summary>
        /// the event the listener is registering to
        /// </summary>
        [SerializeField] private INT2ScriptableEvent Event;
        /// <summary>
        /// the unity event to register the response to raising the event
        /// </summary>
        public UnityEvent<int,int> response;

        /// <summary>
        /// on enable registers to event
        /// </summary>
        private void OnEnable()
        {
            Event.RegisterListener(this);
        }
        /// <summary>
        /// on disable unregisters from event
        /// </summary>
        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }
        /// <summary>
        /// on event raised invokes the respone unityevent
        /// </summary>
        public void OnEventRaised(int _valueA,int _valueB)
        {
            response?.Invoke(_valueA,_valueB);
        }
    }
}
