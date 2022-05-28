using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AutoDefense
{
    /// <summary>
    /// display of a single hero; can be interacted with to buy the hero
    /// </summary>
    public class HeroCard : MonoBehaviour, IPointerDownHandler
    {
        /// <summary>
        /// herodata of the card
        /// </summary>
        [Header("Data")]
        private HeroData heroData;
        /// <summary>
        /// ressources of the player
        /// </summary>
        [SerializeField] private PlayerRessources playerRessources;
        /// <summary>
        /// herocombiner used to handle logic for combining and leveling up heroes
        /// </summary>
        [SerializeField] private HeroCombiner heroCombiner;
        /// <summary>
        /// flag if the hero is affordable by the player
        /// </summary>
        private bool isAffordable = false;
        /// <summary>
        /// the unit parent object heroprefabs get parented to
        /// </summary>
        [SerializeField] GameObject unitParent;
        /// <summary>
        /// the heroprefab
        /// </summary>
        [SerializeField] private GameObject heroPrefab;

        /// <summary>
        /// the whole card
        /// </summary>
        [Header("UI")]
        [SerializeField] public GameObject card;
        /// <summary>
        /// the image displaying the heroes look
        /// </summary>
        [SerializeField] private Image heroImage;
        /// <summary>
        /// the border of the card; gets colored in rarity color
        /// </summary>
        [SerializeField] private Image Border;
        /// <summary>
        /// text to display the name of the hero
        /// </summary>
        [SerializeField] private Text nameText;
        /// <summary>
        /// text to display the alliance of the hero
        /// </summary>
        [SerializeField] private Text allianceText;
        /// <summary>
        /// text to display the class of the hero
        /// </summary>
        [SerializeField] private Text classText;
        /// <summary>
        /// text to deisplay the cost of the hero
        /// </summary>
        [SerializeField] private Text costText;

        /// <summary>
        /// herodata of the card
        /// </summary>
        public HeroData HeroData
        {
            get => heroData;
            set
            {
                heroData = value;
                SetAffordable(playerRessources.PlayerMoney);
            }
        }

        /// <summary>
        /// refreshes all displays of the card
        /// </summary>
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
        /// <summary>
        /// evaluates if the hero is affordable by the player or not
        /// </summary>
        /// <param name="_value"></param>
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
        /// <summary>
        /// buys the unit and puts it on the reserve of the player
        /// </summary>
        /// <param name="eventData"></param>
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
