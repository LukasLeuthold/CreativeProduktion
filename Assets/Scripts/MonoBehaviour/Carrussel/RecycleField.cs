using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AutoDefense
{
    /// <summary>
    /// handles selling a unit
    /// </summary>
    public class RecycleField : MonoBehaviour,IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        /// <summary>
        /// the player ressources
        /// </summary>
        [SerializeField] private PlayerRessources pRessources;
        /// <summary>
        /// the curr used heropool
        /// </summary>
        [SerializeField] private HeroPool heroPool;
        /// <summary>
        /// the used herocombiner
        /// </summary>
        [SerializeField] private HeroCombiner heroCombiner;
        /// <summary>
        /// plays when a unit is sold
        /// </summary>
        [SerializeField]private AUDIOScriptableEvent onUnitSell;

        /// <summary>
        /// image that displays the field sprites
        /// </summary>
        [Header("Icon")]
        [SerializeField] private Image recycleFieldImage;
        /// <summary>
        /// default sprite
        /// </summary>
        [SerializeField] private Sprite closedTrashSprite;
        /// <summary>
        /// sprite showing hover over with unit feedback
        /// </summary>
        [SerializeField]private Sprite openTrashSprite;
        
        /// <summary>
        /// sets default values
        /// </summary>
        private void Start()
        {
            recycleFieldImage.sprite = closedTrashSprite;
        }
        /// <summary>
        /// sells the hero; player gets money; hero gets added to pool
        /// </summary>
        /// <param name="eventData"></param>
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
        /// <summary>
        /// swaps to the hover over sprite
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                recycleFieldImage.sprite = openTrashSprite;
            }
        }
        /// <summary>
        /// swaps to the default sprite
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                recycleFieldImage.sprite = closedTrashSprite;
            }
        }
    }
}
