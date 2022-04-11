using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyWaveBuilder waveBuilder;
        [SerializeField] private LevelInfo levelInfo;

        Queue<EnemyData> enemiesInWave;

        //test
        public int Testpoints;

        private void Start()
        {
            BuildWave(Testpoints);
            DebugWave();
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

        //TODO: implement spawning of queue
        
    }
}
