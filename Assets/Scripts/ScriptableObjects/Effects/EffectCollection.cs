using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "new Effect Collection", menuName = "Effect/EffectCollection")]
    public class EffectCollection : InitScriptObject
    {
        private Dictionary<HeroCollection, EffectInfo> effectDictionary;
        [SerializeField] private EffectInfo[] groupEffects;

        public override void Initialize()
        {
            effectDictionary = new Dictionary<HeroCollection, EffectInfo>();
            for (int i = 0; i < groupEffects.Length; i++)
            {
                effectDictionary.Add(groupEffects[i].sourceCollection, groupEffects[i]);
            }
        }
    }

    public class EffectInfo
    {
        public HeroCollection sourceCollection;
        public HeroCollection targetCollection;
        public Effect effect;
    }
}
