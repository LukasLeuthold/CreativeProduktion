using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "new PlayerRessources", menuName = "ScriptablePlayerRessources/PlayerRessources")]

    public class PlayerRessources : InitScriptObject
    {
        [Header("Player-Health")]
        [SerializeField]private int startPlayerHealth;
        private int playerHealth;

        [Header("Player-Money")]
        [SerializeField]private int startPlayerMoney;
        private int playerMoney;

        [Header("Player-XP")]
        [SerializeField] private int[] xPNeededForLevelUp;
        [SerializeField] private ProbabilityDistribution[] probabilities;
        private int currXP;
        private int currLevel;
        private int maxLevel;

        [Header("Events")]
        [SerializeField]private INTScriptableEvent OnPlayerMoneyChanged;
        [SerializeField]private INTScriptableEvent OnPlayerLevelChanged;
        [SerializeField]private PROBScriptableEvent OnProbabilityChanged;
        [SerializeField]private INT2ScriptableEvent OnPlayerXpChanged;
        [SerializeField]private INTScriptableEvent OnPlayerHpChanged;
        [SerializeField]private BOOLScriptableEvent OnGameOver;
        public int PlayerMoney 
        {
            get => playerMoney;
            set
            {
                playerMoney = value;
                OnPlayerMoneyChanged?.Raise(playerMoney);
            }
        }
        public ProbabilityDistribution CurrProbability 
        {
            get
            {
                if (probabilities[currLevel - 1] != null)
                {
                    return probabilities[currLevel - 1];
                }
                else return new ProbabilityDistribution();
            }
        }
        public int PlayerHealth
        {
            get => playerHealth;
            set
            {
                playerHealth = value;
                if (playerHealth <=0)
                {
                    playerHealth = 0;
                    OnPlayerHpChanged?.Raise(playerHealth);
                    OnGameOver?.Raise(false);
                    return;
                }
                OnPlayerHpChanged?.Raise(playerHealth);
            }
        }


        public int CurrXP
        {
            get => currXP;
            set
            {
                if (currLevel == maxLevel)
                {
                    return;
                }
                if (value < xPNeededForLevelUp[(currLevel - 1)])
                {
                    currXP = value;
                    OnPlayerXpChanged?.Raise(currXP,xPNeededForLevelUp[currLevel-1]);
                }
                else
                {
                    currXP = value - xPNeededForLevelUp[(currLevel - 1)];
                    currLevel++;
                    if (currLevel == maxLevel)
                    {
                        currXP = xPNeededForLevelUp[currLevel - 2];
                        OnPlayerXpChanged?.Raise(currXP, xPNeededForLevelUp[currLevel - 2]);
                    }
                    else
                    {
                        OnPlayerXpChanged?.Raise(currXP, xPNeededForLevelUp[currLevel - 1]);
                    }
                    OnPlayerLevelChanged?.Raise(currLevel);
                    OnProbabilityChanged?.Raise(probabilities[currLevel - 1]);
                }

            }
        }

        public override void Initialize()
        {
            PlayerMoney = startPlayerMoney;
            currLevel = 1;
            CurrXP = 0;
            maxLevel = xPNeededForLevelUp.Length+1;
            OnProbabilityChanged?.Raise(probabilities[currLevel - 1]);
            PlayerHealth = startPlayerHealth;
        }

        public void AddXp()
        {
            CurrXP += 10;
        }
    }
}
