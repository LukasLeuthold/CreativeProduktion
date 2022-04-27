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
        [SerializeField] internal LevelInfo LevelI;
        [SerializeField] internal TMP_Text currStateText;
        [SerializeField] internal Slider timeSlider;
        [SerializeField] internal int editTime;

        public EnemySpawner enemySpawner;

        internal float currTime;

        private Text CurrSateText;

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
                            break;
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

            if (sortHeros.Count > 0)
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
            EnemyData[] eDatas = GameField.Instance.EnemyList.ToArray();
            sortEnemys.Clear();


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
            float time = 0.2f;




            
            for (int i = 0; i < GameField.Instance.EnemyList.Count(); i++)
            {
                if (sortEnemys.Count > 0 && sortEnemys.Peek().nextPosition.x  > 0  )
                {

                    int x = (int)sortEnemys.Peek().nextPosition.x;
                    int y = (int)sortEnemys.Peek().nextPosition.y;
                    GameObject[,] slots = GameField.Instance.Slots;

                    if (slots[x - 1, y].GetComponent<EnemyField>() != null && slots[x - 1, y].GetComponent<EnemyField>().EnemyOnField == null)
                    {
                        sortEnemys.Dequeue().Move();
                        yield return new WaitForSeconds(time);
                    }
                     else if (slots[x - 1, y].GetComponent<UnitSlot>() != null && slots[x - 1, y].GetComponent<UnitSlot>()._HData == null && slots[x - 1, y].GetComponent<UnitSlot>().EnemyOnField == null)
                    {
                        sortEnemys.Dequeue().Move();
                        yield return new WaitForSeconds(time);
                    }
                    else if (slots[x - 1, y].GetComponent<UnitSlot>() != null && slots[x - 1, y].GetComponent<UnitSlot>()._HData != null)
                    {
                        Debug.Log("Zahl");
                        //Vector2 vector2 = Vector2.zero;
                        sortEnemys.Dequeue().Attack();
                        yield return new WaitForSeconds(time);
                    }
                    else
                    {
                    
                        sortEnemys.Dequeue();   

                    }
                }
                else
                {
                    if (GameField.Instance.EnemyList[i] == sortEnemys.Peek() && sortEnemys.Peek().nextPosition.x <= 0)
                    {
                        int x = (int)sortEnemys.Peek().nextPosition.x;
                        int y = (int)sortEnemys.Peek().nextPosition.y;
                        EnemyData edata = sortEnemys.Peek();

                        sortEnemys.Dequeue();
                        edata.DestroyEnemy();
                        GameField.Instance.EnemyList.RemoveAt(i);
                        GameField.Instance.Slots[x, y].GetComponent<UnitSlot>().EnemyOnField = null;
                        yield return new WaitForSeconds(time);
                    }
                }

            }


            SetState("Unit");

        }
        private IEnumerator _UnitsAttack()
        {
            float time = 0.5f;
            int count = sortHeros.Count;

            for (int i = 0; i < count; i++)
            {
                //Vector2 targetField = Vector2.zero;
                testc();
                yield return new WaitForSeconds(time);
            }

            SetState("Fight");
        }
        private void testc()
        {
            GameObject[,] slots = GameField.Instance.Slots;

            int rang = sortHeros.Peek().CurrStatBlock.Range + sortHeros.Peek().CurrStatModifier.RangeMod;
            int y = (int)sortHeros.Peek().Unit.LastSlot.field.y;
            for (int e = 0; e < rang; e++)
            {
                if (slots[2 + e, y].GetComponent<EnemyField>().EnemyOnField != null)
                {
                    //targetField = new Vector2(2 + e, y);
                    sortHeros.Dequeue().Attack();
                    return;
                }
            }
        }
    }
}
