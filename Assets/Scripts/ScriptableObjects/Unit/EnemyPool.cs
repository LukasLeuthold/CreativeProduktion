using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "new EnemyPool", menuName = "ScriptableUnitPool/EnemyPool", order = 2)]
    public class EnemyPool : ScriptableObject
    {
        [SerializeField]
        private ThreatLevel[] enemiesInPool = new ThreatLevel[1];
        [SerializeField]
        private EnemyData bossEnemy;
        public ThreatLevel[] EnemiesInPool { get => enemiesInPool; private set => enemiesInPool = value; }

    }

    [System.Serializable]
    public struct ThreatLevel
    {
        [SerializeField]
        private string name;

        [SerializeField]
        private int pointCost;

        [SerializeField]
        private EnemyData[] enemiesInThreatLevel;

        public int PointCost { get => pointCost; }

        /// <summary>
        /// returns a random enemyData of the ThreatLevel
        /// </summary>
        /// <returns>Random EnemyData</returns>
        public EnemyData GetRandomEnemy()
        {
            return enemiesInThreatLevel[UtilRandom.GetRandomIntFromRange(0, enemiesInThreatLevel.Length)].GetCopy();
        }
    }
}
