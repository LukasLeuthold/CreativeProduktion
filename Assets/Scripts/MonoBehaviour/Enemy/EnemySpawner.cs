using System;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyWaveBuilder waveBuilder;
        [SerializeField] private LevelInfo levelInfo;
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private int spawnProbability;
        [SerializeField] private RectTransform[] spawnFieldTransforms;

        private Queue<EnemyData> enemiesInWave;
        public Queue<EnemyData> EnemiesInWave
        {
            get { return enemiesInWave; }
            set { enemiesInWave = value; }
        }

        

        
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
            DebugWave();
        }
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

        public void SpawnNextEnemy()
        {

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

                            GameField.Instance.EnemyList.Add(enemy);

                        }
                    }
                }
            }
            
        }

    }
}
