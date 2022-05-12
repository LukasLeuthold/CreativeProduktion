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
            StartCoroutine(HoverTime(timeToShow.Value));

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

        IEnumerator HoverTime(float _time)
        {
            yield return new WaitForSeconds(_time);

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
