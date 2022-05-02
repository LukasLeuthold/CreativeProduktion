using UnityEditor;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// custom Inspector for the scriptable event with an bool parameter
    /// </summary>
    [CustomEditor(typeof(BOOLScriptableEvent))]
    public class BOOLScriptableEventInspector : Editor
    {

        /// <summary>
        /// adds the raise manualy button
        /// </summary>
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Raise Event with manual Value"))
            {
                (target as BOOLScriptableEvent).RaiseManualy();
            }
        }
    }
}
