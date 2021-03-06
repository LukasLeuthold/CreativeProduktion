using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AutoDefense
{
    public class HeroCard : MonoBehaviour, IPointerDownHandler
    {
        [Header("Data")]
        private HeroData heroData;
        public HeroData HeroData
        {
            get => heroData;
            set
            {
                heroData = value;
                SetAffordable(playerRessources.PlayerMoney);
            }
        }

        [SerializeField] private PlayerRessources playerRessources;
        [SerializeField] private HeroCombiner heroCombiner;

        private bool isAffordable = false;

        [SerializeField] GameObject unitParent;

        [SerializeField] private GameObject heroPrefab;

        [Header("UI")]
        [SerializeField] public GameObject card;
        [SerializeField] private Image heroImage;
        [SerializeField] private Image Border;
        [SerializeField] private Text nameText;
        [SerializeField] private Text allianceText;
        [SerializeField] private Text classText;
        [SerializeField] private Text costText;


        private void UpdateUnitCard()
        {
            if (heroData == null)
            {
                return;
            }
            heroImage.sprite = heroData.unitSprite;
            nameText.text = heroData.name;
            allianceText.text = heroData.AllianceName;
            classText.text = heroData.ClassName;
            costText.text = heroData.Rarity.Cost.ToString();

            if (isAffordable)
            {
                Border.color = heroData.Rarity.BorderColor;
                heroImage.color = Color.white;
            }
            else if (!isAffordable)
            {
                Border.color = Color.grey;
                Color color = Color.grey;
                color.a = .5f;
                heroImage.color = color;
            }
        }
        public void SetAffordable(int _value)
        {
            if (heroData == null)
            {
                return;
            }
            if (heroData.Rarity.Cost <= _value)
            {
                isAffordable = true;
                UpdateUnitCard();
            }
            else if (heroData.Rarity.Cost > _value)
            {
                isAffordable = false;
                UpdateUnitCard();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (!isAffordable)
                {
                    return;
                }
                for (int i = 0; i < GameField.Instance.Reserve.Length; i++)
                {
                    if (GameField.Instance.Reserve[i].GetComponent<UnitSlot>()._HData == null)
                    {
                        card.SetActive(false);
                        playerRessources.PlayerMoney -= HeroData.Rarity.Cost;
                        GameObject Hero = Instantiate(heroPrefab, unitParent.transform);


                        Hero.GetComponent<RectTransform>().anchoredPosition = GameField.Instance.Reserve[i].GetComponent<RectTransform>().anchoredPosition;
                        Hero.GetComponent<DragDrop>().HData = heroData;
                        Hero.GetComponent<DragDrop>().LastSlot = GameField.Instance.Reserve[i].GetComponent<UnitSlot>();
                        GameField.Instance.Reserve[i].GetComponent<UnitSlot>().Unit = Hero.GetComponent<DragDrop>();
                        GameField.Instance.Reserve[i].GetComponent<UnitSlot>()._HData = heroData;

                        heroCombiner.AddHeroPrefab(Hero.GetComponent<DragDrop>());

                        break;
                    }
                }
            }
        }
    }
}
