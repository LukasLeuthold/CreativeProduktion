using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    [CreateAssetMenu(fileName = "new GameField", menuName = "Manager/GameField", order = 1)]
    public class SOGameField : ScriptableObject
    {
        public HeroData[] HDatas = new HeroData[5];      
    }
}
