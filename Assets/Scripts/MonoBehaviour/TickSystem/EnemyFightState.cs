using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class EnemyFightState : TickState
    {
        public EnemyFightState(TickManager _TManager)
        {

        }
        public override TickState HandleState()
        {
            return this;
        }
    }
}
