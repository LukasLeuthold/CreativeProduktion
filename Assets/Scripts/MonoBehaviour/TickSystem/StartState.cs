using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class StartState : TickState
    {
        private TickManager tickManager;
        public StartState(TickManager _TManager)
        {
            tickManager = _TManager;
        }

        public override void EnterState()
        {
            
        }
        public override void HandleState()
        {
            tickManager.timeSlider.value = 0;
            tickManager.SetState("Edit");
        }
    }
}
