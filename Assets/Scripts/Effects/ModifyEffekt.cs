using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class ModifyEffekt : Effect
    {
        public readonly ModifierBlock buffStats;

        public override void ApplyEffect(HeroData _hero)
        {
            _hero.CurrStatModifier = _hero.CurrStatModifier + buffStats;
        }

        public override void RemoveEffect(HeroData _hero)
        {
            _hero.CurrStatModifier = _hero.CurrStatModifier - buffStats;

        }
    }
}
