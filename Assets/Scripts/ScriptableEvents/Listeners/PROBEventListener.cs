using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AutoDefense
{
    public class PROBEventListener : MonoBehaviour
    {
        /// <summary>
        /// the event the listener is registering to
        /// </summary>
        [SerializeField] private PROBScriptableEvent Event;
        /// <summary>
        /// the unity event to register the response to raising the event
        /// </summary>
        public UnityEvent<ProbabilityDistribution> response;

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
        public void OnEventRaised(ProbabilityDistribution _value)
        {
            response?.Invoke(_value);
        }
    }
}
