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
        /// <summary>Dictionary of all states</summary>
        public Dictionary<string, TickState> _TickStates;
        /// <summary>state</summary>
        private TickState state;
        /// <summary>Levelinfo</summary>
        [SerializeField] internal LevelInfo LevelI;
        /// <summary>Current State Te´xt</summary>
        [SerializeField] internal TMP_Text currStateText;

        /// <summary>Time Slider</summary>
        [SerializeField] internal Slider timeSlider;
        /// <summary>Edit Time</summary>
        [SerializeField] internal int editTime;
        /// <summary>Break time</summary>
        [SerializeField] internal int breakTime;
        /// <summary>enemy Spawner</summary>
        public EnemySpawner enemySpawner;
        /// <summary>Time on the Slider</summary>
        internal float currTime;
        /// <summary>Skip</summary>
        public Button SkipButton;
        /// <summary>Image Block Reserve</summary>
        public GameObject BlockImage;
        /// <summary></summary>
        [HideInInspector]public bool SkipBreak;
        
        [Header("Audio Events")]
        /// <summary>Audio Event</summary>
        [SerializeField] internal AUDIOScriptableEvent onWaveStart;
        [Header("Wave")]
        /// <summary>Current Wave</summary>
        [SerializeField] private Text currWave;
        /// <summary>max Wave</summary>
        [SerializeField] private Text maxWave;
        /// <summary>Player Ressources</summary>
        [SerializeField] private PlayerRessources pRessources;
        /// <summary>Active hero Collection</summary>
        [SerializeField] private HeroCollection activeHeroes;
        /// <summary>all Units on Field</summary>
        Queue<HeroData> sortHeros = new Queue<HeroData>();
        /// <summary>All Enemys on Field</summary>
        Queue<EnemyData> sortEnemys = new Queue<EnemyData>();

        [Header("Events")]
        /// <summary>On Wave End Event</summary>
        [SerializeField] private INTScriptableEvent onWaveEnd;
        /// <summary>On Edit Time Start Event</summary>
        [SerializeField] private VOIDScriptableEvent onEditStart;
        /// <summary>On Edit Time End Event</summary>
        [SerializeField] private VOIDScriptableEvent onEditEnd;

        [Header("EventEffects")]
        /// <summary>One Hero Turn Start Event</summary>
        [SerializeField] private EventEffektCollection onHeroTurnStartEffects;
        /// <summary>OnWave over Event</summary>
        [SerializeField] private EventEffektCollection onWaveOverEffects;

        /// <summary>default values</summary>
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
            maxWave.text = LevelI.MaxWaveCount.ToString();

        }
        /// <summary>default values</summary>
        void Update()
        {
            state.HandleState();
            currWave.text = LevelI.CurrWave.ToString();
        }
        /// <summary>Skips The Edit Time</summary>
        public void SkipEdit()
        {
            currTime = editTime;
            timeSlider.value = editTime;
        }
        /// <summary>
        /// transition into the next State
        /// </summary>
        /// <param name="stateName">new State</param>
        internal void SetState(string stateName)
        {
            if (state != null)
            {
                state.ExitState();
            }
            state = _TickStates[stateName];

            state.EnterState();
        }
        /// <summary>Reset Slider in new State</summary>
        internal void ResetSlider(int maxTime)
        {
            currTime = 0;
            timeSlider.value = 0;
            timeSlider.maxValue = maxTime;
        }
        /// <summary>
        /// Sorts units by speed
        /// </summary>
        internal void UnitAttack()
        {
            HeroData[] hDatas = activeHeroes.ToArray();
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
            else if (SkipBreak)
            {
                SetState("Fight");
            }
            else
            {
                SetState("Break");
            }
        }
        /// <summary>
        /// Attack Logig
        /// </summary>
        private IEnumerator _UnitsAttack()
        {
            float time = 0.5f;
            int count = sortHeros.Count;

            for (int i = 0; i < count; i++)
            {
                if (!sortHeros.Peek().Unit.isDead && (int)sortHeros.Peek().Unit.LastSlot.field.x == 0 && !sortHeros.Peek().isMele)
                {
                    int attackAmount = sortHeros.Peek().CurrStatBlock.AmountAttackActions + sortHeros.Peek().CurrStatModifier.AmountAttackActionsMod;
                    for (int e = 0; e < attackAmount; e++)
                    {
                        _Attack();
                        yield return new WaitForSeconds(time);
                    }
                    sortHeros.Dequeue();
                }
                else if (!sortHeros.Peek().Unit.isDead && (int)sortHeros.Peek().Unit.LastSlot.field.x == 1)
                {
                    int attackAmount = sortHeros.Peek().CurrStatBlock.AmountAttackActions + sortHeros.Peek().CurrStatModifier.AmountAttackActionsMod;
                    for (int e = 0; e < attackAmount; e++)
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

            if (enemySpawner.EnemiesInWave.Count <= 0 && GameField.Instance.EnemyList.Count <= 0)
            {
                CallOnWaveEnd();
                SetState("Edit");
            }
            else if (SkipBreak)
            {
                SetState("Fight");
            }
            else
            {

                SetState("Break");
            }
        }
        /// <summary>
        /// Unit Attack
        /// </summary>
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
        /// <summary>
        ///sorts Enemys by speed
        /// </summary>
        internal void SortEnemy()
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
            StartCoroutine(_EnemyAttackOrMove());
        }
        /// <summary>
        /// Enemy Attack or Move logig
        /// </summary>
        private IEnumerator _EnemyAttackOrMove()
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
                            edata.DestroyEnemy();
                            GameField.Instance.EnemyList.RemoveAt(e);
                            GameField.Instance.Slots[x, y].GetComponent<EnemyField>().EnemyOnField = null;

                            pRessources.PlayerHealth -= edata.EnemyThreatLevel.PlayerDamage;
                            break;
                        }
                    }
                    yield return new WaitForSeconds(time);
                }

            }

            SetState("Unit");
        }

        /// <summary>
        /// Events
        /// </summary>
        public void CallOnWaveEnd()
        {
            onWaveEnd.Raise(LevelI.CurrWave);
            CallOnWaveOverEffects();
        }
        /// <summary>
        /// Events
        /// </summary>
        public void CallOnEditStart()
        {
            onEditStart.Raise();
        }
        /// <summary>
        /// Events
        /// </summary>
        public void CallOnEditEnd()
        {
            onEditEnd.Raise();
        }
        /// <summary>
        /// Events
        /// </summary>
        internal void CallOnHeroTurnStartEffects()
        {
            onHeroTurnStartEffects.ActivateEffects();
        }
        /// <summary>
        /// Events
        /// </summary>
        internal void CallOnWaveOverEffects()
        {
            onWaveOverEffects.ActivateEffects();
        }
        /// <summary>
        /// Break Skip
        /// </summary>
        /// <param name="newValue">true fals</param>
        public void Toggle_Change(bool newValue)
        {
            SkipBreak = !SkipBreak;
        }
    }
}
