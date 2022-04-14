using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class TickState 
    {
        virtual public TickState HandleState()
        {
            return this;
        }
    }
}
