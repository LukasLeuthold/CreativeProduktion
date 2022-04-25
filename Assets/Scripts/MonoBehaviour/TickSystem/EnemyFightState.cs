using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class EnemyFightState : TickState
    {
        TickManager tickManager;
        public EnemyFightState(TickManager _TManager)
        {
            tickManager = _TManager;    
        }
        public override void EnterState()
        {
            tickManager.EnemyMoA();
            Debug.Log("Enemy");
        }
        public override void HandleState()
        {
           
        }
    }
}
