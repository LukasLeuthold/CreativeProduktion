using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "new ModifyEffect", menuName = "Effect/ModifyEffect")]
    public class ModifyEffekt : Effect
    {
        [SerializeField]private ModifierBlock buffStats;

        public override void ApplyEffect(HeroData _hero)
        {
            _hero.CurrStatModifier = _hero.CurrStatModifier + buffStats;
        }


        public override void RemoveEffect(HeroData _hero)
        {
            _hero.CurrStatModifier = _hero.CurrStatModifier - buffStats;
        }

        private void ApplyEffectToGroup(HeroCollection _collection)
        {
            for (int i = 0; i < _collection.HeroesInCollection.Count; i++)
            {
                List<HeroData> heroes = _collection.HeroesInCollection.ElementAt(i).Value;
                for (int j = 0; j < heroes.Count; j++)
                {
                    this.ApplyEffect(heroes[j]);
                }
            }
        }
        private void RemoveEffectFromGroup(HeroCollection _collection)
        {
            for (int i = 0; i < _collection.HeroesInCollection.Count; i++)
            {
                List<HeroData> heroes = _collection.HeroesInCollection.ElementAt(i).Value;
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
