using TMPro;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// class to handle enemy data
    /// </summary>
    [CreateAssetMenu(fileName = "new EnemyData", menuName = "Enemy/EnemyData")]
    public class EnemyData : UnitData
    {
        /// <summary>
        /// animator of the enemy
        /// </summary>
        public Animator anim { get; set; }
        /// <summary>
        /// position the enemy moves to
        /// </summary>
        public Vector2 nextPosition;
        /// <summary>
        /// the transform of the enemy prefab
        /// </summary>
        public RectTransform enemyTransform;
        /// <summary>
        /// the text to display damage numbers
        /// </summary>
        public TMP_Text DamageText;
        /// <summary>
        /// the threatlevel the enemy is part of
        /// </summary>
        private ThreatLevel enemyThreatLevel;
        /// <summary>
        /// the prefab of the enemy
        /// </summary>
        public Enemy enemyPrefab;
        /// <summary>
        /// the threatlevel the enemy is part of
        /// </summary>
        public ThreatLevel EnemyThreatLevel
        {
            get => enemyThreatLevel;
            set => enemyThreatLevel = value;
        }
        /// <summary>
        /// attacks the targetposition
        /// </summary>
        /// <param name="_targetPosition">target position</param>
        public override void Attack(Vector2 _targetPosition)
        {
            GameField.Instance.Slots[(int)_targetPosition.x, (int)_targetPosition.y].GetComponent<UnitSlot>().Unit.damageText.text = (CurrStatBlock.Attack + CurrStatModifier.AttackMod).ToString();

            GameField.Instance.Slots[(int)_targetPosition.x, (int)_targetPosition.y].GetComponent<UnitSlot>()._HData.Anim.Play("Damage");
            anim.Play("Attack");
            GameField.Instance.Slots[(int)_targetPosition.x, (int)_targetPosition.y].GetComponent<UnitSlot>().Unit.CurrHP -= CurrStatBlock.Attack + CurrStatModifier.AttackMod;
        }
        /// <summary>
        /// moves the enemy
        /// </summary>
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
        /// <summary>
        /// destroys the enemy prefab
        /// </summary>
        public void DestroyEnemy()
        {
            enemyTransform.GetComponent<Enemy>().DestroyEnemy();
        }
        /// <summary>
        /// returns a copy of the enemy data object
        /// </summary>
        /// <returns></returns>
        public EnemyData GetCopy()
        {
            EnemyData copy = Object.Instantiate(this);
            copy.name = this.name;
            copy.EnemyThreatLevel = this.EnemyThreatLevel;
            return copy;
        }
        /// <summary>
        /// to string method of the enemy data class
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Enemy {name}";
        }
    }
}
