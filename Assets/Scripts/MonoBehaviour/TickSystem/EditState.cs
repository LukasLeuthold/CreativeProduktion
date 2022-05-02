using UnityEngine;

namespace AutoDefense
{
    public class EditState : TickState
    {
        TickManager tickManager;
        public EditState(TickManager _TManager)
        {
            tickManager = _TManager;
        }

        public override void EnterState()
        {
            tickManager.skipButton.SetActive(true);
            tickManager.ResetSlider(tickManager.editTime);
            tickManager.currStateText.text = "Edit";
            tickManager.LevelI.CurrWave++;
            tickManager.SwitchDragDropGameAll(true);

            for (int i = 0; i < 2; i++)
            {
                for (int e = 0; e < 3; e++)
                {
                    if (GameField.Instance.Slots[i,e].GetComponent<UnitSlot>().Unit != null)
                    {
                        GameField.Instance.Slots[i, e].GetComponent<UnitSlot>().Unit.CurrHP = GameField.Instance.Slots[i, e].GetComponent<UnitSlot>()._HData.CurrStatBlock.MaxHP;
                        GameField.Instance.Slots[i, e].GetComponent<UnitSlot>().Unit.isDead = false;

                    }
                }
            }
        }
        public override void HandleState()
        {
            tickManager.currTime += Time.deltaTime;
            tickManager.timeSlider.value = tickManager.currTime;

            if (tickManager.LevelI.CurrWave > tickManager.LevelI.MaxWaveCount)
            {
                //TODO End Level
                Debug.Log("Level End");
            }
            else if (tickManager.currTime >= tickManager.editTime)
            {
                
                tickManager.SetState("Fight");
            }
        }

        
        public override void ExitState()
        {          
            tickManager.skipButton.SetActive(false);
            tickManager.SwitchDragDropGameAll(false);
        }
    }
}
