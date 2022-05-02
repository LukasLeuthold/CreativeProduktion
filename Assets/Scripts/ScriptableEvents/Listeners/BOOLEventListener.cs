using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AutoDefense
{
    /// <summary>
    /// simple event listener to bool events
    /// </summary>
    public class BOOLEventListener : MonoBehaviour
    {
        /// <summary>
        /// the event the listener is registering to
        /// </summary>
        [SerializeField] private BOOLScriptableEvent Event;
        /// <summary>
        /// the unity event to register the response to raising the event
        /// </summary>
        public UnityEvent<bool> response;

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
        public void OnEventRaised(bool _value)
        {
            response?.Invoke(_value);
        }
    }
}
