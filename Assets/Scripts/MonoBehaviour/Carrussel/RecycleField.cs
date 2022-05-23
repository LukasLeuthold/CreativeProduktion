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
        [SerializeField]private HeroCombiner heroCombiner;
        [SerializeField]private AUDIOScriptableEvent onUnitSell;

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
            heroCombiner.RemoveHeroPrefab(dd);
            HeroData hData = dd.HData;

            if (hData.CurrLevel == 1)
            {
                pRessources.PlayerMoney += hData.Rarity.Cost;
                heroPool.AddUnitCount(hData.name);
            }
            else
            {
                int timesToDo = (int)(Mathf.Pow(heroCombiner.AmountToCombine, hData.CurrLevel - 1));
                for (int i = 0; i < timesToDo; i++)
                {
                    pRessources.PlayerMoney += hData.Rarity.Cost;
                    heroPool.AddUnitCount(hData.name);
                }
            }
            Destroy(eventData.pointerDrag);
            recycleFieldImage.sprite = closedTrashSprite;
            onUnitSell.Raise();
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
