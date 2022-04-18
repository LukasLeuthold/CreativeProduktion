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


        Queue<EnemyData> enemiesInWave;

        //test
        public int Testpoints;
        public bool debug;

        private void Start()
        {
            BuildWave(Testpoints);
            if (debug)
            {
                DebugWave();
            }
            //SpawnNextEnemy();
        }
        public void BuildWave()
        {
            enemiesInWave = waveBuilder.BuildEnemyWave(Mathf.RoundToInt(levelInfo.DifficultieCurve.Evaluate(levelInfo.CurrWave)));
        }
        public void BuildWave(int _points)
        {
            enemiesInWave = waveBuilder.BuildEnemyWave(_points);
        }
        private void DebugWave()
        {
            if (enemiesInWave == null)
            {
                Debug.Log("null");
                return;
            }
            EnemyData[] enemieDebug = enemiesInWave.ToArray();
            foreach (EnemyData enemy in enemieDebug)
            {
                Debug.Log(enemy.ToString());
            }
        }

        public void SpawnNextEnemy()
        {
            for (int i = 0; i < spawnFieldTransforms.Length; i++)
            {
                if (spawnFieldTransforms[i].GetComponent<EnemyField>().EnemyOnField == null)
                {
                    if (UtilRandom.GetPercentageSuccess(spawnProbability))
                    {
                        GameObject clone = Instantiate(enemyPrefab, spawnFieldTransforms[i].position, Quaternion.identity, this.transform);
                        EnemyData enemy = enemiesInWave.Dequeue();
                        clone.GetComponent<Enemy>().EnemyData = enemy;
                        spawnFieldTransforms[i].GetComponent<EnemyField>().EnemyOnField = enemy;
                        // ToDO zu Array hinzufügen
                    }
                }
            }
        }
    }
}
