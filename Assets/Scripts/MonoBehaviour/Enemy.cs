using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class Enemy : MonoBehaviour
    {
        private EnemyData enemyData;
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
            //TODO: use to update enemy card values from data
            throw new NotImplementedException();
        }

        
    }
}
