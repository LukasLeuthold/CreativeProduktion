using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "new ThreatLevel", menuName = "Enemy/ThreatLevel")]
    public class ThreatLevel : InitScriptObject
    {
        [SerializeField]
        private int pointCost;
        [SerializeField]
        private int playerDamage;

        [SerializeField]
        private EnemyData[] enemiesInThreatLevel;

        public int PlayerDamage { get => playerDamage; }
        public int PointCost { get => pointCost; }

        /// <summary>
        /// returns a random enemyData of the ThreatLevel
        /// </summary>
        /// <returns>Random EnemyData</returns>
        public EnemyData GetRandomEnemy()
        {
            return enemiesInThreatLevel[UtilRandom.GetRandomIntFromRange(0, enemiesInThreatLevel.Length)].GetCopy();
        }
        public EnemyData GetEnemyAt(int _index)
        {
            return enemiesInThreatLevel[_index].GetCopy();
        }

        public override void Initialize()
        {
            for (int i = 0; i < enemiesInThreatLevel.Length; i++)
            {
                enemiesInThreatLevel[i].EnemyThreatLevel = this;
            }
        }
    }
}
