using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// effect that targets a specific collection
    /// </summary>
    [CreateAssetMenu(fileName = "new TargetedModifyEffect", menuName = "Effect/TargetedModifyEffect")]
    public class TargetedModifyEffect : Effect
    {
        /// <summary>
        /// modifierblock of the effect
        /// </summary>
        [SerializeField] private ModifierBlock buffStats;
        /// <summary>
        /// colleciton that gets targeted by the effect
        /// </summary>
        [SerializeField] private HeroCollection targetCollection;

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
        /// apllies the effect to target hero collection
        /// </summary>
        /// <param name="_collection"></param>
        public override void ApplyEffectToGroup(HeroCollection _collection)
        {
            for (int i = 0; i < targetCollection.HeroesInCollection.Count; i++)
            {
                List<HeroData> heroes = targetCollection.HeroesInCollection.ElementAt(i).Value;
                for (int j = 0; j < heroes.Count; j++)
                {
                    this.ApplyEffect(heroes[j]);
                }
            }
            targetCollection.OnAddedToCollection += this.ApplyEffect;
            targetCollection.OnRemovedFromCollection += this.RemoveEffect;
        }
        /// <summary>
        /// removes the effect from the target hero collection
        /// </summary>
        /// <param name="_collection"></param>
        public override void RemoveEffectFromGroup(HeroCollection _collection)
        {
            for (int i = 0; i < targetCollection.HeroesInCollection.Count; i++)
            {
                List<HeroData> heroes = targetCollection.HeroesInCollection.ElementAt(i).Value;
                for (int j = 0; j < heroes.Count; j++)
                {
                    this.RemoveEffect(heroes[j]);
                }
            }
            targetCollection.OnAddedToCollection -= this.ApplyEffect;
            targetCollection.OnRemovedFromCollection -= this.RemoveEffect;
        }
        /// <summary>
        /// does nothing at the moment 
        /// </summary>
        public override void Initialize()
        {
        }
    }
}

