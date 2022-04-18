using System;
using UnityEngine;

namespace AutoDefense
{
    public abstract class UnitData : ScriptableObject, ITickable
    {
        public Sprite unitSprite;
        [SerializeField] private StatBlock currStatBlock;
        [SerializeField] private ModifierBlock currStatModifier;
        public event Action OnUnitDataChanged;
        public event Action OnTurnStart;
        public event Action OnMoving;
        public event Action OnAttack;
        public event Action OnOnKill;
        public event Action OnTurnEnd;

        public StatBlock CurrStatBlock { get => currStatBlock; protected set => currStatBlock = value; }
        public ModifierBlock CurrStatModifier { get => currStatModifier; set => currStatModifier = value; }
        private void OnValidate()
        {
            OnUnitDataChanged?.Invoke();
        }
        protected virtual void Attack() { }
        protected virtual void Move() { }

        public abstract void Tick();
    }

    [System.Serializable]
    public struct StatBlock
    {
        [SerializeField] private int maxHP;
        [SerializeField] private int currHP;
        [SerializeField] private int speed;
        [SerializeField] private int attack;
        [SerializeField] private int range;
        [SerializeField] private int pierce;
        [SerializeField] private int amountAttackActions;
        [SerializeField] private int amountMovementActions;
        public int MaxHP { get => maxHP; }
        public int CurrHP
        {
            get => CurrHP;
            set
            {
                if (currHP + value >= maxHP)
                {
                    currHP = maxHP;
                    return;
                }
                if (currHP + value <= 0)
                {
                    currHP = 0;
                    return;
                }
                currHP = value;
            }
        }
        public int Speed { get => speed; }
        public int Attack { get => attack; }
        public int Pierce { get => pierce; }
        public int Range { get => range; }
        public int AmountAttackActions { get => amountAttackActions; }
        public int AmountMovementActions { get => amountMovementActions; }

        public static StatBlock Copy(StatBlock _original)
        {
            StatBlock copy = new StatBlock();
            copy.amountAttackActions = _original.amountAttackActions;
            copy.amountMovementActions = _original.amountMovementActions;
            copy.maxHP = _original.maxHP;
            copy.currHP = _original.currHP;
            copy.speed = _original.speed;
            copy.attack = _original.attack;
            copy.range = _original.range;
            return copy;
        }
    }
    [System.Serializable]
    public struct ModifierBlock
    {
        [SerializeField] private int amountAttackActionsMod;
        [SerializeField] private int amountMovementActionsMod;
        [SerializeField] private int maxHPMod;
        [SerializeField] private int speedMod;
        [SerializeField] private int attackMod;
        [SerializeField] private int pierceMod;
        [SerializeField] private int rangeMod;
        public int AmountAttackActionsMod { get => amountAttackActionsMod; set => amountAttackActionsMod = value; }
        public int AmountMovementActionsMod { get => amountMovementActionsMod; set => amountMovementActionsMod = value; }
        public int MaxHPMod { get => maxHPMod; set => maxHPMod = value; }
        public int SpeedMod { get => speedMod; set => speedMod = value; }
        public int AttackMod { get => attackMod; set => attackMod = value; }
        public int PierceMod { get => pierceMod; set => pierceMod = value; }
        public int RangeMod { get => rangeMod; set => rangeMod = value; }

        public static ModifierBlock Copy(ModifierBlock __original)
        {
            ModifierBlock copy = new ModifierBlock();
            copy.amountAttackActionsMod = __original.amountAttackActionsMod;
            copy.amountMovementActionsMod = __original.amountMovementActionsMod;
            copy.maxHPMod = __original.maxHPMod;
            copy.speedMod = __original.speedMod;
            copy.attackMod = __original.attackMod;
            copy.rangeMod = __original.rangeMod;
            return copy;
        }
        public static ModifierBlock operator +(ModifierBlock a, ModifierBlock b)
        {
            ModifierBlock newMod = new ModifierBlock();
            newMod.AmountAttackActionsMod = a.AmountAttackActionsMod + b.AmountAttackActionsMod;
            newMod.AmountMovementActionsMod = a.AmountMovementActionsMod + b.AmountMovementActionsMod;
            newMod.MaxHPMod = a.MaxHPMod + b.MaxHPMod;
            newMod.SpeedMod = a.SpeedMod + b.SpeedMod;
            newMod.AttackMod = a.AttackMod + b.AttackMod;
            newMod.PierceMod = a.PierceMod + b.PierceMod;
            newMod.RangeMod = a.RangeMod + b.RangeMod;
            return newMod;
        }

        public static ModifierBlock operator -(ModifierBlock a, ModifierBlock b)
        {
            ModifierBlock newMod = new ModifierBlock();
            newMod.AmountAttackActionsMod = a.AmountAttackActionsMod - b.AmountAttackActionsMod;
            newMod.AmountMovementActionsMod = a.AmountMovementActionsMod - b.AmountMovementActionsMod;
            newMod.MaxHPMod = a.MaxHPMod - b.MaxHPMod;
            newMod.SpeedMod = a.SpeedMod - b.SpeedMod;
            newMod.AttackMod = a.AttackMod - b.AttackMod;
            newMod.PierceMod = a.PierceMod - b.PierceMod;
            newMod.RangeMod = a.RangeMod - b.RangeMod;
            return newMod;
        }
    }
}
