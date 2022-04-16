using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "Cards", menuName = "Manager/Cards", order = 1)]
    public class SOCards : ScriptableObject
    {
        public HeroCard[] Cards = new HeroCard[5];
        //TODO: do we need this??
    }
}
