using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AutoDefense
{
    public class UnitSlot : MonoBehaviour, IDropHandler
    {
        public int count;

        public SOGameField _SOGameField;


        public HeroData _HData;

        public bool isGameField;
        
        public DragDrop Unit;
        private DragDrop lastUnit;
        public Vector2 field;
       

        private void Start()
        {
            if (isGameField)
            {
                GameField.Instance.Slots[(int)field.x, (int)field.y] = this.gameObject;
            }
            else
            {
                GameField.Instance.Reserve[count] = this.gameObject;
            }
        }
        public void OnDrop(PointerEventData eventData)
        {
             if (eventData.pointerDrag != null && _HData != null)
            {
                //Tauschen von Units

                //Momentane Unit die getauscht wird
                Unit.GetComponent<RectTransform>().anchoredPosition = eventData.pointerDrag.GetComponent<DragDrop>().LastSlot.GetComponent<RectTransform>().anchoredPosition;
                Unit.LastSlot = eventData.pointerDrag.GetComponent<DragDrop>().LastSlot;
                if (Unit.LastSlot.isGameField)
                {
                    Unit.LastSlot._SOGameField.HDatas[Unit.LastSlot.count] = Unit.HData;
                }
                
                lastUnit = Unit;
               //Unit vom DragDrop
                Unit = eventData.pointerDrag.GetComponent<DragDrop>();
                Unit.LastSlot.Unit = lastUnit;
                Unit.LastSlot._HData = _HData;
                GetInfo(eventData);
                
                _HData = Unit.HData;
                if (isGameField)
                {
                    _SOGameField.HDatas[count] = (HeroData)_HData;
                    _HData.PlaceOnField();
                }
            }
            else if (eventData.pointerDrag != null && !isGameField)
            {
                //Auf die Reserve
                Unit = eventData.pointerDrag.GetComponent<DragDrop>();
                DeletData();
                Unit.LastSlot = null;
                _HData = Unit.HData;
                GetInfo(eventData);
            }
            else if (eventData.pointerDrag != null)
            {   
                //Wenn keine Unit aufm feld stellt
                Unit = eventData.pointerDrag.GetComponent<DragDrop>();
                
                DeletData();
                
                if (isGameField)
                {
                    _HData = Unit.HData;
                    _SOGameField.HDatas[count] = _HData;
                    _HData.PlaceOnField();
                }

                GetInfo(eventData);
            }

        }
        /// <summary>
        /// Löscht Daten des alten slots wenn man ein leeres Feld hinterlässt
        /// </summary>
        private void DeletData()
        {
            if (Unit.LastSlot != null)
            {
                Unit.LastSlot.Unit = null;
                Unit.LastSlot._HData = null;
            }
        }
        private void GetInfo(PointerEventData _eventData)
        {
            Unit.haveSlot = true;
            Unit.LastSlot = this;
            _eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }
}
