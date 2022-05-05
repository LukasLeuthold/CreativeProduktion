using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "new LevelInfo", menuName = "Level/LevelInfo")]
    public class LevelInfo : InitScriptObject
    {
        [Header("WaveManagement")]
        [SerializeField]private int maxWaveCount;
        [SerializeField]private int currWave;
        [SerializeField] private AnimationCurve difficultieCurve;

        [Header("Rewards")]
        [SerializeField]private int goldPerWave;
        [SerializeField]private int xpPerWave;


        //[Header("Events")]
        //[SerializeField]private INTScriptableEvent OnWaveStart;
        //[SerializeField]private INTScriptableEvent OnWaveEnd;

        public int MaxWaveCount { get =>maxWaveCount; private set => maxWaveCount = value; }
        public int CurrWave { get => currWave; set => currWave = value; }
        public int GoldPerWave { get => goldPerWave; private set => goldPerWave = value; }
        public int XpPerWave { get => xpPerWave; set => xpPerWave = value; }
        public AnimationCurve DifficultieCurve { get => difficultieCurve; private set => difficultieCurve = value; }

        public override void Initialize()
        {
            currWave = 0;

        }
        //public void StartWave()
        //{
        //    OnWaveStart.Raise(currWave);
        //}
    }
}
