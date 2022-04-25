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
            tickManager.ResetSlider();
            tickManager.currStateText.text = "Edit";
            tickManager.LevelI.CurrWave++;
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
            if (tickManager.currTime >= tickManager.editTime)
            {
                tickManager.SetState("Fight");
            }
        }
    }
}
