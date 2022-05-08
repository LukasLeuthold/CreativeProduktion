using System;
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
        [SerializeField] internal int breakTime;

        public EnemySpawner enemySpawner;

        internal float currTime;

        public GameObject SkipButton;
        public GameObject BlockImage;

        [SerializeField] private PlayerRessources pRessources;

        [SerializeField] private SOGameField _Field;
        Queue<HeroData> sortHeros = new Queue<HeroData>();
        Queue<EnemyData> sortEnemys = new Queue<EnemyData>();

        [Header("Events")]
        [SerializeField] private INTScriptableEvent onWaveEnd;
        [SerializeField] private VOIDScriptableEvent onEditStart;
        [SerializeField] private VOIDScriptableEvent onEditEnd;

        [Header("EventEffects")]
        [SerializeField] private EventEffektCollection onHeroTurnStartEffects;

        void Start()
        {
            _TickStates = new Dictionary<string, TickState>()
        {
            {"Start",new StartState(this)},
            {"Edit", new EditState(this)},
            {"Fight", new FightState(this)},
            {"Break", new BreakState(this) },
            {"Unit", new UnitFightState(this)},
            {"Enemy", new EnemyFightState(this)}
        };
            state = _TickStates["Start"];

        }

        void Update()
        {
            state.HandleState();
        }

        public void SkipEdit()
        {
            currTime = editTime;
            timeSlider.value = editTime;
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

        internal void ResetSlider(int maxTime)
        {
            currTime = 0;
            timeSlider.value = 0;
            timeSlider.maxValue = maxTime;
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
                SetState("Break");
            }
        }
        private IEnumerator _UnitsAttack()
        {
            float time = 0.5f;
            int count = sortHeros.Count;

            for (int i = 0; i < count; i++)
            {
                if (!sortHeros.Peek().Unit.isDead && (int)sortHeros.Peek().Unit.LastSlot.field.x == 0 && !sortHeros.Peek().isMele)
                {
                    for (int e = 0; e < sortHeros.Peek().CurrStatBlock.AmountAttackActions; e++)
                    {
                        _Attack();
                        yield return new WaitForSeconds(time);
                    }
                    sortHeros.Dequeue();
                }
                else if (!sortHeros.Peek().Unit.isDead && (int)sortHeros.Peek().Unit.LastSlot.field.x == 1)
                {
                    for (int e = 0; e < sortHeros.Peek().CurrStatBlock.AmountAttackActions; e++)
                    {
                        _Attack();
                        yield return new WaitForSeconds(time);
                    }
                    sortHeros.Dequeue();
                }
                else
                {
                    sortHeros.Dequeue();
                }
            }
            SetState("Break");
        }
        private void _Attack()
        {
            GameObject[,] slots = GameField.Instance.Slots;

            int rang = sortHeros.Peek().CurrStatBlock.Range + sortHeros.Peek().CurrStatModifier.RangeMod;
            int y = (int)sortHeros.Peek().Unit.LastSlot.field.y;
            for (int e = 0; e < rang; e++)
            {
                if (slots[2 + e, y].GetComponent<EnemyField>().EnemyOnField != null)
                {
                    Vector2 targetField;
                    targetField = new Vector2(3 + e, y);

                    if (sortHeros.Peek().CurrStatModifier.CanDoPierceDamage && slots[(int)targetField.x, (int)targetField.y].GetComponent<EnemyField>().EnemyOnField != null)
                    {

                        sortHeros.Peek().Attack(targetField);
                    }
                    targetField = new Vector2(2 + e, y);

                    sortHeros.Peek().Attack(targetField);
                    return;
                }
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
            float time = 0.5f;
            int count = sortEnemys.Count;

            for (int i = 0; i < count; i++)
            {
                int x = (int)sortEnemys.Peek().nextPosition.x;
                int y = (int)sortEnemys.Peek().nextPosition.y;
                GameObject[,] slots = GameField.Instance.Slots;
                if (sortEnemys.Count > 0 && sortEnemys.Peek().nextPosition.x > 2)
                {
                    if (slots[x - 1, y].GetComponent<EnemyField>() != null && slots[x - 1, y].GetComponent<EnemyField>().EnemyOnField == null
                        && sortEnemys.Peek().nextPosition.x > 2)
                    {
                        sortEnemys.Dequeue().Move();
                        yield return new WaitForSeconds(0.2f);
                    }
                    else if (slots[x - 1, y].GetComponent<UnitSlot>() != null && slots[x - 1, y].GetComponent<UnitSlot>()._HData == null
                        && slots[x - 1, y].GetComponent<UnitSlot>().EnemyOnField == null && sortEnemys.Peek().nextPosition.x > 2)
                    {
                        sortEnemys.Dequeue().Move();
                        yield return new WaitForSeconds(0.2f);
                    }
                    else
                    {
                        sortEnemys.Dequeue();
                    }
                }
                else if (sortEnemys.Peek().nextPosition.x == 2 && slots[x - 1, y].GetComponent<UnitSlot>() != null
                    && ((slots[x - 1, y].GetComponent<UnitSlot>()._HData != null && !slots[x - 1, y].GetComponent<UnitSlot>().Unit.isDead)
                    || (slots[x - 2, y].GetComponent<UnitSlot>()._HData != null && !slots[x - 2, y].GetComponent<UnitSlot>().Unit.isDead)))
                {
                    Vector2 targetPosition = Vector2.zero;

                    if (slots[x - 1, y].GetComponent<UnitSlot>().Unit != null && !slots[x - 1, y].GetComponent<UnitSlot>().Unit.isDead)
                    {
                        targetPosition = slots[x - 1, y].GetComponent<UnitSlot>().field;
                        sortEnemys.Dequeue().Attack(targetPosition);
                        yield return new WaitForSeconds(time);
                    }
                    else
                    {
                        targetPosition = slots[x - 2, y].GetComponent<UnitSlot>().field;
                        sortEnemys.Dequeue().Attack(targetPosition);
                        yield return new WaitForSeconds(time);
                    }

                }
                else
                {
                    for (int e = 0; e < GameField.Instance.EnemyList.Count(); e++)
                    {
                        if (
                            GameField.Instance.EnemyList[e] == sortEnemys.Peek() &&
                            (slots[x - 1, y].GetComponent<UnitSlot>()._HData == null ||
                            slots[x - 1, y].GetComponent<UnitSlot>().Unit.isDead) &&
                            (slots[x - 2, y].GetComponent<UnitSlot>()._HData == null ||
                            slots[x - 2, y].GetComponent<UnitSlot>().Unit.isDead))
                        {
                            EnemyData edata = sortEnemys.Peek();

                            sortEnemys.Dequeue();
                            pRessources.PlayerHealth -= edata.EnemyThreatLevel.PlayerDamage;
                            edata.DestroyEnemy();
                            GameField.Instance.EnemyList.RemoveAt(e);
                            GameField.Instance.Slots[x, y].GetComponent<EnemyField>().EnemyOnField = null;

                            yield return new WaitForSeconds(time);
                        }
                    }
                }

            }

            SetState("Unit");
        }

        public void CallOnWaveEnd()
        {
            onWaveEnd.Raise(LevelI.CurrWave);
        }
        public void CallOnEditStart()
        {
            onEditStart.Raise();
        }
        public void CallOnEditEnd()
        {
            onEditEnd.Raise();
        }
        internal void CallOnHeroTurnStartEffects()
        {
            onHeroTurnStartEffects.ActivateEffects();
        }

    }
}
