using System;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// handles spawning of enemies
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        /// <summary>
        /// wavebuilder used to build wave
        /// </summary>
        [SerializeField] private EnemyWaveBuilder waveBuilder;
        /// <summary>
        /// info object of the level
        /// </summary>
        [SerializeField] private LevelInfo levelInfo;
        /// <summary>
        /// prefab of the enemy
        /// </summary>
        [SerializeField] private GameObject enemyPrefab;
        /// <summary>
        /// probability of spawning an enemy
        /// </summary>
        [SerializeField] private int spawnProbability;
        /// <summary>
        /// transforms of the spawnfields
        /// </summary>
        [SerializeField] private RectTransform[] spawnFieldTransforms;
        /// <summary>
        /// gets played when enemy is spawned
        /// </summary>
        [SerializeField] private AUDIOScriptableEvent onEnemySpawn;

        /// <summary>
        /// queue of enemies in the wave
        /// </summary>
        private Queue<EnemyData> enemiesInWave;
        /// <summary>
        /// queue of enemies in the wave
        /// </summary>
        public Queue<EnemyData> EnemiesInWave
        {
            get { return enemiesInWave; }
            set { enemiesInWave = value; }
        }
        /// <summary>
        /// uses the wavebuilder to build a wave
        /// </summary>
        public void BuildWave()
        {
            if (levelInfo.CurrWave == levelInfo.MaxWaveCount)
            {
                EnemiesInWave = waveBuilder.BuildBossWave(Mathf.RoundToInt(levelInfo.DifficultieCurve.Evaluate(levelInfo.CurrWave)));
            }
            else
            {
                EnemiesInWave = waveBuilder.BuildEnemyWave(Mathf.RoundToInt(levelInfo.DifficultieCurve.Evaluate(levelInfo.CurrWave)));
            }
        }
        /// <summary>
        /// debugs the wave build
        /// </summary>
        private void DebugWave()
        {
            if (EnemiesInWave == null)
            {
                Debug.Log("null");
                return;
            }
            EnemyData[] enemieDebug = EnemiesInWave.ToArray();
            foreach (EnemyData enemy in enemieDebug)
            {
                Debug.Log(enemy.ToString());
            }
        }
        /// <summary>
        /// spawns the wave from the queue on the spawnfields 
        /// </summary>
        public void SpawnNextEnemy()
        {
            bool spawnedSomething = false;
            if (levelInfo.CurrWave == levelInfo.MaxWaveCount)
            {
                for (int i = 0; i < spawnFieldTransforms.Length; i++)
                {
                    if (spawnFieldTransforms[i].GetComponent<EnemyField>().EnemyOnField == null && EnemiesInWave.Count > 0)
                    {
                            GameObject clone = Instantiate(enemyPrefab, spawnFieldTransforms[i].position, Quaternion.identity, this.transform);
                            EnemyData enemy = EnemiesInWave.Dequeue();
                            clone.GetComponent<Enemy>().EnemyData = enemy;
                            spawnFieldTransforms[i].GetComponent<EnemyField>().EnemyOnField = enemy;
                            enemy.nextPosition = spawnFieldTransforms[i].GetComponent<EnemyField>().field;
                            GameField.Instance.EnemyList.Add(enemy);
                    }
                }
                spawnedSomething = true;
            }
            else
            {
                for (int i = 0; i < spawnFieldTransforms.Length; i++)
                {
                    if (spawnFieldTransforms[i].GetComponent<EnemyField>().EnemyOnField == null && EnemiesInWave.Count > 0)
                    {
                        if (UtilRandom.GetPercentageSuccess(spawnProbability))
                        {
                            GameObject clone = Instantiate(enemyPrefab, spawnFieldTransforms[i].position, Quaternion.identity, this.transform);
                            EnemyData enemy = EnemiesInWave.Dequeue();
                            clone.GetComponent<Enemy>().EnemyData = enemy;
                            spawnFieldTransforms[i].GetComponent<EnemyField>().EnemyOnField = enemy;
                            enemy.nextPosition = spawnFieldTransforms[i].GetComponent<EnemyField>().field;
                            spawnedSomething = true;

                            GameField.Instance.EnemyList.Add(enemy);

                        }
                    }
                }
            }
            if (spawnedSomething == true)
            {
                onEnemySpawn?.Raise();
            }
        }
    }
}
