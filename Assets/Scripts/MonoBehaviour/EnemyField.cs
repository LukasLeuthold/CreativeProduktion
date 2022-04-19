using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class EnemyField : MonoBehaviour
    {
        public Vector2 field;
        public EnemyData EnemyOnField { get; set; }

        void Start()
        {
            GameField.Instance.Slots[(int)field.x, (int)field.y] = this.gameObject;
        }
    }
}
