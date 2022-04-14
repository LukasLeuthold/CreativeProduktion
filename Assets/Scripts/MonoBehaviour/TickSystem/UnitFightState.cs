using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class UnitFightState : TickState
    {
        public UnitFightState(TickManager _TManager)
        {

        }
        public override TickState HandleState()
        {
            return this;
        }
    }
}
