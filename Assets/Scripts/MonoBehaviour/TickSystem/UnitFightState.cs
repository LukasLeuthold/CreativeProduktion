using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class UnitFightState : TickState
    {
        /// <summary>
        /// Referenz tickmanager
        /// </summary>
        TickManager tickManager;
        /// <summary>
        /// default Values
        /// </summary>
        public UnitFightState(TickManager _TManager)
        {
            tickManager = _TManager;
        }
        public override void EnterState()
        {
            tickManager.CallOnHeroTurnStartEffects();
            tickManager.UnitAttack();
        }
        public override void HandleState()
        {
            
        }
    }
}
