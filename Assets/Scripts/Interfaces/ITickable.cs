using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public interface ITickable
    {
        public event Action OnTurnStart;
        public event Action OnMoving;
        public event Action OnAttack;
        public event Action OnOnKill;
        public event Action OnTurnEnd;




        public void Tick();

    }
}
