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
        [SerializeField]private EnemyData enemyData;
        [SerializeField]private Image enemyImage;
        [SerializeField]private Animator animator;
        public TMP_Text damageNumber;
        public EnemyField lastEnemyField;
        public int currHP;
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

        private void Start()
        {
            currHP = enemyData.CurrStatBlock.MaxHP;
        }
        private void Update()
        {
            if (currHP <= 0)
            {
                lastEnemyField.EnemyOnField = null;
                DestroyEnemy();

                for (int i = 0; i < GameField.Instance.EnemyList.Count; i++)
                {
                    if (GameField.Instance.EnemyList[i] == enemyData)
                    {
                        GameField.Instance.EnemyList.RemoveAt(i);
                    }
                }
            }
        }
        private void UpdateEnemyVisual()
        {
            enemyImage.sprite = enemyData.unitSprite;
            enemyData.enemyTransform = GetComponent<RectTransform>();
            enemyData.anim = animator;
            enemyData.DamageText = damageNumber;
            enemyData.enemyPrefab = this;
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
