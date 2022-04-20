using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        [SerializeField]internal VOIDScriptableEvent onEnemyMove;

        [SerializeField] private SOGameField _Field;
        Queue<HeroData> sortHeros = new Queue<HeroData>();
        Queue<EnemyData> sortEnemys = new Queue<EnemyData>();
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
            
            HeroData[] hDatas = _Field.HDatas.ToArray();
                          
            
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
            StartCoroutine(_UnitsAttack());
        }
        internal void EnemyMoA()
        {
            EnemyData[] eDatas = GameField.Instance.Enemys.ToArray();


            for (int e = 0; e < eDatas.Length; e++)
            {
                int speed = int.MinValue;
                for (int i = 0; i < eDatas.Length; i++)
                {
                    if (eDatas[i] != null && speed <= eDatas[i].CurrStatBlock.Speed + eDatas[i].CurrStatModifier.SpeedMod)
                    {
                        speed = eDatas[i].CurrStatBlock.Speed + eDatas[i].CurrStatModifier.SpeedMod;
                    }
                }
                for (int i = 0; i < eDatas.Length; i++)
                {
                    if (eDatas[i] != null && speed == eDatas[i].CurrStatBlock.Speed + eDatas[i].CurrStatModifier.SpeedMod)
                    {
                        sortEnemys.Enqueue(eDatas[i]);
                        eDatas[i] = null;
                    }
                }
            }
            StartCoroutine(_EnemyAttack());
        }
        private IEnumerator _EnemyAttack()
        {
            float time = 0.5f;
            
            for (int i = 0; i <= GameField.Instance.Enemys.Length; i++)
            {
                if (sortEnemys.Count != 0)
                {

                    if (sortEnemys.Peek().nextPosition.x > 0)
                    {
                        if (GameField.Instance.Slots[(int)sortEnemys.Peek().nextPosition.x - 1, (int)sortEnemys.Peek().nextPosition.y].GetComponent<UnitSlot>() != null && GameField.Instance.Slots[(int)sortEnemys.Peek().nextPosition.x - 1, (int)sortEnemys.Peek().nextPosition.y].GetComponent<UnitSlot>()._HData != null)
                        {
                            sortEnemys.Dequeue().Attack();
                            yield return new WaitForSeconds(time);
                        }
                        else
                        {
                            sortEnemys.Dequeue().Move();
                            yield return new WaitForSeconds(time);
                        }
                    }
                    else
                    {
                        
                        for (int e = 0; e < GameField.Instance.Enemys.Length; e++)
                        {
                            if (GameField.Instance.Enemys[e] == sortEnemys.Peek())
                            {
                                sortEnemys.Dequeue().DestroyEnemy();
                                GameField.Instance.Enemys[e] = null;
                                break;
                            }
                        }
                    }

                }

            }
            SetState("Unit");
        }
        private IEnumerator _UnitsAttack()
        {
            float time = 0.5f;

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
