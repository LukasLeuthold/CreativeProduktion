using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class TickState 
    {   
        /// <summary>
        /// called once in the beginning
        /// </summary>
        virtual public void EnterState()
        {

        }
        /// <summary>
        /// called in Update
        /// </summary>
        virtual public void HandleState()
        {
            
        }
        /// <summary>
        /// called once in trasiotion
        /// </summary>
        virtual public void ExitState()
        {

        }
    }
}
