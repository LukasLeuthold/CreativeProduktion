using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "new PlayerRessources", menuName = "ScriptablePlayerRessources/PlayerRessources")]

    public class PlayerRessources : InitScriptObject
    {
        //scriptable variable
        private int playerHealth;
        private int playerMoney;
        [SerializeField]private int startPlayerMoney;
        private int PlayerLevel;

        [SerializeField]private INTScriptableEvent OnPlayerMoneyChanged;
        [SerializeField]private INTScriptableEvent OnPlayerLevelChanged;

        private int currXP;

        private int currLevel;
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

        public int CurrXP
        {
            get => currXP;
            set
            {
                if (currXP + value < xPNeededForLevelUp[(currLevel - 1)])
                {
                    currXP += value;
                }
                else
                {
                    int difference = xPNeededForLevelUp[(currLevel - 1)] - currXP;
                    currXP = value - difference;
                    currLevel++;
                    OnPlayerLevelChanged.Raise(currLevel);
                }
            }
        }

        public override void Initialize()
        {
            PlayerMoney = startPlayerMoney;
            currLevel = 1;
        }
    }
}
