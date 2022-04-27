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


            if (tickManager.enemySpawner.EnemiesInWave.Count > 0)
            {
                tickManager.enemySpawner.SpawnNextEnemy();
            }

            if (tickManager.enemySpawner.EnemiesInWave.Count <= 0 && GameField.Instance.EnemyList.Count <= 0)
            {
                tickManager.enemySpawner.BuildWave();
                tickManager.SetState("Edit");
            }
            else
            {
                tickManager.currStateText.text = "Fight";
                tickManager.SetState("Enemy");
            }
        }
        public override void HandleState()
        {


        }
    }
}
