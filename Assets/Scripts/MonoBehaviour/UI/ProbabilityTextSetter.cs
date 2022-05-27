using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoDefense
{
    /// <summary>
    /// displays probability values
    /// </summary>
    public class ProbabilityTextSetter : MonoBehaviour
    {
        /// <summary>
        /// common hero rarity
        /// </summary>
        [SerializeField] private HeroRarity rarityCommon;
        /// <summary>
        /// rare hero rarity
        /// </summary>
        [SerializeField] private HeroRarity rarityRare;
        /// <summary>
        /// lord hero rarity
        /// </summary>
        [SerializeField] private HeroRarity rarityLord;
        /// <summary>
        /// text for common value
        /// </summary>
        [SerializeField] private Text commonText;
        /// <summary>
        /// text for rare value
        /// </summary>
        [SerializeField] private Text rareText;
        /// <summary>
        /// text for lord value
        /// </summary>
        [SerializeField] private Text lordText;
        /// <summary>
        /// sets the text after the probability values
        /// </summary>
        /// <param name="_probabilities">probability to display</param>
        public void SetProbabilityText(ProbabilityDistribution _probabilities)
        {
            commonText.color = rarityCommon.BorderColor;
            commonText.text = _probabilities.probabilityCommon.ToString() + "%";

            rareText.color = rarityRare.BorderColor;
            rareText.text = _probabilities.probabilityRare.ToString() + "%";

            lordText.color = rarityLord.BorderColor;
            lordText.text = _probabilities.probabilityLord.ToString() + "%";
        }
    }
}
