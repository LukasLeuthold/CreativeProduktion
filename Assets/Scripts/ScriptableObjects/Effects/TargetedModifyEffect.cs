using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "new TargetedModifyEffect", menuName = "Effect/TargetedModifyEffect")]
    public class TargetedModifyEffect : Effect
    {
        [SerializeField] private ModifierBlock buffStats;
        [SerializeField] private HeroCollection targetCollection;

        public override void ApplyEffect(HeroData _hero)
        {
            _hero.CurrStatModifier = _hero.CurrStatModifier + buffStats;
        }
        public override void RemoveEffect(HeroData _hero)
        {
            _hero.CurrStatModifier = _hero.CurrStatModifier - buffStats;
        }

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
        public override void RemoveEffectFromGroup(HeroCollection _collection)
        {
            targetCollection.OnAddedToCollection -= this.ApplyEffect;
            targetCollection.OnRemovedFromCollection -= this.RemoveEffect;
            for (int i = 0; i < targetCollection.HeroesInCollection.Count; i++)
            {
                List<HeroData> heroes = targetCollection.HeroesInCollection.ElementAt(i).Value;
                for (int j = 0; j < heroes.Count; j++)
                {
                    this.RemoveEffect(heroes[j]);
                }
            }
        }
        public override void Initialize()
        {
        }
    }
}

