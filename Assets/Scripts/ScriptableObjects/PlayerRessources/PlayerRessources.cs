using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "new PlayerRessources", menuName = "ScriptablePlayerRessources/PlayerRessources")]

    public class PlayerRessources : ScriptableObject
    {
        //scriptable variable
        private int playerHealth;
        [SerializeField]private int playerMoney;
        private int PlayerLevel;

        [SerializeField]private INTScriptableEvent OnPlayerMoneyChanged;

        private int currXP;
        [SerializeField] private int[] xPNeededForLevelUp;
        [SerializeField] private ProbabilityDistribution[] probabilities;

        public int PlayerMoney 
        {
            get => playerMoney;
            set
            {
                playerMoney = value;
                OnPlayerMoneyChanged?.Raise(playerMoney);
            }
        }
    }
}
