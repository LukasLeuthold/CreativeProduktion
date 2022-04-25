using UnityEngine;

namespace AutoDefense
{
    public class FightState : TickState
    {
        private TickManager tickManager;
        public FightState(TickManager _TManager)
        {
            tickManager = _TManager;
        }
        public override void EnterState()
        {
            Debug.Log("Fight");


            if (tickManager.enemySpawner.EnemiesInWave.Count > 0)
            {
                tickManager.enemySpawner.SpawnNextEnemy();
            }
            tickManager.currStateText.text = "Fight";
            tickManager.SetState("Enemy");


        }
        public override void HandleState()
        {


        }
    }
}
