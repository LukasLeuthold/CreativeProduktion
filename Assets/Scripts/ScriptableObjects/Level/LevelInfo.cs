using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "new LevelInfo", menuName = "Level/LevelInfo")]
    public class LevelInfo : InitScriptObject
    {
        [SerializeField]private int maxWaveCount;
        [SerializeField]private int currWave;

        //[SerializeField]private int minDifficultie;
        //[SerializeField]private int maxDifficultie;
        [SerializeField] private AnimationCurve difficultieCurve;


        [Header("Events")]
        [SerializeField]private INTScriptableEvent OnWaveStart;
        [SerializeField]private INTScriptableEvent OnWaveEnd;

        public int MaxWaveCount { get =>maxWaveCount; private set => maxWaveCount = value; }
        public int CurrWave { get => currWave; set => currWave = value; }
        public AnimationCurve DifficultieCurve { get => difficultieCurve; private set => difficultieCurve = value; }

        public override void Initialize()
        {
            currWave = 0;

        }
    }
}
