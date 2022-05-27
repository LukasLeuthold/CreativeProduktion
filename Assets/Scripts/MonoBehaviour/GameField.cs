using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoDefense
{
    public class GameField : MonoBehaviour
    {
        /// <summary>Singolton</summary>
        public static GameField Instance;

        /// <summary>List all Enemys on the Board</summary>
        public List<EnemyData> EnemyList = new List<EnemyData>();
        
        /// <summary>All Fields on the Board</summary>
        public GameObject[,] Slots = new GameObject[10, 3];
        
        /// <summary>All Fileds in the Reserve</summary>
        [HideInInspector] public GameObject[] Reserve = new GameObject[9];

        /// <summary>true when a Unit gets Draged</summary>
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

        /// <summary>
        /// Change Color of a Field on the Map
        /// </summary>
        /// <param name="color">Witch Color</param>
        /// <param name="alpha">alpha of the Color</param>
        /// <param name="image"> witch Field</param>
        public void SelectetField(Color color, float alpha, Image image)
        {
            var tempColor = image.color;
            tempColor = color;
            tempColor.a = alpha;
            image.color = tempColor;
        }
    }
}
