using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "new EnemyPool", menuName = "ScriptableUnitPool/EnemyPool", order = 2)]
    public class EnemyPool : ScriptableObject
    {
        [SerializeField]
        private ThreatLevel[] enemiesInPool;
        [SerializeField]
        private EnemyData bossEnemy;
        public ThreatLevel[] EnemiesInPool { get => enemiesInPool; set => enemiesInPool = value; }

        //public EnemyData GetRandomEnemy(int _threatLevel)
        //{
        //    if (_threatLevel == -1)
        //    {
        //        return bossEnemy;
        //    }
        //    else
        //    {
        //        return EnemiesInPool[_threatLevel].GetRandomEnemy();
        //    }
        //}
    }

    [System.Serializable]
    public struct ThreatLevel
    {
        [SerializeField]
        private string name;

        public readonly int pointCost;

        [SerializeField]
        private EnemyData[] enemiesInThreatLevel;

        public EnemyData GetRandomEnemy()
        {
            return enemiesInThreatLevel[UtilRandom.GetRandomIntFromRange(0, enemiesInThreatLevel.Length)];
        }
    }

}
