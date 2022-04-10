using UnityEditor;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// custom Inspector for the scriptable event with an int parameter
    /// </summary>
    [CustomEditor(typeof(INTScriptableEvent))]
    public class INTScriptableEventInspector : Editor
    {
        /// <summary>
        /// adds the raise manualy button
        /// </summary>
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Raise Event with manual Value"))
            {
                (target as INTScriptableEvent).RaiseManualy();
            }
        }
    }
}
