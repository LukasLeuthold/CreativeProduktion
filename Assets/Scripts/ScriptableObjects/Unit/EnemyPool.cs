using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    ///class to handle enemy pool behaviour
    /// </summary>
    [CreateAssetMenu(fileName = "new EnemyPool", menuName = "Enemy/EnemyPool")]
    public class EnemyPool : ScriptableObject
    {
        /// <summary>
        /// the threatlevels in the enemy pool
        /// </summary>
        [SerializeField]
        private ThreatLevel[] threatLevels = new ThreatLevel[1];
        /// <summary>
        /// the threatlevel of the boss
        /// </summary>
        [SerializeField]
        private ThreatLevel bossEnemy;
        /// <summary>
        /// the threatlevels in the enemy pool
        /// </summary>
        public ThreatLevel[] ThreatLevels { get => threatLevels; private set => threatLevels = value; }
        /// <summary>
        /// the threatlevel of the boss
        /// </summary>
        public EnemyData BossEnemy
        {
            get { return bossEnemy.GetEnemyAt(0); }
        }
    }
}
