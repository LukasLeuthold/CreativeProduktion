using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoDefense
{
    /// <summary>
    /// manages attribute display logic
    /// </summary>
    [RequireComponent(typeof(HoverTip))]
    public class AttributeVisualManager : MonoBehaviour
    {
        /// <summary>
        /// text to display attribute name
        /// </summary>
        [SerializeField] private Text nameText;
        /// <summary>
        /// text to display diversity count
        /// </summary>
        [SerializeField] private Text diversityText;
        /// <summary>
        /// image to set the background of the attribute
        /// </summary>
        [SerializeField] private Image attributeImage;
        /// <summary>
        /// hovertip component to enable tool tip
        /// </summary>
        [SerializeField] private HoverTip toolTip;
        /// <summary>
        /// the displayed collection this attribute is from
        /// </summary>
        public HeroCollection DisplayedCollection
        {
            get;
            set;
        }
        /// <summary>
        /// text to display attribute name
        /// </summary>
        public string NameText
        {
            set
            {
                nameText.text = value;
            }
        }
        /// <summary>
        /// text to display diversity count
        /// </summary>
        public string DiversityText
        {
            set
            {
                diversityText.text = value;
            }
        }
        /// <summary>
        /// text to display the tooltip
        /// </summary>
        public string ToolTipText
        {
            set
            {
                toolTip.toolTipText = value;
            }
        }
        /// <summary>
        /// background sprite of the attribute
        /// </summary>
        public Sprite Attributesprite
        {
            set
            {
                attributeImage.sprite = value;
                attributeImage.SetNativeSize();
            }
        }

    }
}
