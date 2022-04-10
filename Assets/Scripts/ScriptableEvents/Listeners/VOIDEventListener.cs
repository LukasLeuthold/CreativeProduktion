using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AutoDefense
{
    /// <summary>
    /// simple event listener to void events
    /// </summary>
    public class VOIDEventListener : MonoBehaviour
    {
        /// <summary>
        /// the event the listener is registering to
        /// </summary>
        [SerializeField]private  VOIDScriptableEvent Event;
        /// <summary>
        /// the unity event to register the response to raising the event
        /// </summary>
        public UnityEvent response;

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
        public void OnEventRaised()
        {
            response?.Invoke();
        }
    }
}
