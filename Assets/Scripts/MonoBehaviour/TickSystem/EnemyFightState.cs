using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class EnemyFightState : TickState
    {
        /// <summary>
        /// Referenz tickmanager
        /// </summary>
        TickManager tickManager;
        /// <summary>
        /// default Values
        /// </summary>
        public EnemyFightState(TickManager _TManager)
        {
            tickManager = _TManager;    
        }
        public override void EnterState()
        {
            tickManager.SortEnemy();
        }
        public override void HandleState()
        {
           
        }
    }
}
