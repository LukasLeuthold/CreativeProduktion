using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    public class TurnTextOff : MonoBehaviour
    {
        [SerializeField] private GameObject damageText;
        // Start is called before the first frame update

        public void _TurnTextOff()
        {
            damageText.SetActive(false);
        }
    }
}
