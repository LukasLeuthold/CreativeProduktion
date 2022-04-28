using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AutoDefense
{
    [CustomEditor (typeof(HeroData))]
    public class HeroTest : Editor
    {
        //public override void OnInspectorGUI()
        //{
        //    DrawDefaultInspector();
        //    if (GUILayout.Button("Place on Field"))
        //    {
        //        (target as HeroData).PlaceOnField();
        //    }
        //    if (GUILayout.Button("Remove from Field"))
        //    {
        //        (target as HeroData).RemoveFromField();
        //    }
        //}
    }
}
