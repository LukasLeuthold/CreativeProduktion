using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class Initializer : MonoBehaviour
    {
        [SerializeField] private InitScriptObject[] initializableObjects;

        private void Awake()
        {
            for (int i = 0; i < initializableObjects.Length; i++)
            {
                initializableObjects[i].Initialize();
            }
        }
    }
}
