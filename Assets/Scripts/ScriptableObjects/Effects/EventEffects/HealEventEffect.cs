using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "new HealEffect", menuName = "Effect/EventEffect/HealEffect")]
    public class HealEventEffect : BaseEventEffekt
    {
        [SerializeField]private int healAmount;

        public override void ActivateEffect()
        {
            for (int i = 0; i < subscribedHeroes.Count; i++)
            {
                subscribedHeroes[i].Unit.CurrHP += healAmount;
            }
        }
    }
}
