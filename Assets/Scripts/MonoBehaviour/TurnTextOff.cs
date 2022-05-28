using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AutoDefense
{
    public class TurnTextOff : MonoBehaviour
    {
        /// <summary>
        /// Damage Text on the Enemy
        /// </summary>
        [SerializeField] private GameObject damageText;

        /// <summary>
        /// Turns Damage Text off
        /// </summary>
        public void _TurnTextOff()
        {
            damageText.SetActive(false);
        }
    }
}
