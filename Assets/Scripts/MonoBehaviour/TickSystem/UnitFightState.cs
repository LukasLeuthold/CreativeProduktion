using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class UnitFightState : TickState
    {
        TickManager tickManager;
        public UnitFightState(TickManager _TManager)
        {
            tickManager = _TManager;
        }
        public override void EnterState()
        {
            tickManager.UnitAttack();
        }
        public override void HandleState()
        {
            
        }
    }
}
