using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class TickManager : MonoBehaviour
    {
        public Dictionary<string, TickState> _TickStates;
        private TickState state;
        void Start()
        {
            _TickStates = new Dictionary<string, TickState>()
        {
            {"Start",new StartState(this)},
            {"Edit", new EditState(this)},
            {"Unit", new UnitFightState(this)},
            {"Enemy", new EnemyFightState(this)}
        };
            state = _TickStates["Start"];
        }

        void Update()
        {
            state = state.HandleState();
        }
    }
}
