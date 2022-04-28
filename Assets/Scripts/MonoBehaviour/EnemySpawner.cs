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


        public Queue<EnemyData> EnemiesInWave;

        //test
        public int Testpoints;
        public bool debug;

        private void Start()
        {
            BuildWave();

        }
        public void BuildWave()
        {
            EnemiesInWave = waveBuilder.BuildEnemyWave(Mathf.RoundToInt(levelInfo.DifficultieCurve.Evaluate(levelInfo.CurrWave)));
        }
        public void BuildWave(int _points)
        {
            EnemiesInWave = waveBuilder.BuildEnemyWave(_points);
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
