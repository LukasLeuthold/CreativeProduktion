using UnityEngine;
using UnityEngine.UI;

namespace AutoDefense
{
    public class TextSetter : MonoBehaviour
    {
        [SerializeField] private string preFixA = "";
        [SerializeField] private string suFixA = "";
        [SerializeField] private Text textA;
        [SerializeField] private string preFixB = "";
        [SerializeField] private string suFixB = "";
        [SerializeField] private Text textB;

        public void SetText(int _number)
        {
            string finalText = preFixA + _number.ToString() + suFixA;
            textA.text = finalText;
        }
        public void SetText(string _string)
        {
            string finalText = preFixA + _string + suFixA;
            textA.text = finalText;
        }
        public void SetText(int _numberA,int _numberB)
        {
            string finalTextA = preFixA + _numberA.ToString() + suFixA;
            textA.text = finalTextA;
            string finalTextB = preFixB + _numberB.ToString() + suFixB;
            textB.text = finalTextB;
        }

    }
}
