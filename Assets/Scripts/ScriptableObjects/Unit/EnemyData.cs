using TMPro;
using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "new EnemyData", menuName = "ScriptableUnitData/EnemyData", order = 2)]

    public class EnemyData : UnitData
    {
        public Animator anim { get; set; }
        public Vector2 nextPosition;
        public RectTransform enemyTransform;
        public TMP_Text DamageText;
        public override void Attack()
        {
            anim.Play("Attack");

        }

        public override void Move()
        {
            RectTransform nextTransform = GameField.Instance.Slots[(int)nextPosition.x - 1, (int)nextPosition.y].GetComponent<RectTransform>();

            //EnemyData altem feld entziehen
            if (GameField.Instance.Slots[(int)nextPosition.x, (int)nextPosition.y].GetComponent<EnemyField>() != null)
            {
                GameField.Instance.Slots[(int)nextPosition.x, (int)nextPosition.y].GetComponent<EnemyField>().EnemyOnField = null;
            }
            else
            {
                GameField.Instance.Slots[(int)nextPosition.x, (int)nextPosition.y].GetComponent<UnitSlot>().EnemyOnField = null;
            }

            // Zuweisung der nächsten Position
            if (GameField.Instance.Slots[(int)nextPosition.x - 1, (int)nextPosition.y].GetComponent<EnemyField>() == null)
            {
                if (GameField.Instance.Slots[(int)nextPosition.x - 1, (int)nextPosition.y].GetComponent<UnitSlot>().field != null)
                {
                    nextPosition = GameField.Instance.Slots[(int)nextPosition.x - 1, (int)nextPosition.y].GetComponent<UnitSlot>().field;
                }
            }
            else
            {
                GameField.Instance.Slots[(int)nextPosition.x, (int)nextPosition.y].GetComponent<EnemyField>().EnemyOnField = null;
                nextPosition = GameField.Instance.Slots[(int)nextPosition.x - 1, (int)nextPosition.y].GetComponent<EnemyField>().field;
            }

            //EnemyData neuem Feld hinzufügen
            if (GameField.Instance.Slots[(int)nextPosition.x, (int)nextPosition.y].GetComponent<EnemyField>() != null)
            {
                GameField.Instance.Slots[(int)nextPosition.x, (int)nextPosition.y].GetComponent<EnemyField>().EnemyOnField = this;
            }
            else
            {
                GameField.Instance.Slots[(int)nextPosition.x, (int)nextPosition.y].GetComponent<UnitSlot>().EnemyOnField = this;
            }

            //Bewegen auf die nächste Position
            if (GameField.Instance.Slots[(int)nextPosition.x, (int)nextPosition.y].GetComponent<UnitSlot>() == null)
            {
                enemyTransform.anchoredPosition = nextTransform.anchoredPosition;
            }
            else
            {
                if (GameField.Instance.Slots[(int)nextPosition.x, (int)nextPosition.y].GetComponent<UnitSlot>()._HData == null)
                {
                    enemyTransform.anchoredPosition = nextTransform.anchoredPosition;
                }
            }
        }

        public void DestroyEnemy()
        {

            enemyTransform.GetComponent<Enemy>().DestroyEnemy();
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
                
            }
        }

        public override string ToString()
        {
            return $"Enemy {name}";
        }
    }
}
