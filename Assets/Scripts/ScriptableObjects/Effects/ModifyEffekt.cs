using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// effect that changes the modifier of a hero
    /// </summary>
    [CreateAssetMenu(fileName = "new ModifyEffect", menuName = "Effect/ModifyEffect")]
    public class ModifyEffekt : Effect
    {
        /// <summary>
        /// modifier block that gets put on the herodata 
        /// </summary>
        [SerializeField]private ModifierBlock buffStats;

        /// <summary>
        /// apllies the effect to the herodata
        /// </summary>
        /// <param name="_hero">herodata to apply effect to</param>
        public override void ApplyEffect(HeroData _hero)
        {
            _hero.CurrStatModifier = _hero.CurrStatModifier + buffStats;
        }
        /// <summary>
        /// removes the effect from the herodata
        /// </summary>
        /// <param name="_hero">herodata to remove effect from</param>
        public override void RemoveEffect(HeroData _hero)
        {
            _hero.CurrStatModifier = _hero.CurrStatModifier - buffStats;
        }
        /// <summary>
        /// apllies the effect to hero collection
        /// </summary>
        /// <param name="_collection">collection to apply effect to</param>
        public override void ApplyEffectToGroup(HeroCollection _collection)
        {
            for (int i = 0; i < _collection.HeroesInCollection.Count; i++)
            {
                List<HeroData> heroes = _collection.HeroesInCollection.ElementAt(i).Value;
                for (int j = 0; j < heroes.Count; j++)
                {
                    this.ApplyEffect(heroes[j]);
                }
            }
            _collection.OnAddedToCollection += this.ApplyEffect;
            _collection.OnRemovedFromCollection += this.RemoveEffect;
        }
        /// <summary>
        /// removes the effect from the hero collection
        /// </summary>
        /// <param name="_collection">collection to remove effect from</param>
        public override void RemoveEffectFromGroup(HeroCollection _collection)
        {
            for (int i = 0; i < _collection.HeroesInCollection.Count; i++)
            {
                List<HeroData> heroes = _collection.HeroesInCollection.ElementAt(i).Value;
                for (int j = 0; j < heroes.Count; j++)
                {
                    this.RemoveEffect(heroes[j]);
                }
            }
            _collection.OnAddedToCollection -= this.ApplyEffect;
            _collection.OnRemovedFromCollection -= this.RemoveEffect;
        }

        /// <summary>
        /// does nothing at the moment
        /// </summary>
        public override void Initialize()
        {
        }
    }
}
