using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "new EnemyData", menuName = "ScriptableUnitData/EnemyData", order = 2)]
    public class EnemyData : UnitData
    {
        protected override void Attack()
        {
            throw new System.NotImplementedException();
        }

        protected override void Move()
        {
            base.Move();
        }

        public EnemyData GetCopy()
        {
            EnemyData copy = Object.Instantiate(this);
            copy.name = this.name;
            return copy;
        }

        public override void Tick()
        {
            bool CanMove = false;
            if (CanMove)
            {
                for (int i = 0; i < (CurrStatBlock.AmountMovementActions + CurrStatModifier.AmountMovementActionsMod); i++)
                {
                    Move();
                }
            }
            for (int i = 0; i < (CurrStatBlock.AmountAttackActions + CurrStatModifier.AmountAttackActionsMod); i++)
            {
                Attack();
            }
        }

        public override string ToString()
        {
            return $"Enemy {name}";
        }
    }
}
