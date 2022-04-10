using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public interface IDamageable
    {
        public event Action OnTakingDamage;
        public event Action OnDeath;

        public void TakeDamage(int _amount);
    }
}
