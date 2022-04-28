using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AutoDefense
{
    public enum ToolTipVerticalAllignment
    {
        CENTER,
        TOP,
        BOTTOM
    }
    public enum ToolTipHorizontalAllignment
    {
        CENTER,
        LEFT,
        RIGHT
    }


    public class HoverTipManager : MonoBehaviour
    {

        [SerializeField]private  RectTransform tipPanelRect;
        [SerializeField]private  TextMeshProUGUI tipText;

        public static Action<string, Vector2,ToolTipHorizontalAllignment,ToolTipVerticalAllignment> OnMouseEnter;
        public static Action OnMouseExit;

        private void ShowTip(string _tipText,Vector2 _position, ToolTipHorizontalAllignment _hAllignment, ToolTipVerticalAllignment _vAllignment)
        {
            tipText.text = _tipText;
            tipPanelRect.sizeDelta = new Vector2(tipText.preferredWidth > 200 ? 200 : tipText.preferredWidth, tipText.preferredHeight);

            tipPanelRect.gameObject.SetActive(true);

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

        private void HideTip()
        {
            tipText.text = default;
            tipPanelRect.gameObject.SetActive(false);
        }
        private void Start()
        {
            HideTip();
        }
        private void OnEnable()
        {
            OnMouseEnter += ShowTip;
            OnMouseExit += HideTip;
        }
        private void OnDisable()
        {
            OnMouseEnter -= ShowTip;
            OnMouseExit -= HideTip;
        }

    }
}
