using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AutoDefense
{
    public class HoverTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private ScriptableFloatValue timeToShow;
        [SerializeField] private ToolTipHorizontalAllignment horizontalAllignment;
        [SerializeField] private ToolTipVerticalAllignment verticalAllignment;
        [SerializeField, TextArea] public string toolTipText;

        public void OnPointerEnter(PointerEventData eventData)
        {
            StopAllCoroutines();
            if (!string.IsNullOrWhiteSpace(toolTipText))
            {
                StartCoroutine(HoverTime());
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            StopAllCoroutines();
            HoverTipManager.OnMouseExit();
        }

        IEnumerator HoverTime()
        {
            yield return new WaitForSeconds(timeToShow.Value);

            HoverTipManager.OnMouseEnter(toolTipText, Input.mousePosition, horizontalAllignment, verticalAllignment);
        }
    }
}
