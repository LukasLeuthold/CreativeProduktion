using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AutoDefense
{
    public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler , IPointerClickHandler
    {
        [HideInInspector]public UnitSlot LastSlot;

        
        private HeroData heroData;
        public HeroData HData
        {
            get => heroData;
            set
            {
                heroData = value;
                UpdateUnitCard();
            }
        }

        [Header("CurrStats")]
        [HideInInspector]public bool haveSlot;
        [SerializeField] private GameObject currStats;
        [SerializeField] private Text hP;
        [SerializeField] private Text speed;
        [SerializeField] private Text aT;
        
        [Header("Details")] 
        [SerializeField] private GameObject details;
        [SerializeField] private Text _HP;
        [SerializeField] private Text _Speed;
        [SerializeField] private Text _AT;
        [SerializeField] private Text _Range;
        [SerializeField] private Text _Name;
        [SerializeField] private Text _Cost;
        [SerializeField] private Image _border;

        private bool isOpen = true;
        private RectTransform rectTransform;
        private Vector2 lastRectTranform;
        private CanvasGroup canvasGroup;

        
        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            currStats.SetActive(false);
            details.SetActive(false);
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (LastSlot!= null && LastSlot.isGameField)
            {
                LastSlot._HData = null;
                LastSlot._SOGameField.HDatas[LastSlot.count] = null;
                
            }
            
            canvasGroup.alpha = 0.7f;
            canvasGroup.blocksRaycasts = false;
            GameField.Instance.isGrabing = true;
   
            lastRectTranform = rectTransform.anchoredPosition;
            transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {         
            rectTransform.anchoredPosition += eventData.delta;
        }

        public void OnDrop(PointerEventData eventData)
        {
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = 1f;

            GameField.Instance.isGrabing = false;
            canvasGroup.blocksRaycasts = true;
            if (!haveSlot)
            {
                rectTransform.anchoredPosition = lastRectTranform;               
            }
            haveSlot = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            
        }

        private void Update()
        {
            if (GameField.Instance.isGrabing)
            {
                canvasGroup.blocksRaycasts = false;
            }
            else
            {
                canvasGroup.blocksRaycasts = true;
            }

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            currStats.SetActive(true);
            transform.SetAsLastSibling();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            currStats.SetActive(false);
            details.SetActive(false);
        }

        private void UpdateUnitCard()
        {
            hP.text = heroData.CurrStatBlock.MaxHP + heroData.CurrStatModifier.MaxHPMod.ToString();
            speed.text = heroData.CurrStatBlock.Speed.ToString();
            aT.text = heroData.CurrStatBlock.Attack.ToString();

            _HP.text = heroData.CurrStatBlock.MaxHP + heroData.CurrStatModifier.MaxHPMod.ToString();
            _Speed.text = heroData.CurrStatBlock.Speed + heroData.CurrStatModifier.SpeedMod.ToString();
            _AT.text = heroData.CurrStatBlock.Attack + heroData.CurrStatModifier.AttackMod.ToString();
            _Range.text = heroData.CurrStatBlock.Range + heroData.CurrStatModifier.RangeMod.ToString();
            _Name.text = heroData.name;
            _Cost.text = heroData.Rarity.Cost.ToString();
            _border.color = heroData.Rarity.BorderColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {

                if (!isOpen)
                {
                    isOpen = true;
                    details.SetActive(false);
                    currStats.SetActive(true);
                }
                else if (isOpen)
                {
                    isOpen = false;
                    details.SetActive(true);
                    currStats.SetActive(false);
                }


            }
        }
    }
}
