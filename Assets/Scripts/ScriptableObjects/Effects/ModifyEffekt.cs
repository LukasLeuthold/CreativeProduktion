using System.Collections;
using System.Collections.Generic;
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
        public override void Initialize()
        {
        }
    }
}
