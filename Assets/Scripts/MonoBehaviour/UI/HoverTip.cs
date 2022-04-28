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
        private bool isAttributeToolTip = false;

        public void OnPointerEnter(PointerEventData eventData)
        {
            StopAllCoroutines();
            StartCoroutine(HoverTime());

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            StopAllCoroutines();
            HoverTipManager.OnMouseExit();
            if (isAttributeToolTip)
            {
                AttributeBox.OnTurnOffHighlight(GetComponent<AttributeVisualManager>().DisplayedCollection);
            }
        }

        IEnumerator HoverTime()
        {
            yield return new WaitForSeconds(timeToShow.Value);

            if (!string.IsNullOrWhiteSpace(toolTipText))
            {
                HoverTipManager.OnMouseEnter(toolTipText, Input.mousePosition, horizontalAllignment, verticalAllignment);
            }
            if (isAttributeToolTip)
            {
                AttributeBox.OnTurnOnHighlight(GetComponent<AttributeVisualManager>().DisplayedCollection);
            }
        }

        private void OnEnable()
        {
            if (TryGetComponent<AttributeVisualManager>(out AttributeVisualManager aVM))
            {
                isAttributeToolTip = true;
            }
            else
            {

                isAttributeToolTip = false;
            }
        }
    }
}
