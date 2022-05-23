using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoDefense
{
    public class GameField : MonoBehaviour
    {
        public static GameField Instance;


        public List<EnemyData> EnemyList = new List<EnemyData>();
        public GameObject[,] Slots = new GameObject[10, 3];

        [HideInInspector] public GameObject[] Reserve = new GameObject[9];

        [HideInInspector] public bool isGrabing;

    

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

  
        }


        public void SelectetField(Color color, float alpha, Image image)
        {
            var tempColor = image.color;
            tempColor = color;
            tempColor.a = alpha;
            image.color = tempColor;
        }
    }
}
