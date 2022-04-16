using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AutoDefense
{
    public class RecycleField : MonoBehaviour,IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]private PlayerRessources pRessources;
        [SerializeField]private HeroPool heroPool;

        [Header("Bin Icon")]
        [SerializeField]private Image recycleFieldImage;
        [SerializeField]private Sprite closedTrashSprite;
        [SerializeField]private Sprite openTrashSprite;
        

        private void Start()
        {
            recycleFieldImage.sprite = closedTrashSprite;
        }

        public void OnDrop(PointerEventData eventData)
        {
            DragDrop dd = eventData.pointerDrag.GetComponent<DragDrop>();
            dd.LastSlot._HData = null;
            dd.LastSlot.Unit = null;
            dd.haveSlot = true;
            GameField.Instance.isGrabing = false;

            HeroData hData = dd.HData;
            pRessources.PlayerMoney += hData.Rarity.Cost;
            heroPool.AddUnitCount(hData.name);
            Destroy(eventData.pointerDrag);
            recycleFieldImage.sprite = closedTrashSprite;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                recycleFieldImage.sprite = openTrashSprite;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                recycleFieldImage.sprite = closedTrashSprite;
            }
        }
    }
}
