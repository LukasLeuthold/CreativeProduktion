using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "new ProbabilityDistribution",menuName ="Player/ProbailityDistribution")]
    public class ProbabilityDistribution : ScriptableObject
    {
        public int probabilityCommon;
        public int probabilityRare;
        public int probabilityLord;
    }
}
