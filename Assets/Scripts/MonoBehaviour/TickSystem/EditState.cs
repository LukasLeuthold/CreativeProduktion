using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class EditState :  TickState
    {
        TickManager tickManager;       
        public EditState(TickManager _TManager)
        {
            tickManager = _TManager;
        }

        public override void EnterState()
        {
            tickManager.ResetSlider();
        }
        public override void  HandleState()
        {               
            tickManager.currTime += Time.deltaTime;
            tickManager.timeSlider.value = tickManager.currTime;
                
                if (tickManager.currTime >= tickManager.editTime)
                {
                    tickManager.SetState("Fight");
                }                      
        }
    }
}
