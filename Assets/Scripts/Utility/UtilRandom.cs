using System;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// simple utility class for random stuff and things
    /// </summary>
    public static class UtilRandom
    {
        /// <summary>
        /// returns a random int
        /// </summary>
        /// <returns></returns>
        public static int GetRandomInt()
        {
            System.Random rng = new System.Random(Guid.NewGuid().GetHashCode());
            return rng.Next();
        }
       /// <summary>
       /// returns a random int form range
       /// </summary>
       /// <param name="_min">minvalue</param>
       /// <param name="_max">maxvalue</param>
       /// <returns></returns>
        public static int GetRandomIntFromRange(int _min,int _max)
        {
            System.Random rng = new System.Random(Guid.NewGuid().GetHashCode());
            return rng.Next(_min,_max);
        }
        /// <summary>
        /// returns percantile success
        /// </summary>
        /// <param name="_probability">probability of success</param>
        /// <returns></returns>
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
