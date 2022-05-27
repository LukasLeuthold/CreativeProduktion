using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// abstract base class for initializable objects
    /// </summary>
    public abstract class InitScriptObject : ScriptableObject
    {
        /// <summary>
        /// handles initialization logic
        /// </summary>
        public abstract void Initialize();
    }
}
