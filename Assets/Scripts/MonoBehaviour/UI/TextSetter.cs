using UnityEngine;
using UnityEngine.UI;

namespace AutoDefense
{
    /// <summary>
    /// component that sets a text field with specific values
    /// </summary>
    public class TextSetter : MonoBehaviour
    {
        /// <summary>
        /// prefix of text a
        /// </summary>
        [SerializeField] private string preFixA = "";
        /// <summary>
        /// sufix of text a
        /// </summary>
        [SerializeField] private string suFixA = "";
        /// <summary>
        /// text a
        /// </summary>
        [SerializeField] private Text textA;
        /// <summary>
        /// prefix of text b
        /// </summary>
        [SerializeField] private string preFixB = "";
        /// <summary>
        /// sufix of text b
        /// </summary>
        [SerializeField] private string suFixB = "";
        /// <summary>
        /// text b
        /// </summary>
        [SerializeField] private Text textB;

        /// <summary>
        /// sets the text from integer input
        /// </summary>
        /// <param name="_number">input integer</param>
        public void SetText(int _number)
        {
            string finalText = preFixA + _number.ToString() + suFixA;
            textA.text = finalText;
        }
        /// <summary>
        /// sets the text from string input
        /// </summary>
        /// <param name="_string">string input</param>
        public void SetText(string _string)
        {
            string finalText = preFixA + _string + suFixA;
            textA.text = finalText;
        }
        /// <summary>
        /// sets both texts from two number inputs
        /// </summary>
        /// <param name="_numberA">input integer</param>
        /// <param name="_numberB">input integer</param>
        public void SetText(int _numberA,int _numberB)
        {
            string finalTextA = preFixA + _numberA.ToString() + suFixA;
            textA.text = finalTextA;
            string finalTextB = preFixB + _numberB.ToString() + suFixB;
            textB.text = finalTextB;
        }
    }
}
