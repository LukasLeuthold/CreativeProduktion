using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AutoDefense
{
    public class UnitSlot : MonoBehaviour, IDropHandler
    {
        /// <summary>Position in the Array</summary>
        public int count;
        /// <summary>All unit relevnat Datas</summary>
        public HeroData _HData;
        /// <summary>tell if this objekt is on the Unit Field</summary>
        public bool isGameField;

        /// <summary>Unit</summary>
        public DragDrop Unit;
        /// <summary>Last unit</summary>
        private DragDrop lastUnit;
        /// <summary>Posiotion in the Slots Array</summary>
        public Vector2 field;
        /// <summary>Enemy Reverenz</summary>
        public EnemyData EnemyOnField { get; set; }
        /// <summary>default values</summary>
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
            Image image = GetComponent<Image>();
            GameField.Instance.SelectetField(Color.white, 0, image);
        }
        /// <summary>Slot on Drop Event</summary>
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null && _HData != null)
            {
                //Tauschen von Units

                //Momentane Unit die getauscht wird

                //geforcte unit auf position der gedroppten unit
                Unit.GetComponent<RectTransform>().anchoredPosition = eventData.pointerDrag.GetComponent<DragDrop>().LastSlot.GetComponent<RectTransform>().anchoredPosition;

                //geforcte unit zuweisung von unitslot der gedroppten unit
                Unit.LastSlot = eventData.pointerDrag.GetComponent<DragDrop>().LastSlot;

                //geforcte unit als alte unit
                lastUnit = Unit;
                //dem alten feld der gedroppten unit wird hdata der geforcten unit zugewiesen
                Unit.LastSlot._HData = _HData;
                //gedroppte unit wird dem feld zugewiesen
                Unit = eventData.pointerDrag.GetComponent<DragDrop>();
                //dem alten feld der gedroppten uniit wird geforcte unit zugewiesen
                Unit.LastSlot.Unit = lastUnit;
                //hdata wird erneuert mit gedroppter hdata
                _HData = Unit.HData;
                

                if (isGameField)
                {
                    //gamefield wird gesetzt von diesem feld
                    if (Unit.LastSlot.isGameField)
                    {
                        //add dropped
                        Unit.HData.PlaceOnField(Unit);
                    }
                    else
                    {
                        lastUnit.HData.RemoveFromField(lastUnit);

                        //neue hdata wird platziert
                        Unit.HData.PlaceOnField(Unit);
                    }
                }
                else
                {
                    if (eventData.pointerDrag.GetComponent<DragDrop>().LastSlot.isGameField)
                    {
                        //added geforcte unit zu gamefield so
                        lastUnit.HData.PlaceOnField(lastUnit);
                    }
                }
                //verankert gedroppte unit auf diesem feld
                GetInfo(eventData);
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
                    _HData.PlaceOnField(Unit);
                }

                GetInfo(eventData);
            }

        }
        /// <summary>
        /// Löscht Daten des alten slots wenn man ein leeres Feld hinterlässt
        /// </summary>
        private void DeletData()
        {
            if (Unit.LastSlot != null && Unit.LastSlot != this)
            {
                Unit.LastSlot.Unit = null;

                Unit.LastSlot._HData = null;
            }
        }
        /// <summary>Sets Unit Valuse</summary>
        private void GetInfo(PointerEventData _eventData)
        {
            Unit.haveSlot = true;
            Unit.LastSlot = this;
            _eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }
}
