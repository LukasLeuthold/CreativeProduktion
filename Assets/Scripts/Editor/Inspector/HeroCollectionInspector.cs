using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AutoDefense
{
    [CustomEditor(typeof(HeroCollection))]
    public class HeroCollectionInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if(GUILayout.Button("Clear Collection"))
            {
                (target as HeroCollection).Initialize();
            }
            //if (GUILayout.Button("Apply Effect"))
            //{
            //    (target as HeroCollection).ClearCollection();
            //}
            //if (GUILayout.Button("Remove Effect"))
            //{
            //    (target as HeroCollection).ClearCollection();
            //}
        }
    }
}
