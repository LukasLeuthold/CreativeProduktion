using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AutoDefense
{
    public class RecycleField : MonoBehaviour,IDropHandler
    {
        [SerializeField]private PlayerRessources pRessources;
        [SerializeField]private HeroPool heroPool;

        public void OnDrop(PointerEventData eventData)
        {
            DragDrop dd = eventData.pointerDrag.GetComponent<DragDrop>();
            dd.LastSlot._HData = null;
            dd.LastSlot.Unit = null;

            HeroData hData = dd.HData;
            pRessources.PlayerMoney += hData.Rarity.Cost;
            heroPool.AddUnitCount(hData.name);
            Destroy(eventData.pointerDrag);
        }

    }
}
