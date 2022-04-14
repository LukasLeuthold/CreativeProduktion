using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class EditState :  TickState
    {
        public EditState(TickManager _TManager)
        {

        }
        public override TickState HandleState()
        {
            return this;
        }
    }
}
