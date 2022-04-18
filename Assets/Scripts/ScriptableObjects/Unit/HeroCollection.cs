using System.Collections.Generic;
using System.Linq;
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

        private int lowestDiversity;
        public int Diversity
        {
            get => diversity;
            set
            {
                if (dicGroupEffect == null)
                {
                    diversity = value;
                    return;
                }
                if (diversity < value)
                {
                    if (!dicGroupEffect.ContainsKey(value))
                    {
                        diversity = value;
                        return;
                    }
                    if (currEffect != null)
                    {
                        RemoveEffectFromGroup(currEffect);
                    }
                    diversity = value;
                    currEffect = dicGroupEffect[diversity];
                    currNeededDiversity = diversity;
                    ApplyEffectToGroup(currEffect);
                }
                else if (diversity > value)
                {
                    if (currEffect == null)
                    {
                        diversity = value;
                        return;
                    }
                    if (dicGroupEffect.ContainsKey(diversity) && !dicGroupEffect.ContainsKey(value))
                    {
                        if (value < lowestDiversity)
                        {
                            RemoveEffectFromGroup(currEffect);
                            currEffect = null;
                            diversity = value;
                            return;
                        }
                        else
                        {
                            RemoveEffectFromGroup(currEffect);
                            for (int i = value; i >=0; i--)
                            {
                                if (dicGroupEffect.ContainsKey(i))
                                {
                                    currEffect = dicGroupEffect[i];
                                    currNeededDiversity = i;
                                    diversity = value;
                                    ApplyEffectToGroup(currEffect);
                                    return;
                                }
                            }
                        }
                    }
                    if (!dicGroupEffect.ContainsKey(diversity) && !dicGroupEffect.ContainsKey(value))
                    {
                        diversity = value;
                        return;
                    }
                    if (!dicGroupEffect.ContainsKey(diversity) && dicGroupEffect.ContainsKey(value))
                    {
                        diversity = value;
                        return;
                    }
                }
            }
        }

        public GroupEffect[] groupEffects;
        //private GroupEffect currEffect;
        private Effect currEffect;
        private int currNeededDiversity;
        private Dictionary<int, Effect> dicGroupEffect;

        /// <summary>
        /// adds a herodata to the collection
        /// </summary>
        /// <param name="_hero">herodata to add</param>
        public void AddToCollection(HeroData _hero)
        {
            heroessssTest.Add(_hero);
            if (currEffect != null)
            {
                //currEffect.effect.ApplyEffect(_hero);
                currEffect.ApplyEffect(_hero);
            }
            if (heroesInCollection.ContainsKey(_hero.name))
            {
                heroesInCollection[_hero.name].Add(_hero);
            }
            else
            {
                heroesInCollection.Add(_hero.name, new List<HeroData>());
                heroesInCollection[_hero.name].Add(_hero);
                Diversity++;
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
            if (currEffect != null)
            {
                currEffect.RemoveEffect(_hero);
            }
            if (heroesInCollection[_hero.name].Count <= 0)
            {
                heroesInCollection.Remove(_hero.name);
                Diversity--;
            }
            //Debug.Log("after subtracting 1 " + this.name + " the diversity is " + diversity);
        }

        public override void Initialize()
        {
            heroesInCollection = new Dictionary<string, List<HeroData>>();
            currEffect = null;
            diversity = 0;
            heroessssTest = new List<HeroData>();
            if (groupEffects.Length == 0)
            {
                return;
            }
            lowestDiversity = int.MaxValue;
            dicGroupEffect = new Dictionary<int, Effect>();
            for (int i = 0; i < groupEffects.Length; i++)
            {
                dicGroupEffect.Add(groupEffects[i].neededDiversity, groupEffects[i].effect);
                if (groupEffects[i].neededDiversity < lowestDiversity)
                {
                    lowestDiversity = groupEffects[i].neededDiversity;
                }
            }
        }

        private void ApplyEffectToGroup(Effect _effect)
        {
            Debug.Log("applying buff");
            for (int i = 0; i < heroesInCollection.Count; i++)
            {
                List<HeroData> heroes = heroesInCollection.ElementAt(i).Value;
                for (int j = 0; j < heroes.Count; j++)
                {
                    _effect.ApplyEffect(heroes[j]);
                }
            }
        }
        private void RemoveEffectFromGroup(Effect _effect)
        {
            Debug.Log("removing buff");
            for (int i = 0; i < heroesInCollection.Count; i++)
            {
                List<HeroData> heroes = heroesInCollection.ElementAt(i).Value;
                for (int j = 0; j < heroes.Count; j++)
                {
                    _effect.RemoveEffect(heroes[j]);
                }
            }
        }
    }

    [System.Serializable]
    public class GroupEffect
    {
        public int neededDiversity;
        public Effect effect;
    }
}
