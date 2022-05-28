using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AutoDefense
{
    /// <summary>
    /// the vertical allignment of the tooltip box
    /// </summary>
    public enum ToolTipVerticalAllignment
    {
        CENTER,
        TOP,
        BOTTOM
    }
    /// <summary>
    /// the horizontal allignment of the tooltip box
    /// </summary>
    public enum ToolTipHorizontalAllignment
    {
        CENTER,
        LEFT,
        RIGHT
    }

    /// <summary>
    /// manages the tooltip logic
    /// </summary>
    public class HoverTipManager : MonoBehaviour
    {
        /// <summary>
        /// transform of the tooltip window
        /// </summary>
        [SerializeField] private RectTransform tipPanelRect;
        /// <summary>
        /// text of the tooltip
        /// </summary>
        [SerializeField]private  TextMeshProUGUI tipText;
        /// <summary>
        /// gets called when the mouse enters a field which has a tooltip
        /// </summary>
        public static Action<string, Vector2, ToolTipHorizontalAllignment, ToolTipVerticalAllignment> OnMouseEnter;
        /// <summary>
        /// gets called when the mouse leaves a field which has a tooltip
        /// </summary>
        public static Action OnMouseExit;
        /// <summary>
        /// shows the tooltip on the right position
        /// </summary>
        /// <param name="_tipText">text of the tooltip</param>
        /// <param name="_position">the position the box appears</param>
        /// <param name="_hAllignment">the horizontal allignment of the tooltip box</param>
        /// <param name="_vAllignment">the vertical allignment of the tooltip box</param>
        private void ShowTip(string _tipText,Vector2 _position, ToolTipHorizontalAllignment _hAllignment, ToolTipVerticalAllignment _vAllignment)
        {
            tipText.text = _tipText;
            tipPanelRect.gameObject.SetActive(true);
            tipPanelRect.sizeDelta = new Vector2(tipText.preferredWidth > 200 ? 200 : tipText.preferredWidth, tipText.preferredHeight);


            Vector2 positionVector = Vector2.zero;
            switch (_hAllignment)
            {
                case ToolTipHorizontalAllignment.CENTER:
                    positionVector.x = _position.x;
                    break;
                case ToolTipHorizontalAllignment.LEFT:
                    positionVector.x = _position.x - (tipPanelRect.sizeDelta.x * 0.5f);
                    break;
                case ToolTipHorizontalAllignment.RIGHT:
                    positionVector.x = _position.x + (tipPanelRect.sizeDelta.x * 0.5f);
                    break;
            }
            switch (_vAllignment)
            {
                case ToolTipVerticalAllignment.CENTER:
                    positionVector.y = _position.y;
                    break;
                case ToolTipVerticalAllignment.TOP:
                    positionVector.y = _position.y + (tipPanelRect.sizeDelta.y * 0.5f);
                    break;
                case ToolTipVerticalAllignment.BOTTOM:
                    positionVector.y = _position.y - (tipPanelRect.sizeDelta.y * 0.5f);
                    break;
            }
            tipPanelRect.transform.position = positionVector;
        }
        /// <summary>
        /// hides the tooltip
        /// </summary>
        private void HideTip()
        {
            tipText.text = default;
            tipPanelRect.gameObject.SetActive(false);
        }
        /// <summary>
        /// sets default values
        /// </summary>
        private void Start()
        {
            HideTip();
        }
        /// <summary>
        /// subscribes to actions
        /// </summary>
        private void OnEnable()
        {
            OnMouseEnter += ShowTip;
            OnMouseExit += HideTip;
        }
        /// <summary>
        /// unsubscribes from actions
        /// </summary>
        private void OnDisable()
        {
            OnMouseEnter -= ShowTip;
            OnMouseExit -= HideTip;
        }

    }
}
