using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace AutoDefense
{
    [CustomEditor(typeof(HeroPool))]

    public class HeroPoolInspector : Editor
    {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
//                if (GUILayout.Button("Get LineUp"))
//{
//                   // (target as HeroPool).GetLineUp((target as HeroPool).testAmount, (target as HeroPool).testProp);
//                }
            }
    }
}
