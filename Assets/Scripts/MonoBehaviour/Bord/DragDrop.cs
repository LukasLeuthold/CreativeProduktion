using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AutoDefense
{
    public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
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

        [HideInInspector]public bool haveSlot;

        [SerializeField] private GameObject currStats;
        [SerializeField] private Text hP;
        [SerializeField] private Text speed;
        [SerializeField] private Text aT;
       
        private RectTransform rectTransform;
        private Vector2 lastRectTranform;
        private CanvasGroup canvasGroup;

        
        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            currStats.SetActive(false);
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
        }

        private void UpdateUnitCard()
        {
            hP.text = heroData.CurrStatBlock.MaxHP.ToString();
            speed.text = heroData.CurrStatBlock.Speed.ToString();
            aT.text = heroData.CurrStatBlock.Attack.ToString();
        }
    }
}
