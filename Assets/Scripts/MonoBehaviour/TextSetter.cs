using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoDefense
{
    public class TextSetter : MonoBehaviour
    {
        [SerializeField]private Text text;

        public void SetText(int _number)
        {
            text.text = _number.ToString();
        }
        public void SetText(string _string)
        {
            text.text = _string;
        }

    }
}
