//Made by David
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// Simple Class to store info about hero rarity
    /// </summary>
    [CreateAssetMenu(fileName = "new HeroRarity", menuName = "HeroRarity")]
    public class HeroRarity : ScriptableObject
    {
        /// <summary>
        /// color of the unitcard border
        /// </summary>
        [SerializeField] private Color borderColor;
        /// <summary>
        /// prize of the unit in the shop
        /// </summary>
        [SerializeField] private int cost;
        /// <summary>
        /// prize of the unit in the shop
        /// </summary>
        public int Cost { get => cost; }
        /// <summary>
        /// color of the unitcard border
        /// </summary>
        public Color BorderColor { get => borderColor; }
    }
}
