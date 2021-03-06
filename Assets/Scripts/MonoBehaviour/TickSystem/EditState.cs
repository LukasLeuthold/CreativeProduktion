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
            tickManager.SkipButton.SetActive(true);
            tickManager.ResetSlider(tickManager.editTime);
            tickManager.currStateText.text = "Edit";
            tickManager.LevelI.CurrWave++;
            tickManager.BlockImage.SetActive(false);
            tickManager.CallOnEditStart();
            for (int i = 0; i < 2; i++)
            {
                for (int e = 0; e < 3; e++)
                {
                    if (GameField.Instance.Slots[i,e].GetComponent<UnitSlot>().Unit != null)
                    {
                        GameField.Instance.Slots[i, e].GetComponent<UnitSlot>().Unit.CurrHP = GameField.Instance.Slots[i, e].GetComponent<UnitSlot>()._HData.CurrStatBlock.MaxHP;
                        GameField.Instance.Slots[i, e].GetComponent<UnitSlot>().Unit.isDead = false;
                        GameField.Instance.Slots[i, e].GetComponent<UnitSlot>().Unit.CantDragDrop = false;

                    }
                }
            }
        }
        public override void HandleState()
        {
            tickManager.currTime += Time.deltaTime;
            tickManager.timeSlider.value = tickManager.currTime;

            if (tickManager.currTime >= tickManager.editTime)
            {
                
                tickManager.SetState("Fight");
            }
        }

        
        public override void ExitState()
        {        
            tickManager.BlockImage.SetActive(true);
            tickManager.SkipButton.SetActive(false);
            tickManager.CallOnEditEnd();
        }
    }
}
