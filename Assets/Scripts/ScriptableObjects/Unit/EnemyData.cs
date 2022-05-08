using TMPro;
using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "new EnemyData", menuName = "Enemy/EnemyData")]

    public class EnemyData : UnitData
    {
        public Animator anim { get; set; }
        public Vector2 nextPosition;
        public RectTransform enemyTransform;
        public TMP_Text DamageText;
        private ThreatLevel enemyThreatLevel;
        public Enemy enemyPrefab;

        public ThreatLevel EnemyThreatLevel
        {
            get => enemyThreatLevel;
            set => enemyThreatLevel = value;
        }

        public override void Attack(Vector2 _targetPosition)
        {
            GameField.Instance.Slots[(int)_targetPosition.x, (int)_targetPosition.y].GetComponent<UnitSlot>().Unit.damageText.text = (CurrStatBlock.Attack + CurrStatModifier.AttackMod).ToString();

            GameField.Instance.Slots[(int)_targetPosition.x, (int)_targetPosition.y].GetComponent<UnitSlot>()._HData.Anim.Play("Damage");
            anim.Play("Attack");
            GameField.Instance.Slots[(int)_targetPosition.x, (int)_targetPosition.y].GetComponent<UnitSlot>().Unit.CurrHP -= CurrStatBlock.Attack + CurrStatModifier.AttackMod;
        }

        public override void Move()
        {
            RectTransform nextTransform = GameField.Instance.Slots[(int)nextPosition.x - 1, (int)nextPosition.y].GetComponent<RectTransform>();

            //EnemyData altem feld entziehen
            if (GameField.Instance.Slots[(int)nextPosition.x, (int)nextPosition.y].GetComponent<EnemyField>() != null)
            {
                GameField.Instance.Slots[(int)nextPosition.x, (int)nextPosition.y].GetComponent<EnemyField>().EnemyOnField = null;
                if (GameField.Instance.Slots[(int)nextPosition.x, (int)nextPosition.y].GetComponent<EnemyField>().enemyPrefab != null)
                {

                GameField.Instance.Slots[(int)nextPosition.x, (int)nextPosition.y].GetComponent<EnemyField>().enemyPrefab.lastEnemyField = null;
                }
                GameField.Instance.Slots[(int)nextPosition.x, (int)nextPosition.y].GetComponent<EnemyField>().enemyPrefab = null;

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
                GameField.Instance.Slots[(int)nextPosition.x, (int)nextPosition.y].GetComponent<EnemyField>().enemyPrefab = enemyPrefab;
                GameField.Instance.Slots[(int)nextPosition.x, (int)nextPosition.y].GetComponent<EnemyField>().enemyPrefab.lastEnemyField = GameField.Instance.Slots[(int)nextPosition.x, (int)nextPosition.y].GetComponent<EnemyField>();
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
            copy.EnemyThreatLevel = this.EnemyThreatLevel;
            return copy;
        }

        public override string ToString()
        {
            return $"Enemy {name}";
        }
    }
}
