using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// effect that heals for a given amount when triggered
    /// </summary>
    [CreateAssetMenu(fileName = "new HealEffect", menuName = "Effect/EventEffect/HealEffect")]
    public class HealEventEffect : BaseEventEffekt
    {
        /// <summary>
        /// amount the heroes get healed for
        /// </summary>
        [SerializeField]private int healAmount;

        /// <summary>
        /// activates the effect
        /// </summary>
        public override void ActivateEffect()
        {
            for (int i = 0; i < subscribedHeroes.Count; i++)
            {
                subscribedHeroes[i].Unit.CurrHP += healAmount;
            }
        }
    }
}
