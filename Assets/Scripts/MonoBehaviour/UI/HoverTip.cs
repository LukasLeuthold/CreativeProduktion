using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AutoDefense
{
    /// <summary>
    /// handles tooltip logic when to show and when to hide
    /// </summary>
    public class HoverTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        /// <summary>
        /// scriptable float value
        /// </summary>
        [SerializeField] private ScriptableFloatValue timeToShow;
        /// <summary>
        /// the horizontal allignment of the tooltip box
        /// </summary>
        [SerializeField] private ToolTipHorizontalAllignment horizontalAllignment;
        /// <summary>
        /// the vertical allignment of the tooltip box
        /// </summary>
        [SerializeField] private ToolTipVerticalAllignment verticalAllignment;
        /// <summary>
        /// text of the tooltip
        /// </summary>
        [SerializeField, TextArea] public string toolTipText;
        /// <summary>
        /// flag if it is an attribute box or normal tooltip
        /// </summary>
        private bool isAttributeToolTip = false;

        /// <summary>
        /// starts coroutine hovertime
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            StopAllCoroutines();
            StartCoroutine(HoverTime(timeToShow.Value));

        }
        /// <summary>
        /// ends all coroutines and hides tooltip
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            StopAllCoroutines();
            HoverTipManager.OnMouseExit();
            if (isAttributeToolTip)
            {
                AttributeBox.OnTurnOffHighlight(GetComponent<AttributeVisualManager>().DisplayedCollection);
            }
        }
        /// <summary>
        /// waits for some time and shows tooltip
        /// </summary>
        /// <param name="_time"></param>
        /// <returns></returns>
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

        /// <summary>
        /// sets the tooltip from the attribute if it is an attribute tooltip
        /// </summary>
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
