using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class BreakState : TickState
    {
        TickManager tickManager;
        public BreakState(TickManager _TManager)
        {
            tickManager = _TManager;
        }

        public override void EnterState()
        {
            tickManager.ResetSlider(tickManager.breakTime);
            tickManager.currStateText.text = "Break";
        }
        public override void HandleState()
        {
            tickManager.currTime += Time.deltaTime;
            tickManager.timeSlider.value = tickManager.currTime;
            if (tickManager.currTime >= tickManager.breakTime)
            {
                tickManager.SetState("Fight");
            }
        }
    }
}
