using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoDefense
{
    public abstract class BaseEventEffekt : Effect
    {
        public List<HeroData> subscribedHeroes = new List<HeroData>();
        [SerializeField] private EventEffektCollection collection;
        public override void ApplyEffect(HeroData _hero)
        {
            subscribedHeroes.Add(_hero);
        }

        public override void RemoveEffect(HeroData _hero)
        {
            subscribedHeroes.Remove(_hero);
        }

        public override void ApplyEffectToGroup(HeroCollection _collection)
        {
            for (int i = 0; i < _collection.HeroesInCollection.Keys.Count; i++)
            {
                string[] keys = _collection.HeroesInCollection.Keys.ToArray();
                List<HeroData> hData = _collection.HeroesInCollection[keys[i]];
                for (int j = 0; j < hData.Count; j++)
                {
                    subscribedHeroes.Add(hData[j]);
                }

            }
            collection.eventEffects.Add(this);
            _collection.OnAddedToCollection += this.ApplyEffect;
            _collection.OnRemovedFromCollection += this.RemoveEffect;
        }
        public override void RemoveEffectFromGroup(HeroCollection _collection)
        {
            subscribedHeroes.Clear();
            _collection.OnAddedToCollection -= this.ApplyEffect;
            _collection.OnRemovedFromCollection -= this.RemoveEffect;
            collection.eventEffects.Remove(this);
        }

        public abstract void ActivateEffect();

        public override void Initialize()
        {
            subscribedHeroes.Clear();
        }
    }
}
