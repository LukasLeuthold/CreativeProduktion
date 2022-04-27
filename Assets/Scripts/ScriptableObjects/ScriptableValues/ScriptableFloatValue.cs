using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(menuName ="ScriptableValues/FloatValue",fileName = "new ScriptableFloatValue")]
    public class ScriptableFloatValue : ScriptableObject
    {
        [SerializeField] private float value;
        public float Value { get; set; }

        private void OnValidate()
        {
            Value = value;
        }
    }
}
