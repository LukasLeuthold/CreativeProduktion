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
                tickManager.CallOnWaveEnd();
                tickManager.enemySpawner.BuildWave();
                tickManager.SetState("Edit");
            }
            else
            {
                tickManager.currStateText.text = "Fight";
                tickManager.SetState("Enemy");
            }

            for (int i = 0; i < 2; i++)
            {
                for (int e = 0; e < 3; e++)
                {
                    if (GameField.Instance.Slots[i, e].GetComponent<UnitSlot>().Unit != null)
                    {
                        GameField.Instance.Slots[i, e].GetComponent<UnitSlot>().Unit.CantDragDrop = true;
                    }
                }
            }
        }
        public override void HandleState()
        {


        }
    }
}
