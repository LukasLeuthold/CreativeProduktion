using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// effect that gives money for player health when activated
    /// </summary>
    [CreateAssetMenu(fileName = "new MoneyForHealthEffect", menuName = "Effect/EventEffect/MoneyForHealthEffect")]
    public class MoneyForHealthEffect : BaseEventEffekt
    {
        /// <summary>
        /// ressources of the player
        /// </summary>
        [SerializeField] private PlayerRessources playerRessources;
        /// <summary>
        /// health cost to activate the effect
        /// </summary>
        [SerializeField] private int healthCost;
        /// <summary>
        /// money the player receives after activating the effect
        /// </summary>
        [SerializeField] private int moneyReward;

        /// <summary>
        /// activates the effect
        /// </summary>
        public override void ActivateEffect()
        {
            playerRessources.PlayerHealth -= healthCost;
            playerRessources.PlayerMoney += moneyReward;
        }
    }
}
