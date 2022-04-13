using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AutoDefense
{
    public class HeroCard : MonoBehaviour , IPointerDownHandler
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
        
        [SerializeField]public GameObject card;

        [SerializeField] GameObject unitParent;

        [SerializeField] private GameObject heroPrefab;
        
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

        public void OnPointerDown(PointerEventData eventData)
        {
            
            for (int i = 0; i < GameField.Instance.Reserve.Length; i++)
            {
                if (GameField.Instance.Reserve[i].GetComponent<UnitSlot>()._HData == null)
                {
                    card.SetActive(false);
                    
                    GameObject Hero = Instantiate(heroPrefab,unitParent.transform);

                    
                    Hero.GetComponent<RectTransform>().anchoredPosition = GameField.Instance.Reserve[i].GetComponent<RectTransform>().anchoredPosition;
                    Hero.GetComponent<DragDrop>().HData = heroData;
                    Hero.GetComponent<DragDrop>().LastSlot = GameField.Instance.Reserve[i].GetComponent<UnitSlot>();
                    GameField.Instance.Reserve[i].GetComponent<UnitSlot>().Unit = Hero.GetComponent<DragDrop>();
                    GameField.Instance.Reserve[i].GetComponent<UnitSlot>()._HData = heroData;

                    break;
                }
            }
        }
    }
}
