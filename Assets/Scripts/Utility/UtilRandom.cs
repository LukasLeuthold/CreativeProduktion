using System;
using UnityEngine;

namespace AutoDefense
{
    public static class UtilRandom
    {
        public static int GetRandomInt()
        {
            System.Random rng = new System.Random(Guid.NewGuid().GetHashCode());
            return rng.Next();
        }
        public static int GetRandomIntFromRange(int _min,int _max)
        {
            System.Random rng = new System.Random(Guid.NewGuid().GetHashCode());
            return rng.Next(_min,_max);
        }
        public static bool GetPercentageSuccess(int _probability)
        {
            _probability = Mathf.Clamp(_probability, 0, 100);
            if (GetRandomIntFromRange(0,100)<_probability)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
