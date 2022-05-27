using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// stores infos about the level settings
    /// </summary>
    [CreateAssetMenu(fileName = "new LevelInfo", menuName = "Level/LevelInfo")]
    public class LevelInfo : InitScriptObject
    {
        /// <summary>
        /// maximum amount of waves in level
        /// </summary>
        [Header("WaveManagement")]
        [SerializeField]private int maxWaveCount;
        /// <summary>
        /// current wavecount
        /// </summary>
        [SerializeField]private int currWave;
        /// <summary>
        /// difficultie curve of the level
        /// </summary>
        [SerializeField] private AnimationCurve difficultieCurve;

        /// <summary>
        /// gold reward per wave
        /// </summary>
        [Header("Rewards")]
        [SerializeField]private int goldPerWave;
        /// <summary>
        /// xp reward per wave
        /// </summary>
        [SerializeField]private int xpPerWave;

        /// <summary>
        /// gets called when all waves are over
        /// </summary>
        [Header("Events")]
        [SerializeField] private BOOLScriptableEvent OnGameOver;

        /// <summary>
        /// maximum amount of waves in level
        /// </summary>
        public int MaxWaveCount { get =>maxWaveCount; private set => maxWaveCount = value; }
        /// <summary>
        /// current wavecount
        /// </summary>
        public int CurrWave {
            get => currWave;
            set
            {
                if (value >MaxWaveCount)
                {
                    OnGameOver.Raise(true);
                    return;
                }
                currWave = value; 
            }
        }
        /// <summary>
        /// gold reward per wave
        /// </summary>
        public int GoldPerWave { get => goldPerWave; private set => goldPerWave = value; }
        /// <summary>
        /// xp reward per wave
        /// </summary>
        public int XpPerWave { get => xpPerWave; set => xpPerWave = value; }
        /// <summary>
        /// difficultie curve of the level
        /// </summary>
        public AnimationCurve DifficultieCurve { get => difficultieCurve; private set => difficultieCurve = value; }
        /// <summary>
        /// sets default values
        /// </summary>
        public override void Initialize()
        {
            currWave = 0;
        }
    }
}
