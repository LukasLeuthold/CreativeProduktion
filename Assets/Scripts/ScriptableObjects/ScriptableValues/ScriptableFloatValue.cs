using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(menuName ="ScriptableValues/FloatValue",fileName = "new ScriptableFloatValue")]
    public class ScriptableFloatValue : ScriptableObject
    {
        [SerializeField] private float myValue;
        public float Value { get => myValue; set => myValue = value; }
    }
}
