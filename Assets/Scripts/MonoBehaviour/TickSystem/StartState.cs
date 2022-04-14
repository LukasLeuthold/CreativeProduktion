using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class StartState : TickState
    {
        public StartState(TickManager _TManager)
        {

        }
        public override TickState HandleState()
        {
            return this;
        }
    }
}
