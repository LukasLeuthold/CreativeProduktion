using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public abstract class Effect : ScriptableObject
    {
        public abstract void ApplyEffect(HeroData _hero);
        public abstract void RemoveEffect(HeroData _hero);
    }
}
