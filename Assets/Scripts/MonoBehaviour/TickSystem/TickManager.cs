using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace AutoDefense
{
    
    public class TickManager : MonoBehaviour
    {

        public Dictionary<string, TickState> _TickStates;
        private TickState state;
        [SerializeField]internal LevelInfo levelI;
        [SerializeField]internal TMP_Text currStateText; 
        [SerializeField]internal Slider timeSlider;
        [SerializeField]internal int editTime;

        internal float currTime;

        private Text CurrSateText;
        

        [SerializeField] SOGameField _Field;
        Queue<HeroData> sortHeros = new Queue<HeroData>();
        void Start()
        {
            _TickStates = new Dictionary<string, TickState>()
        {
            {"Start",new StartState(this)},
            {"Edit", new EditState(this)},
            {"Fight", new FightState(this)},
            {"Unit", new UnitFightState(this)},
            {"Enemy", new EnemyFightState(this)}
        };
            state = _TickStates["Start"];
            
        }

        void Update()
        {
            state.HandleState();
            Debug.Log(state);
        }

        public void OnWaveEnd(int waveNumber)
        {
            SetState("Edit");
        }
        internal void SetState(string stateName)
        {
            if (state != null)
            {
                state.ExitState();
            }
            state = _TickStates[stateName];
         
            state.EnterState();
        }

        internal void ResetSlider()
        {
            currTime = 0;
            timeSlider.value = 0;
            timeSlider.maxValue = editTime;
        }

        internal void EnemyAttack()
        {

        }
        internal void UnitAttack()
        {
            
            HeroData[] hDatas = _Field.HDatas;
            
            for (int e = 0; e < hDatas.Length; e++)
            {
                int speed = int.MinValue;
                for (int i = 0; i < hDatas.Length; i++)
                {
                    if (hDatas[i] != null && speed <= hDatas[i].CurrStatBlock.Speed + hDatas[i].CurrStatModifier.SpeedMod)
                    {
                        speed = hDatas[i].CurrStatBlock.Speed + hDatas[i].CurrStatModifier.SpeedMod;
                    }
                }
                for (int i = 0; i < hDatas.Length; i++)
                {
                    if (hDatas[i] != null && speed == hDatas[i].CurrStatBlock.Speed + hDatas[i].CurrStatModifier.SpeedMod)
                    {
                        sortHeros.Enqueue(hDatas[i]);
                        hDatas[i] = null;
                    }
                }
            }
            StartCoroutine(UnitsAttack());
        }

        private IEnumerator UnitsAttack()
        {
            int time = 2;

            for (int i = 0; i <= 6; i++)
            {
                if (sortHeros.Count != 0)
                {
                    sortHeros.Dequeue().Attack();
                    yield return new WaitForSeconds(time);
                }
                
            }
            SetState("Fight");
        }
    }
}
