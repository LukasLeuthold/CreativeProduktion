using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public abstract class BaseEventEffekt : Effect
    {
        public List<HeroData> subscribedHeroes = new List<HeroData>();
        [SerializeField]private EventEffektCollection collection;
        public override void ApplyEffect(HeroData _hero)
        {
            if (subscribedHeroes.Count == 0)
            {
                collection.eventEffects.Add(this);
            }
            subscribedHeroes.Add(_hero);
        }

        public override void RemoveEffect(HeroData _hero)
        {
            subscribedHeroes.Remove(_hero);
            if (subscribedHeroes.Count == 0)
            {
                collection.eventEffects.Remove(this);
            }
        }

        public abstract void ActivateEffect();

        public override void Initialize()
        {
            subscribedHeroes.Clear();
        }
    }
}
