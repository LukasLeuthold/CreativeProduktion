using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// SO storing information about the player ressources like gold,xp,level,etc.
    /// </summary>
    [CreateAssetMenu(fileName = "new PlayerRessources", menuName = "Player/PlayerRessources")]
    public class PlayerRessources : InitScriptObject
    {
        /// <summary>
        /// starting life total of the player
        /// </summary>
        [Header("Player-Health")]
        [SerializeField]private int startPlayerHealth;
        /// <summary>
        /// current player health
        /// </summary>
        private int playerHealth;
        /// <summary>
        /// starting gold amount
        /// </summary>
        [Header("Player-Money")]
        [SerializeField]private int startPlayerMoney;
        /// <summary>
        /// value the interest is based on player gets 1 interest per amount
        /// </summary>
        [SerializeField,Min(1)] private int interestCalculationValue = 1;
        /// <summary>
        /// maximum possible interest amount
        /// </summary>
        [SerializeField] private int maxPossibleInterest;
        /// <summary>
        /// current player money
        /// </summary>
        private int playerMoney;

        /// <summary>
        /// array of how much xp is needed to level up
        /// </summary>
        [Header("Player-XP")]
        [SerializeField] private int[] xPNeededForLevelUp;
        /// <summary>
        /// probability distribution array; is choosen by curr level
        /// </summary>
        [SerializeField] private ProbabilityDistribution[] probabilities;
        /// <summary>
        /// amount of xp the player has currently
        /// </summary>
        private int currXP;
        /// <summary>
        /// current level of the player
        /// </summary>
        private int currLevel;
        /// <summary>
        /// maximum level the player can reach
        /// </summary>
        private int maxLevel;

        /// <summary>
        /// gets called when player money changes
        /// </summary>
        [Header("Events")]
        [SerializeField]private INTScriptableEvent OnPlayerMoneyChanged;
        /// <summary>
        /// gets called when player level changes
        /// </summary>
        [SerializeField] private INTScriptableEvent OnPlayerLevelChanged;
        /// <summary>
        /// gets called when the currently used probability changes
        /// </summary>
        [SerializeField]private PROBScriptableEvent OnProbabilityChanged;
        /// <summary>
        /// gets called when the amount of xp the player has changes
        /// </summary>
        [SerializeField]private INT2ScriptableEvent OnPlayerXpChanged;
        /// <summary>
        /// gets called when the player hp changes
        /// </summary>
        [SerializeField] private INTScriptableEvent OnPlayerHpChanged;
        /// <summary>
        /// gets called when player hp hits 0
        /// </summary>
        [SerializeField]private BOOLScriptableEvent OnGameOver;

        /// <summary>
        /// gets called when player levels up
        /// </summary>
        [Header("Audio")]
        [SerializeField] private AUDIOScriptableEvent OnPlayerLevelUp;
        /// <summary>
        /// gets called when player looses life
        /// </summary>
        [SerializeField]private AUDIOScriptableEvent OnPlayerLifeLost;


        /// <summary>
        /// current player money
        /// </summary>
        public int PlayerMoney 
        {
            get => playerMoney;
            set
            {
                playerMoney = value;
                OnPlayerMoneyChanged?.Raise(playerMoney);
            }
        }
        /// <summary>
        /// the currently used probability based on current level
        /// </summary>
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
        /// <summary>
        /// current player health
        /// </summary>
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
                OnPlayerLifeLost?.Raise();
                OnPlayerHpChanged?.Raise(playerHealth);
            }
        }
        /// <summary>
        /// calculates the currently available amount of interest
        /// </summary>
        public int Interest
        {
            get
            {
                int interest = playerMoney / interestCalculationValue;
                if (interest<=maxPossibleInterest)
                {
                    return interest;
                }
                else
                {
                    return maxPossibleInterest;
                }
            }
        }
        /// <summary>
        /// maximum level the player can reach
        /// </summary>
        public int MaxLevel
        {
            get =>maxLevel;
            set =>maxLevel = value;
        }
        /// <summary>
        /// amount of xp the player has currently
        /// </summary>
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
                    OnPlayerLevelUp?.Raise();
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
        /// <summary>
        /// sets up default values
        /// </summary>
        public override void Initialize()
        {
            PlayerMoney = startPlayerMoney;
            currLevel = 1;
            CurrXP = 0;
            maxLevel = xPNeededForLevelUp.Length+1;
            OnProbabilityChanged?.Raise(probabilities[currLevel - 1]);
            playerHealth = startPlayerHealth;
            OnPlayerHpChanged?.Raise(playerHealth);
        }
    }
}
