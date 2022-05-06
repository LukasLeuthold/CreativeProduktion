using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "new EnemyPool", menuName = "Enemy/EnemyPool")]
    public class EnemyPool : ScriptableObject
    {
        [SerializeField]
        private ThreatLevel[] threatLevels = new ThreatLevel[1];
        [SerializeField]
        private ThreatLevel bossEnemy;
        public ThreatLevel[] ThreatLevels { get => threatLevels; private set => threatLevels = value; }

        public EnemyData BossEnemy
        {
            get { return bossEnemy.GetEnemyAt(0); }
        }
    }
}
