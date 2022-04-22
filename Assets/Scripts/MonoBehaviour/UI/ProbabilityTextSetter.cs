using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoDefense
{
    public class ProbabilityTextSetter : MonoBehaviour
    {
        [SerializeField] private HeroRarity rarityCommon;
        [SerializeField] private HeroRarity rarityRare;
        [SerializeField] private HeroRarity rarityLord;
        [SerializeField] private Text commonText;
        [SerializeField] private Text rareText;
        [SerializeField] private Text lordText;

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
