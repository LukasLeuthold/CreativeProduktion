using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// collection of event effects that can get called on specific times
    /// </summary>
    [CreateAssetMenu(fileName = "new EventEffectCollection", menuName = "Effect/EventEffectCollection")]
    public class EventEffektCollection : InitScriptObject
    {
        /// <summary>
        /// list of effects in the collection
        /// </summary>
        public List<BaseEventEffekt> eventEffects = new List<BaseEventEffekt>();

        /// <summary>
        /// activates the effects
        /// </summary>
        public void ActivateEffects()
        {
            for (int i = 0; i < eventEffects.Count; i++)
            {
                eventEffects[i].ActivateEffect();
            }
        }

        /// <summary>
        /// clears list of subscribed effects
        /// </summary>
        public override void Initialize()
        {
            eventEffects.Clear();
        }
    }
}
