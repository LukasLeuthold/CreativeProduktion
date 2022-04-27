using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoDefense
{
    [RequireComponent(typeof(HoverTip))]
    public class AttributeVisualManager : MonoBehaviour
    {
        [SerializeField] private Text nameText;
        [SerializeField] private Text diversityText;
        [SerializeField] private HoverTip toolTip;

        public string NameText
        {
            set
            {
                nameText.text = value;
            }
        }
        public string DiversityText
        {
            set
            {
                diversityText.text = value;
            }
        }
        public string ToolTipText
        {
            set
            {
                toolTip.toolTipText = value;
            }
        }
        public Color TextColor
        {
            set
            {
                diversityText.color = value;
                nameText.color = value;
            }
        }

    }
}
