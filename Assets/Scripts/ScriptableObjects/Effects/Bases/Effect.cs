using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// abstract base class for effects
    /// </summary>
    public abstract class Effect : InitScriptObject
    {
        /// <summary>
        /// apllies the effect to the herodata
        /// </summary>
        /// <param name="_hero">herodata to apply effect to</param>
        public abstract void ApplyEffect(HeroData _hero);
        /// <summary>
        /// removes the effect from the herodata
        /// </summary>
        /// <param name="_hero">herodata to remove effect from</param>
        public abstract void RemoveEffect(HeroData _hero);
        /// <summary>
        /// apllies the effect to hero collection
        /// </summary>
        /// <param name="_collection">collection to apply effect to</param>
        public virtual void ApplyEffectToGroup(HeroCollection _collection) { }
        /// <summary>
        /// removes the effect from the hero collection
        /// </summary>
        /// <param name="_collection">collection to remove effect from</param>
        public virtual void RemoveEffectFromGroup(HeroCollection _collection) { }
    }
}
