using System.Collections.Generic;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// class to handle unitcollection logic for applying effects on to groups and tracking group count
    /// </summary>
    [CreateAssetMenu(fileName = "new HeroCollection", menuName = "ScriptableCollection/HeroCollection", order = 1)]
    public class HeroCollection : InitScriptObject
    {
        //TODO: list for testing purpose only used for visualizing the dictionary
        public List<HeroData> heroessssTest;
        //
        /// <summary>
        /// dictionary of unique heroes and the amount of them on the battlefield
        /// </summary>
        Dictionary<string, List<HeroData>> heroesInCollection;
        /// <summary>
        /// number of unique units on the battlefield
        /// </summary>
        [SerializeField] private int diversity;

        public GroupEffect[] groupEffects;

        /// <summary>
        /// adds a herodata to the collection
        /// </summary>
        /// <param name="_hero">herodata to add</param>
        public void AddToCollection(HeroData _hero)
        {
            heroessssTest.Add(_hero);
            if (heroesInCollection.ContainsKey(_hero.name))
            {
                heroesInCollection[_hero.name].Add(_hero);
            }
            else
            {
                heroesInCollection.Add(_hero.name, new List<HeroData>());
                heroesInCollection[_hero.name].Add(_hero);
                diversity++;
            }
            Debug.Log("after adding 1 " + this.name + " the diversity is: " + diversity);
        }
        /// <summary>
        /// removes a herodata from the collection
        /// </summary>
        /// <param name="_hero">herodata to remove</param>
        public void RemoveFromCollection(HeroData _hero)
        {
            heroessssTest.Remove(_hero);
            heroesInCollection[_hero.name].Remove(_hero);
            if (heroesInCollection[_hero.name].Count <= 0)
            {
                heroesInCollection.Remove(_hero.name);
                diversity--;
            }
            Debug.Log("after subtracting 1 " + this.name + " the diversity is " + diversity);
        }

        public override void Initialize()
        {
            heroesInCollection = new Dictionary<string, List<HeroData>>();
            diversity = 0;
            heroessssTest = new List<HeroData>();
        }
    }

    [System.Serializable]
    public struct GroupEffect
    {
        public int neededDiversity;
        public Effect effect;
    }
}
