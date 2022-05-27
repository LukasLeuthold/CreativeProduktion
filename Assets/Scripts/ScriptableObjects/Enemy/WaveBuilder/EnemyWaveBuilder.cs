using System;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// handles logic for building enemy waves
    /// </summary>
    [CreateAssetMenu(fileName = "new EnemyWaveBuilder", menuName = "Enemy/WaveBuilder")]
    public class EnemyWaveBuilder : ScriptableObject
    {
        /// <summary>
        /// enemypool the wave is build from
        /// </summary>
        [SerializeField] private EnemyPool enemyPool;

        /// <summary>
        /// builds a random wave from a pool with a given pointamount
        /// </summary>
        /// <param name="_pointsForWave">given pointamount</param>
        /// <returns></returns>
        public Queue<EnemyData> BuildEnemyWave(int _pointsForWave)
        {
            Queue<EnemyData> enemyDatas = new Queue<EnemyData>();
            List<ThreatLevel> possibleBuys = new List<ThreatLevel>();
            int pointsToBuy = _pointsForWave;
            do
            {
                possibleBuys.Clear();
                for (int i = 0; i < enemyPool.ThreatLevels.Length; i++)
                {
                    if (enemyPool.ThreatLevels[i].PointCost > 0 && enemyPool.ThreatLevels[i].PointCost <= pointsToBuy)
                    {
                        possibleBuys.Add(enemyPool.ThreatLevels[i]);
                    }
                }
                if (possibleBuys.Count > 0)
                {
                    int threatLevel = UtilRandom.GetRandomIntFromRange(0, possibleBuys.Count);
                    pointsToBuy -= possibleBuys[threatLevel].PointCost;
                    enemyDatas.Enqueue(possibleBuys[threatLevel].GetRandomEnemy());
                }
            } while (possibleBuys.Count>0);
            return enemyDatas;
        }
        /// <summary>
        /// builds a boss wave from a pool 
        /// </summary>
        /// <param name="_pointsForWave">given pointamount</param>
        /// <returns></returns>
        public Queue<EnemyData> BuildBossWave(int _pointsForWave)
        {
            Queue<EnemyData> enemyDatas = new Queue<EnemyData>();
            for (int i = 0; i < 3; i++)
            {
                enemyDatas.Enqueue(enemyPool.BossEnemy.GetCopy());
            }
            return enemyDatas;
        }
    }
}
