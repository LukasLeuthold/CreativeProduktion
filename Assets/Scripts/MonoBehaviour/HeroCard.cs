using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoDefense
{
    public class HeroCard : MonoBehaviour
    {
        [Header("Data")]
        private HeroData heroData;
        public HeroData HeroData
        {
            get => heroData;
            set
            {
                heroData = value;
                UpdateUnitCard();
            }
        }
        [Header("UI")]
        [SerializeField] private Image heroImage; 
        [SerializeField] private Image Border; 
        [SerializeField]private Text nameText;
        [SerializeField]private Text allianceText;
        [SerializeField]private Text classText;
        [SerializeField]private Text costText;
        

        private void UpdateUnitCard()
        {
            if (heroData == null)
            {
                return;
            }
            heroImage.sprite = heroData.unitSprite;
            nameText.text = heroData.name;
            Border.color = heroData.Rarity.BorderColor;
            allianceText.text = heroData.AllianceName;
            classText.text = heroData.ClassName;
            costText.text = heroData.Rarity.Cost.ToString();
        }

    }
}
