using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public abstract class UnitData : ScriptableObject,ITickable
    {
        public Sprite unitSprite;
        [SerializeField]private StatBlock currStatBlock;
        [SerializeField]private ModifierBlock currStatModifier;
        public event Action OnModifierChanged;
        public event Action OnUnitDataChanged;
        public event Action OnTurnStart;
        public event Action OnMoving;
        public event Action OnAttack;
        public event Action OnOnKill;
        public event Action OnTurnEnd;

        public StatBlock CurrStatBlock { get => currStatBlock; protected set => currStatBlock = value; }
        public ModifierBlock CurrStatModifier 
        {
            get => currStatModifier;
            set
            {
                currStatModifier = value;
                OnModifierChanged?.Invoke();
            }
        }
        private void OnValidate()
        {
            OnUnitDataChanged?.Invoke();
        }
        public virtual void Attack(Vector2 enemyOnField) { }
        public virtual void Move() { }

        public abstract void Tick();
    }

    [System.Serializable]
    public struct StatBlock
    {
        [SerializeField] private int amountAttackActions ;
        [SerializeField] private int amountMovementActions ;
        [SerializeField]private int maxHP;
        [SerializeField]private int speed;
        [SerializeField]private int attack;
        [SerializeField]private int pierce;
        [SerializeField]private int range;
        public int MaxHP { get => maxHP; }
        public int Speed { get => speed; }
        public int Attack { get => attack; }
        public int Pierce { get => pierce; }
        public int Range { get => range; }
        public int AmountAttackActions { get => amountAttackActions; }
        public int AmountMovementActions { get => amountMovementActions; }

        public static StatBlock Copy(StatBlock __original)
        {
            StatBlock copy = new StatBlock();
            copy.amountAttackActions = __original.amountAttackActions;
            copy.amountMovementActions = __original.amountMovementActions;
            copy.maxHP = __original.maxHP;
            copy.speed = __original.speed;
            copy.attack = __original.attack;
            copy.range = __original.range;
            return copy;
        }
    }
    [System.Serializable]
    public struct ModifierBlock
    {
        [SerializeField] private int maxHPMod;
        [SerializeField] private int speedMod;
        [SerializeField] private int attackMod;
        [SerializeField] private int rangeMod;
        [SerializeField] private int pierceMod;
        [SerializeField] private int amountAttackActionsMod;
        [SerializeField] private int amountMovementActionsMod;
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
