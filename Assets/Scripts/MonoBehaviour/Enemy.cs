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
        [SerializeField]private Animator animator;

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
            enemyData.enemyTransform = GetComponent<RectTransform>();
            enemyData.anim = animator;
            //TODO: use to update enemy card values from data
        }

        public void DestroyEnemy()
        {
            Destroy(this.gameObject);
        }


    }
}
