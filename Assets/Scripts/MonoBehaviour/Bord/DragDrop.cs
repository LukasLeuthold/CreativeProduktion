using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AutoDefense
{
    public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
    {
        [HideInInspector]public UnitSlot LastSlot;

        public HeroData HData;
        
        [HideInInspector]public bool haveSlot;
       
        private RectTransform rectTransform;
        private Vector2 lastRectTranform;
        private CanvasGroup canvasGroup;

        
        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
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
    }
}
