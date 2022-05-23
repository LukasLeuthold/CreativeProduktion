using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AutoDefense
{
    public class TurnTextOff : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject hoverOver;
        [SerializeField] private GameObject damageText;


        public void _TurnTextOff()
        {
            damageText.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
           // StopAllCoroutines();
            StopCoroutine(ShowHoverOver());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //StopAllCoroutines();
            hoverOver.SetActive(false);
        }

        IEnumerator ShowHoverOver()
        {
            yield return new WaitForSeconds(0.5f);
            hoverOver.SetActive(true);
        }
    }

}
