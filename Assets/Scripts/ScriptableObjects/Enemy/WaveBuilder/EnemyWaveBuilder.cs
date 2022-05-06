using System;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "new EnemyWaveBuilder", menuName = "Enemy/WaveBuilder")]
    public class EnemyWaveBuilder : ScriptableObject
    {
        [SerializeField] private EnemyPool enemyPool;

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

        public Queue<EnemyData> BuildBossWave(int _pointsForWave)
        {
            //Queue<EnemyData> enemyDatas = BuildEnemyWave(_pointsForWave);
            Queue<EnemyData> enemyDatas = new Queue<EnemyData>();
            for (int i = 0; i < 3; i++)
            {
                enemyDatas.Enqueue(enemyPool.BossEnemy.GetCopy());
            }
            return enemyDatas;
        }
    }
}
