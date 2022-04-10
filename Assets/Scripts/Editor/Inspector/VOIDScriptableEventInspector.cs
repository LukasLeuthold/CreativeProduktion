using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// custom Inspector for the scriptable event without parameter
    /// </summary>
    [CustomEditor(typeof(VOIDScriptableEvent))]
    public class VOIDScriptableEventInspector : Editor
    {
        /// <summary>
        /// adds the raise event button
        /// </summary>
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if(GUILayout.Button("Raise Event"))
            {
                (target as VOIDScriptableEvent).Raise();
            }
        }
    }
}
