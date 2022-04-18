using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class FightState : TickState
    {
        private TickManager tickManager;
        public FightState(TickManager _TManager)
        {
            tickManager = _TManager;
        }
        public override void HandleState()
        {
            if (true)
            {
                tickManager.SetState("Unit");
            }           
        }
    }
}
