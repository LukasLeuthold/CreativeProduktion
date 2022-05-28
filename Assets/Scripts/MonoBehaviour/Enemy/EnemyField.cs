using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoDefense
{
    public class EnemyField : MonoBehaviour
    {
        /// <summary>Position of the Field</summary>
        public Vector2 field;
        /// <summary>All Enemy relevant Datas</summary>
        public EnemyData enemyData;
        /// <summary>All Enemy relevant Datas</summary>
        public EnemyData EnemyOnField { get { return enemyData; } set { enemyData = value; } }
        
        /// <summary>Enemy Prefab</summary>
        public Enemy enemyPrefab;
        
        /// <summary>default values</summary>
        void Start()
        {
            GameField.Instance.Slots[(int)field.x, (int)field.y] = this.gameObject;
            Image image = GetComponent<Image>();
            GameField.Instance.SelectetField(Color.white, 0, image);
        }
    }
}
