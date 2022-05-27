using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// collection of enemies with info about how much playerdamage they do and how much they cost
    /// </summary>
    [CreateAssetMenu(fileName = "new ThreatLevel", menuName = "Enemy/ThreatLevel")]
    public class ThreatLevel : InitScriptObject
    {
        /// <summary>
        /// cost of the threatlevel
        /// </summary>
        [SerializeField]
        private int pointCost;
        /// <summary>
        /// damage enemies of the threatlevel do to the player
        /// </summary>
        [SerializeField]
        private int playerDamage;

        /// <summary>
        /// all enemies in the threatlevel
        /// </summary>
        [SerializeField]
        private EnemyData[] enemiesInThreatLevel;

        /// <summary>
        /// damage enemies of the threatlevel do to the player
        /// </summary>
        public int PlayerDamage { get => playerDamage; }
        /// <summary>
        /// cost of the threatlevel
        /// </summary>
        public int PointCost { get => pointCost; }

        /// <summary>
        /// returns a random enemyData of the ThreatLevel
        /// </summary>
        /// <returns>Random EnemyData</returns>
        public EnemyData GetRandomEnemy()
        {
            return enemiesInThreatLevel[UtilRandom.GetRandomIntFromRange(0, enemiesInThreatLevel.Length)].GetCopy();
        }
        /// <summary>
        /// returns an enemyData at the index
        /// </summary>
        /// <param name="_index">index</param>
        /// <returns></returns>
        public EnemyData GetEnemyAt(int _index)
        {
            return enemiesInThreatLevel[_index].GetCopy();
        }
        /// <summary>
        /// initiializes enemies in threatlevel
        /// </summary>
        public override void Initialize()
        {
            for (int i = 0; i < enemiesInThreatLevel.Length; i++)
            {
                enemiesInThreatLevel[i].EnemyThreatLevel = this;
            }
        }
    }
}
