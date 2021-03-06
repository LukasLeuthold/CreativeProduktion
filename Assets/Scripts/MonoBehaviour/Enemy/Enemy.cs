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
        [SerializeField] private Slider hpSlider;
        public TMP_Text damageNumber;
        public EnemyField lastEnemyField;
        [HideInInspector]public int currHP;
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
            hpSlider.maxValue = enemyData.CurrStatBlock.MaxHP;
            hpSlider.value = hpSlider.maxValue;
        }
        private void Update()
        {
            hpSlider.value = currHP;    
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
