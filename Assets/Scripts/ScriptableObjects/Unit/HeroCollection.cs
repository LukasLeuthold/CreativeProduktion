using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// class to handle unitcollection logic for applying effects on to groups and tracking group count
    /// </summary>
    [CreateAssetMenu(fileName = "new HeroCollection", menuName = "ScriptableCollection/HeroCollection", order = 1)]
    public class HeroCollection : ScriptableObject
    {
        //TODO: list for testing purpose only used for visualizing the dictionary
        public List<HeroData> heroessssTest;
        //
        /// <summary>
        /// dictionary of unique heroes and the amount of them on the battlefield
        /// </summary>
        Dictionary<HeroData, int> heroesInCollection = new Dictionary<HeroData, int>();
        /// <summary>
        /// number of unique units on the battlefield
        /// </summary>
        [SerializeField] private int diversity;

        public void ClearCollection()
        {
            diversity = 0;
            heroesInCollection = new Dictionary<HeroData, int>();
            heroessssTest = new List<HeroData>();
        }
        /// <summary>
        /// adds a herodata to the collection
        /// </summary>
        /// <param name="_hero">herodata to add</param>
        public void AddToCollection(HeroData _hero)
        {
            heroessssTest.Add(_hero);
            //if (heroesInCollection.ContainsKey(_hero))
            //{
            //    heroesInCollection[_hero]++;
            //}
            //else
            //{
            //    heroesInCollection.Add(_hero, 1);
            //    diversity++;
            //}
            //Debug.Log("after adding 1 " + this.name + " the amount is " + diversity);
        }
        /// <summary>
        /// removes a herodata from the collection
        /// </summary>
        /// <param name="_hero">herodata to remove</param>
        public void RemoveFromCollection(HeroData _hero)
        {
                heroessssTest.Remove(_hero);
            //if (heroesInCollection.ContainsKey(_hero))
            //{
            //    heroesInCollection[_hero]--;
            //    if (heroesInCollection[_hero] == 0)
            //    {
            //        heroesInCollection.Remove(_hero);
            //        diversity--;
            //    }
            //    Debug.Log("after subtracting 1 " + this.name + " the amount is " + diversity);
            //}
        }
    }
}
