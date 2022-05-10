using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "new MoneyForHealthEffect", menuName = "Effect/EventEffect/MoneyForHealthEffect")]
    public class MoneyForHealthEffect : BaseEventEffekt
    {
        [SerializeField] private PlayerRessources playerRessources;
        [SerializeField] private int healthCost;
        [SerializeField] private int moneyReward;

        public override void ActivateEffect()
        {
            Debug.Log("activated: " + this.name);
            playerRessources.PlayerHealth -= healthCost;
            playerRessources.PlayerMoney += moneyReward;
        }
    }
}
