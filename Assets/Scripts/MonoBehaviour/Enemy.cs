using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoDefense
{
    public class Enemy : MonoBehaviour
    {
        private EnemyData enemyData;
        [SerializeField]private Image enemyImage;

        public EnemyData EnemyData
        {
            get
            {
                return enemyData;
            }
            set
            {
                enemyData = value;
                UpdateEnemyVisual();
            }
        }

        private void UpdateEnemyVisual()
        {
            enemyImage.sprite = enemyData.unitSprite;
            //TODO: use to update enemy card values from data
        }

        
    }
}
