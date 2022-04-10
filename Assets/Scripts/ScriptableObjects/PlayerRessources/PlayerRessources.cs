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
        private int playerMoney;
        private int PlayerLevel;

        private int currXP;
        [SerializeField] private int[] xPNeededForLevelUp;
        [SerializeField] private ProbabilityDistribution[] probabilities;


    }
}
