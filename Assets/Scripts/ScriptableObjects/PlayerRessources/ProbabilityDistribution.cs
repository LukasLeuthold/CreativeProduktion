using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// Scriptable object storing info about custom probability distribution
    /// </summary>
    [CreateAssetMenu(fileName = "new ProbabilityDistribution",menuName ="Player/ProbailityDistribution")]
    public class ProbabilityDistribution : ScriptableObject
    {
        /// <summary>
        /// probability for a common unit
        /// </summary>
        public int probabilityCommon;
        /// <summary>
        /// probability for a rare unit
        /// </summary>
        public int probabilityRare;
        /// <summary>
        /// probability for a lord unit
        /// </summary>
        public int probabilityLord;
    }
}
