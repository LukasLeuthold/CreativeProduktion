using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoDefense
{
    public class SliderSetter : MonoBehaviour
    {
        [SerializeField] private Slider slider;

       public void SetSlider(int _valueA,int _valueB)
        {
            slider.maxValue = _valueB;
            slider.value = _valueA;
        }
    }
}
