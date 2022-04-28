using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class EnemyField : MonoBehaviour
    {
        public Vector2 field;

        public EnemyData enemyData;
        public EnemyData EnemyOnField { get { return enemyData; } set { enemyData = value; } }

        public Enemy enemyPrefab;

        void Start()
        {
            GameField.Instance.Slots[(int)field.x, (int)field.y] = this.gameObject;
        }
    }
}
