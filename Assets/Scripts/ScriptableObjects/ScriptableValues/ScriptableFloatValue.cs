using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// a scriptable object data container for a float value
    /// </summary>
    [CreateAssetMenu(menuName ="ScriptableValues/FloatValue",fileName = "new ScriptableFloatValue")]
    public class ScriptableFloatValue : ScriptableObject
    {
        /// <summary>
        /// value of the data container
        /// </summary>
        [SerializeField] private float myValue;
        /// <summary>
        /// value of the data container
        /// </summary>
        public float Value { get => myValue; set => myValue = value; }
    }
}
