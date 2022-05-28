using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AutoDefense
{
    public class Enemy : MonoBehaviour
    {
        /// <summary>All Enemy relevnat Data</summary>
        [SerializeField]private EnemyData enemyData;
        /// <summary>Enemy Image</summary>
        [SerializeField]private Image enemyImage;
        /// <summary>Enemy Animator</summary>
        [SerializeField]private Animator animator;
        /// <summary>HP bar</summary>
        [SerializeField] private Slider hpSlider;
        /// <summary>Enemy HP text</summary>
        [SerializeField] private Text hP;
        /// <summary>Enemy Attack text</summary>
        [SerializeField] private Text attack;


        /// <summary>Damage Number when Enemy gets Damage</summary>
        public TMP_Text damageNumber;
        /// <summary> Last Field from the Enemy</summary>
        public EnemyField lastEnemyField;
        /// <summary>Enemy current HP</summary>
        [HideInInspector]public int currHP;
        /// <summary>All Enemy relevant Data</summary>
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

        /// <summary>default values</summary>
        private void Start()
        {
            currHP = enemyData.CurrStatBlock.MaxHP;
            hpSlider.maxValue = enemyData.CurrStatBlock.MaxHP;
            hpSlider.value = hpSlider.maxValue;
        }
        /// <summary>default values</summary>
        private void Update()
        {
            hP.text = currHP.ToString();
            attack.text = (enemyData.CurrStatBlock.Attack + enemyData.CurrStatModifier.AttackMod).ToString();
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
        /// <summary>Updates Enemy Visuals</summary>
        private void UpdateEnemyVisual()
        {
            enemyImage.sprite = enemyData.unitSprite;
            enemyData.enemyTransform = GetComponent<RectTransform>();
            enemyData.anim = animator;
            enemyData.DamageText = damageNumber;
            enemyData.enemyPrefab = this;
        }

        /// <summary> Turns Text Off</summary>
        public void TurnTextOff()
        {
            damageNumber.gameObject.SetActive(false);
        }
        /// <summary>Destroyes Enemy</summary>
        public void DestroyEnemy()
        {
            Destroy(this.gameObject);
        }


    }
}
