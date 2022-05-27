using UnityEngine;
using UnityEngine.UI;

namespace AutoDefense
{
    /// <summary>
    /// class to set a slider to specific values
    /// </summary>
    public class SliderSetter : MonoBehaviour
    {
        /// <summary>
        /// slider to set
        /// </summary>
        [SerializeField] private Slider slider;
        /// <summary>
        /// sets the slider to a specific pair of values
        /// </summary>
        /// <param name="_valueA">value of the slider</param>
        /// <param name="_valueB">max value of the slider</param>
        public void SetSlider(int _valueA, int _valueB)
        {
            slider.maxValue = _valueB;
            slider.value = _valueA;
        }
    }
}
