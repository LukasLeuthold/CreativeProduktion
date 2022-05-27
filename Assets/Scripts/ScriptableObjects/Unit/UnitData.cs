using System;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// abstract base class for unit datas
    /// </summary>
    public abstract class UnitData : ScriptableObject
    {
        /// <summary>
        /// sprite of the unit
        /// </summary>
        public Sprite unitSprite;
        /// <summary>
        /// current statblock of the unit
        /// </summary>
        [SerializeField] private StatBlock currStatBlock;
        /// <summary>
        /// current modifierblock of the unit
        /// </summary>
        [SerializeField] private ModifierBlock currStatModifier;
        /// <summary>
        /// gets called when modifier block gets changed
        /// </summary>
        public event Action OnModifierChanged;
        /// <summary>
        /// gets called when unitdata gets changed
        /// </summary>
        public event Action OnUnitDataChanged;
        /// <summary>
        /// gets called when curr statblock gets changed
        /// </summary>
        public event Action OnCurrStatBlockChanged;

        /// <summary>
        /// current statblock of the unit
        /// </summary>
        public StatBlock CurrStatBlock
        {
            get => currStatBlock;
            protected set
            {
                currStatBlock = value;
                OnCurrStatBlockChanged?.Invoke();
            }
        }
        /// <summary>
        /// current modifierblock of the unit
        /// </summary>
        public ModifierBlock CurrStatModifier
        {
            get => currStatModifier;
            set
            {
                currStatModifier = value;
                OnModifierChanged?.Invoke();
            }
        }
        /// <summary>
        /// calls on unitdata changed on validate
        /// </summary>
        private void OnValidate()
        {
            OnUnitDataChanged?.Invoke();
        }
        /// <summary>
        /// attacks the target position
        /// </summary>
        /// <param name="targetPosition">target position</param>
        public virtual void Attack(Vector2 targetPosition) { }
        /// <summary>
        /// moves the unit
        /// </summary>
        public virtual void Move() { }
    }

    /// <summary>
    /// class to store unit stats
    /// </summary>
    [System.Serializable]
    public class StatBlock
    {
        /// <summary>
        /// amount of attack actions the unit has
        /// </summary>
        [SerializeField] private int amountAttackActions;
        /// <summary>
        /// amount of movement actions the unit has
        /// </summary>
        [SerializeField] private int amountMovementActions;
        /// <summary>
        /// maximum hp of the unit
        /// </summary>
        [SerializeField] private int maxHP;
        /// <summary>
        /// speed value of the unit
        /// </summary>
        [SerializeField] private int speed;
        /// <summary>
        /// attack value of the unit
        /// </summary>
        [SerializeField] private int attack;
        /// <summary>
        /// range value of the unit
        /// </summary>
        [SerializeField] private int range;

        /// <summary>
        /// maximum hp of the unit
        /// </summary>
        public int MaxHP { get => maxHP; }
        /// <summary>
        /// speed value of the unit
        /// </summary>
        public int Speed { get => speed; }
        /// <summary>
        /// attack value of the unit
        /// </summary>
        public int Attack { get => attack; }
        /// <summary>
        /// range value of the unit
        /// </summary>
        public int Range { get => range; }
        /// <summary>
        /// amount of attack actions the unit has
        /// </summary>
        public int AmountAttackActions { get => amountAttackActions; }
        /// <summary>
        /// amount of movement actions the unit has
        /// </summary>
        public int AmountMovementActions { get => amountMovementActions; }

        /// <summary>
        /// returns a copy of an original statblock
        /// </summary>
        /// <param name="__original">statblock that gets copied</param>
        /// <returns></returns>
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

    /// <summary>
    /// simple struct to hold modifier values
    /// </summary>
    [System.Serializable]
    public struct ModifierBlock
    {
        /// <summary>
        /// maximum hp modifier of the unit
        /// </summary>
        [SerializeField] private int maxHPMod;
        /// <summary>
        /// speed modifier of the unit
        /// </summary>
        [SerializeField] private int speedMod;
        /// <summary>
        /// attack modifier of the unit
        /// </summary>
        [SerializeField] private int attackMod;
        /// <summary>
        /// range modifier of the unit
        /// </summary>
        [SerializeField] private int rangeMod;
        /// <summary>
        /// modifier amount of attack actions the unit has
        /// </summary>
        [SerializeField] private int amountAttackActionsMod;
        /// <summary>
        /// modifier amount of movement actions the unit has
        /// </summary>
        [SerializeField] private int amountMovementActionsMod;
        /// <summary>
        /// flag if unit can do pierce damage
        /// </summary>
        [SerializeField] private bool canDoPirceDamage;
        /// <summary>
        /// modifier amount of attack actions the unit has
        /// </summary>
        public int AmountAttackActionsMod { get => amountAttackActionsMod; set => amountAttackActionsMod = value; }
        /// <summary>
        /// modifier amount of movement actions the unit has
        /// </summary>
        public int AmountMovementActionsMod { get => amountMovementActionsMod; set => amountMovementActionsMod = value; }
        /// <summary>
        /// maximum hp modifier of the unit
        /// </summary>
        public int MaxHPMod { get => maxHPMod; set => maxHPMod = value; }
        /// <summary>
        /// speed modifier of the unit
        /// </summary>
        public int SpeedMod { get => speedMod; set => speedMod = value; }
        /// <summary>
        /// attack modifier of the unit
        /// </summary>
        public int AttackMod { get => attackMod; set => attackMod = value; }
        /// <summary>
        /// range modifier of the unit
        /// </summary>
        public int RangeMod { get => rangeMod; set => rangeMod = value; }
        /// <summary>
        /// flag if unit can do pierce damage
        /// </summary>
        public bool CanDoPierceDamage { get => canDoPirceDamage; set => canDoPirceDamage = value; }
        /// <summary>
        /// returns a copy of original modifier block
        /// </summary>
        /// <param name="__original">modifierblock that gets copied</param>
        /// <returns></returns>
        public static ModifierBlock Copy(ModifierBlock __original)
        {
            ModifierBlock copy = new ModifierBlock();
            copy.amountAttackActionsMod = __original.amountAttackActionsMod;
            copy.amountMovementActionsMod = __original.amountMovementActionsMod;
            copy.maxHPMod = __original.maxHPMod;
            copy.speedMod = __original.speedMod;
            copy.attackMod = __original.attackMod;
            copy.rangeMod = __original.rangeMod;
            copy.canDoPirceDamage = __original.canDoPirceDamage;
            
            return copy;
        }
        /// <summary>
        /// adds a modifierblock to another
        /// </summary>
        /// <param name="a">modifier block 1</param>
        /// <param name="b">modifier block 2</param>
        /// <returns></returns>
        public static ModifierBlock operator +(ModifierBlock a, ModifierBlock b)
        {
            ModifierBlock newMod = new ModifierBlock();
            newMod.AmountAttackActionsMod = a.AmountAttackActionsMod + b.AmountAttackActionsMod;
            newMod.AmountMovementActionsMod = a.AmountMovementActionsMod + b.AmountMovementActionsMod;
            newMod.MaxHPMod = a.MaxHPMod + b.MaxHPMod;
            newMod.SpeedMod = a.SpeedMod + b.SpeedMod;
            newMod.AttackMod = a.AttackMod + b.AttackMod;
            newMod.RangeMod = a.RangeMod + b.RangeMod;
            newMod.CanDoPierceDamage = b.CanDoPierceDamage;
            return newMod;
        }
        /// <summary>
        /// subtracts a modifierblock from another
        /// </summary>
        /// <param name="a">modifier block 1</param>
        /// <param name="b">modifier block 2</param>
        /// <returns></returns>
        public static ModifierBlock operator -(ModifierBlock a, ModifierBlock b)
        {
            ModifierBlock newMod = new ModifierBlock();
            newMod.AmountAttackActionsMod = a.AmountAttackActionsMod - b.AmountAttackActionsMod;
            newMod.AmountMovementActionsMod = a.AmountMovementActionsMod - b.AmountMovementActionsMod;
            newMod.MaxHPMod = a.MaxHPMod - b.MaxHPMod;
            newMod.SpeedMod = a.SpeedMod - b.SpeedMod;
            newMod.AttackMod = a.AttackMod - b.AttackMod;
            newMod.RangeMod = a.RangeMod - b.RangeMod;
            newMod.CanDoPierceDamage = !b.CanDoPierceDamage;
            return newMod;
        }
    }
}
