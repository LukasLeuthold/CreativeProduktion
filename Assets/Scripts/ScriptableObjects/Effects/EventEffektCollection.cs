using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "new EventEffectCollection", menuName = "Effect/EventEffectCollection")]
    public class EventEffektCollection : InitScriptObject
    {
        public List<BaseEventEffekt> eventEffects = new List<BaseEventEffekt>();

        public void ActivateEffects()
        {
            for (int i = 0; i < eventEffects.Count; i++)
            {
                eventEffects[i].ActivateEffect();
            }
        }

        public override void Initialize()
        {
            eventEffects.Clear();
        }
    }
}
