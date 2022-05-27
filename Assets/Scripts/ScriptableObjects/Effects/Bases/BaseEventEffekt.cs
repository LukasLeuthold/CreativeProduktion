using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// base class for event effects
    /// </summary>
    public abstract class BaseEventEffekt : Effect
    {
        /// <summary>
        /// list of heroes subscribed to the effect
        /// </summary>
        public List<HeroData> subscribedHeroes = new List<HeroData>();
        /// <summary>
        /// colleciton the effect is part of
        /// </summary>
        [SerializeField] private EventEffektCollection collection;
        /// <summary>
        /// apllies the effect to the herodata
        /// </summary>
        /// <param name="_hero">herodata to apply effect to</param>
        public override void ApplyEffect(HeroData _hero)
        {
            subscribedHeroes.Add(_hero);
        }
        /// <summary>
        /// removes the effect from the herodata
        /// </summary>
        /// <param name="_hero">herodata to remove effect from</param>
        public override void RemoveEffect(HeroData _hero)
        {
            subscribedHeroes.Remove(_hero);
        }
        /// <summary>
        /// apllies the effect to hero collection
        /// </summary>
        /// <param name="_collection">collection to apply effect to</param>
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
        /// <summary>
        /// removes the effect from the hero collection
        /// </summary>
        /// <param name="_collection">collection to remove effect from</param>
        public override void RemoveEffectFromGroup(HeroCollection _collection)
        {
            subscribedHeroes.Clear();
            _collection.OnAddedToCollection -= this.ApplyEffect;
            _collection.OnRemovedFromCollection -= this.RemoveEffect;
            collection.eventEffects.Remove(this);
        }
        /// <summary>
        /// activates the effect
        /// </summary>
        public abstract void ActivateEffect();
        /// <summary>
        /// clears subscribed heroes
        /// </summary>
        public override void Initialize()
        {
            subscribedHeroes.Clear();
        }
    }
}
