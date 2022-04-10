using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "new EnemyWaveBuilder", menuName = "Enemy/WaveBuilder", order = 2)]
    public class EnemyWaveBuilder : ScriptableObject
    {
        [SerializeField] private EnemyPool enemyPool;
        [SerializeField] private AnimationCurve difficultieCurve;
        [SerializeField,Range(0,100)] private int probabilityForEnemyToSpawn;

        //welcher der drei stellen werden gefült
        //mit welchen einheiten werden sie gefüllt
        //sind noch punkte übrig
        public SpawnRow[] BuildWavePlan(int _waveCount)
        {
            if (_waveCount == -1)
            {
                return BuildBossWave();
            }
            else
            {
                return BuildMinionWave(_waveCount);
            }
        }

        private SpawnRow[] BuildBossWave()
        {
            throw new NotImplementedException();
        }

        private SpawnRow[] BuildMinionWave(int _waveCount)
        {
            int maxPointsForWave = Mathf.RoundToInt(difficultieCurve.Evaluate(_waveCount));
            List<SpawnRow> spawnRows = new List<SpawnRow>();
            do
            {
                EnemyData[] enemiesForSpawnRow = new EnemyData[3];
                for (int i = 0; i < enemiesForSpawnRow.Length; i++)
                {
                    if (UtilRandom.GetPercentageSuccess(probabilityForEnemyToSpawn))
                    {
                        bool enemyChosen = false;
                        do
                        {
                            int randomThreatLevel = UtilRandom.GetRandomIntFromRange(0, enemyPool.EnemiesInPool.Length);
                            maxPointsForWave -= enemyPool.EnemiesInPool[randomThreatLevel].pointCost;

                            enemiesForSpawnRow[i] = enemyPool.EnemiesInPool[randomThreatLevel].GetRandomEnemy();


                        } while (!enemyChosen);
                    }
                    else
                    {
                        enemiesForSpawnRow[i] = null;
                    }
                }
                spawnRows.Add(new SpawnRow(enemiesForSpawnRow));
            } while (maxPointsForWave > 0);

            return spawnRows.ToArray();
        }
    }

    public struct SpawnRow
    {
        public SpawnRow(EnemyData[] enemies)
        {
            enemiesToSpawn = enemies;
        }
        public EnemyData[] enemiesToSpawn;
    }
}
