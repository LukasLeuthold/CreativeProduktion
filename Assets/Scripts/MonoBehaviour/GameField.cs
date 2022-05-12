using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class GameField : MonoBehaviour
    {
        public static GameField Instance;

        //public EnemyData[] Enemys = new EnemyData[1];
        public List<EnemyData> EnemyList = new List<EnemyData>();
        public GameObject[,] Slots = new GameObject[10, 3];
       
        [HideInInspector]public GameObject[] Reserve = new GameObject[9];
        
        [HideInInspector]public bool isGrabing;

        //public SOGameField sOGameField;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            //for (int i = 0; i < 6; i++)
            //{
            //    sOGameField.HDatas[i] = null;
            //}           
        }

    }
}
