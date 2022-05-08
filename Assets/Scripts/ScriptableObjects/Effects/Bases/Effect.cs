using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public abstract class Effect : InitScriptObject
    {
        public abstract void ApplyEffect(HeroData _hero);
        public abstract void RemoveEffect(HeroData _hero);

        public virtual void ApplyEffectToGroup(HeroCollection _collection) { }
        public virtual void RemoveEffectFromGroup(HeroCollection _collection) { }
    }
}
