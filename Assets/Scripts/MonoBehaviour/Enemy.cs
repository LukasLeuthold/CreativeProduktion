using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutoDefense
{
    public class Enemy : MonoBehaviour
    {
        private EnemyData enemyData;
        [SerializeField]private Image enemyImage;
        [SerializeField]private Animator animator;
        public TMP_Text damageNumber;
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
            enemyData.DamageText = damageNumber;
            //TODO: use to update enemy card values from data
        }

        public void TurnTextOff()
        {
            damageNumber.gameObject.SetActive(false);
        }
        public void DestroyEnemy()
        {
            Destroy(this.gameObject);
        }


    }
}
