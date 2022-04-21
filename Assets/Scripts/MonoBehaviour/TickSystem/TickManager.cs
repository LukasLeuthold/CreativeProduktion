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
        [SerializeField] internal LevelInfo levelI;
        [SerializeField] internal TMP_Text currStateText;
        [SerializeField] internal Slider timeSlider;
        [SerializeField] internal int editTime;

        internal float currTime;

        private Text CurrSateText;

        [SerializeField] internal VOIDScriptableEvent onEnemyMove;

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
            //Debug.Log(state);
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

        internal void UnitAttack()
        {

            HeroData[] hDatas = _Field.HDatas;
            GameObject[,] slots = GameField.Instance.Slots;

            List<HeroData> herosOnField = new List<HeroData>();

            sortHeros.Clear();

            for (int i = 0; i < hDatas.Length; i++)
            {
                if (hDatas[i] != null)
                {
                    int y = (int)hDatas[i].Unit.LastSlot.field.y;
                    int rang = hDatas[i].CurrStatBlock.Range + hDatas[i].CurrStatModifier.RangeMod;

                    for (int e = 0; e < rang; e++)
                    {
                        if (slots[2 + e, y].GetComponent<EnemyField>().EnemyOnField != null)
                        {
                            herosOnField.Add(hDatas[i]);
                        }
                    }

                }
            }

            while (herosOnField.Count != 0)
            {
                HeroData hData = null;
                int maxSpeed = int.MinValue;
                for (int i = herosOnField.Count - 1; i >= 0; i--)
                {
                    int speed = herosOnField[i].CurrStatBlock.Speed + herosOnField[i].CurrStatModifier.SpeedMod;
                    if (speed >= maxSpeed)
                    {
                        maxSpeed = speed;
                        hData = herosOnField[i];
                    }
                }
                sortHeros.Enqueue(hData);
                herosOnField.Remove(hData);
            }



            //for (int i = 0; i < hDatas.Length; i++)
            //{
            //    if (hDatas[i] != null && hDatas[i] != null && hDatas[i].CurrStatBlock.Speed + hDatas[i].CurrStatModifier.SpeedMod <= minSpeed)
            //    {
            //        minSpeed = hDatas[i].CurrStatBlock.Speed + hDatas[i].CurrStatModifier.SpeedMod;
            //    }
            //}

            //for (int i = 0; i < hDatas.Length; i++)
            //{
            //    if (hDatas[i] != null)
            //    {
            //        int unitSpeed = hDatas[i].CurrStatBlock.Speed + hDatas[i].CurrStatModifier.SpeedMod;

            //        if (minSpeed == unitSpeed)
            //        {
            //            sortHeros.Enqueue(hDatas[i]);
            //            hDatas[i] = null;
            //        }
            //    }
            //}


            if (sortHeros.Count != 0)
            {
                StartCoroutine(_UnitsAttack());
            }
            else
            {
                SetState("Fight");
            }
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
                        int x = (int)sortEnemys.Peek().nextPosition.x;
                        int y = (int)sortEnemys.Peek().nextPosition.y;
                        GameObject[,] slots = GameField.Instance.Slots;

                        if (slots[x - 1, y].GetComponent<UnitSlot>() != null && slots[x - 1, y].GetComponent<UnitSlot>()._HData != null)
                        {
                            Vector2 vector2 =  Vector2.zero;
                            sortEnemys.Dequeue().Attack(vector2);
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
            int count = sortHeros.Count;
            GameObject[,] slots = GameField.Instance.Slots;

            int rang = sortHeros.Peek().CurrStatBlock.Range + sortHeros.Peek().CurrStatModifier.RangeMod;
            int y = (int)sortHeros.Peek().Unit.LastSlot.field.y;


            for (int i = 0; i < count; i++)
            {
                Vector2 targetField = Vector2.zero;
                for (int e = 0; e < rang; e++)
                {
                    if (slots[2 + e, y].GetComponent<EnemyField>().EnemyOnField != null)
                    {
                        targetField = new Vector2(2+e, y);
                        break;
                    }
                }
                sortHeros.Dequeue().Attack(targetField);
                yield return new WaitForSeconds(time);
            }

            sortHeros.Clear();
            SetState("Fight");
        }
    }
}
