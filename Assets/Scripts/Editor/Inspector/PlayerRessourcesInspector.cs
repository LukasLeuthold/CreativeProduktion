using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AutoDefense
{
    [CustomEditor(typeof(PlayerRessources))]
    public class PlayerRessourcesInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("AddXP"))
            {
                (target as PlayerRessources).AddXp();
            }
        }
    }
}
