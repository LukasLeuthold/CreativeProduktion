using UnityEngine;

namespace AutoDefense
{
    public class BreakState : TickState
    {
        TickManager tickManager;
        public BreakState(TickManager _TManager)
        {
            tickManager = _TManager;
            
        }

        public override void EnterState()
        {
            if (tickManager.enemySpawner.EnemiesInWave.Count <= 0 && GameField.Instance.EnemyList.Count <= 0)
            {
                tickManager.CallOnWaveEnd();
                tickManager.SetState("Edit");
            }
            tickManager.ResetSlider(tickManager.breakTime);
            tickManager.currStateText.text = "Break";
            for (int i = 0; i < 2; i++)
            {
                for (int e = 0; e < 3; e++)
                {
                    if (GameField.Instance.Slots[i, e].GetComponent<UnitSlot>().Unit != null)
                    {
                        GameField.Instance.Slots[i, e].GetComponent<UnitSlot>().Unit.CantDragDrop = false;
                    }
                }
            }
        }
        public override void HandleState()
        {
            tickManager.currTime += Time.deltaTime;
            tickManager.timeSlider.value = tickManager.currTime;

            if (tickManager.currTime >= tickManager.breakTime)
            {
                tickManager.SetState("Fight");
            }
        }

    }
}
