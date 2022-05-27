using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoDefense
{
    /// <summary>
    /// class to handle unitcollection logic for applying effects on to groups and tracking group count
    /// </summary>
    [CreateAssetMenu(fileName = "new HeroCollection", menuName = "Hero/HeroCollection")]
    public class HeroCollection : InitScriptObject
    {
        /// <summary>
        /// dictionary of unique heroes and the amount of them on the battlefield
        /// </summary>
        Dictionary<string, List<HeroData>> heroesInCollection;
        /// <summary>
        /// list of hero prefabs
        /// </summary>
        private List<DragDrop> heroDragDrops;
        /// <summary>
        /// array of effects of the collection
        /// </summary>
        public GroupEffect[] groupEffects;
        /// <summary>
        /// the currently active effect
        /// </summary>
        private GroupEffect currActiveEffect;
        /// <summary>
        /// dictionary of effects of the collection with needed diversity as key
        /// </summary>
        private Dictionary<int, GroupEffect> dicGroupEffect;
        /// <summary>
        /// color the highlight border of this collection has
        /// </summary>
        [SerializeField] private Color highlightColor;

        /// <summary>
        /// gets called when the first unit of the collection is placed
        /// </summary>
        public event Action<HeroCollection> OnFirstUnitPlaced;
        /// <summary>
        /// gets called when a parameter of the collection is changed
        /// </summary>
        public event Action<HeroCollection> OnCollectionChanged;
        /// <summary>
        /// gets called when the last unit of the colleciton is removed from play
        /// </summary>
        public event Action<HeroCollection> OnLastUnitRemoved;

        /// <summary>
        /// gets called when a unit is added to play
        /// </summary>
        public event Action<HeroData> OnAddedToCollection;
        /// <summary>
        /// gets called when a unit is removed from play
        /// </summary>
        public event Action<HeroData> OnRemovedFromCollection;
        /// <summary>
        /// the amount of unique heroes in play
        /// </summary>
        [SerializeField] private int diversity;
        /// <summary>
        /// gets called when a buff is activated
        /// </summary>
        [SerializeField] private AUDIOScriptableEvent OnBuffActivated;
        /// <summary>
        /// gets caled when a buff is deactivated
        /// </summary>
        [SerializeField] private AUDIOScriptableEvent OnBuffDeactivated;
        /// <summary>
        /// lowest diversity of all effects of the collection
        /// </summary>
        private int lowestDiversity;
        /// <summary>
        /// the currently active effect
        /// </summary>
        public GroupEffect CurrActiveEffect
        {
            get
            {
                return currActiveEffect;
            }
            set
            {
                if (currActiveEffect != null)
                {
                    currActiveEffect.effect.RemoveEffectFromGroup(this);
                }
              
                currActiveEffect = value;
                if (currActiveEffect != null)
                {
                    currActiveEffect.effect.ApplyEffectToGroup(this);
                    OnBuffActivated?.Raise();
                }
                else
                {
                    OnBuffDeactivated?.Raise();
                }
            }
        }
        /// <summary>
        /// the amount of unique heroes in play
        /// </summary>
        public int Diversity
        {
            get => diversity;
            set
            {
                if (dicGroupEffect == null)
                {
                    diversity = value;
                    OnCollectionChanged?.Invoke(this);
                    return;
                }
                //adding herotype
                if (diversity < value)
                {
                    if (!dicGroupEffect.ContainsKey(value))
                    {
                        diversity = value;
                        OnCollectionChanged?.Invoke(this);
                        return;
                    }
                    //if (currActiveEffect != null)
                    //{
                    //    currActiveEffect.effect.RemoveEffectFromGroup(this);
                    //}
                    diversity = value;
                    CurrActiveEffect = dicGroupEffect[diversity];
                    OnCollectionChanged?.Invoke(this);
                }
                //removing herotype
                else if (diversity > value)
                {
                    if (currActiveEffect == null)
                    {
                        diversity = value;
                        OnCollectionChanged?.Invoke(this);
                        return;
                    }
                    if (dicGroupEffect.ContainsKey(diversity) && !dicGroupEffect.ContainsKey(value))
                    {
                        if (value < lowestDiversity)
                        {
                            CurrActiveEffect = null;
                            diversity = value;
                            OnCollectionChanged?.Invoke(this);
                            return;
                        }
                        else
                        {
                            for (int i = value; i >= 0; i--)
                            {
                                if (dicGroupEffect.ContainsKey(i))
                                {
                                    CurrActiveEffect = dicGroupEffect[i];
                                    diversity = value;
                                    OnCollectionChanged?.Invoke(this);
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        diversity = value;
                        if (dicGroupEffect.ContainsKey(value))
                        {
                            CurrActiveEffect = dicGroupEffect[diversity];
                        }
                        OnCollectionChanged?.Invoke(this);
                        return;
                    }
                }
            }
        }
        /// <summary>
        /// dictionary of unique heroes and the amount of them on the battlefield
        /// </summary>
        public Dictionary<string, List<HeroData>> HeroesInCollection
        {
            get { return heroesInCollection; }
            set { heroesInCollection = value; }
        }




        /// <summary>
        /// adds a herodata to the collection
        /// </summary>
        /// <param name="_hero">herodata to add</param>
        public void AddToCollection(HeroData _hero, DragDrop _heroDrag)
        {
            OnAddedToCollection?.Invoke(_hero);
            if (Diversity == 0)
            {
                OnFirstUnitPlaced?.Invoke(this);
            }
            heroDragDrops.Add(_heroDrag);
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

        }
        /// <summary>
        /// removes a herodata from the collection
        /// </summary>
        /// <param name="_hero">herodata to remove</param>
        public void RemoveFromCollection(HeroData _hero, DragDrop _heroDrag)
        {
            OnRemovedFromCollection?.Invoke(_hero);
            heroDragDrops.Remove(_heroDrag);
            heroesInCollection[_hero.name].Remove(_hero);
            if (heroesInCollection[_hero.name].Count <= 0)
            {
                heroesInCollection.Remove(_hero.name);
                Diversity--;
            }
            if (Diversity == 0)
            {
                OnLastUnitRemoved?.Invoke(this);
            }
        }
        /// <summary>
        /// returns the currrent tooltip
        /// </summary>
        /// <returns></returns>
        public string GetToolTip()
        {
            string toolTip = this.name + "\n";
            for (int i = 0; i < groupEffects.Length; i++)
            {

                if (currActiveEffect != null && currActiveEffect.neededDiversity == groupEffects[i].neededDiversity)
                {
                    toolTip += "<b>" + "{" + "<u>" + groupEffects[i].neededDiversity.ToString() + "</u>" + "} " + "</b>";
                }
                else
                {
                    toolTip += "{" + groupEffects[i].neededDiversity.ToString() + "} ";
                }
            }
            toolTip += "\n";
            toolTip += "\n";
            if (currActiveEffect != null)
            {
                toolTip += currActiveEffect.effectToolTip;
            }
            else
            {
                toolTip += "No active effect";
            }
            return toolTip;
        }
        /// <summary>
        /// turns on highlights for all heroes in play of the collection
        /// </summary>
        public void TurnOnHighlights()
        {
            for (int i = 0; i < heroDragDrops.Count; i++)
            {
                heroDragDrops[i].ToggleHighlight(true, highlightColor);
            }
        }
        /// <summary>
        /// turns off highlights for all heroes in play of the collection
        /// </summary>
        public void TurnOffHighlights()
        {
            for (int i = 0; i < heroDragDrops.Count; i++)
            {
                heroDragDrops[i].ToggleHighlight(false, highlightColor);
            }
        }
        /// <summary>
        /// initializes collection reseting all values and lists
        /// </summary>
        public override void Initialize()
        {
            heroesInCollection = new Dictionary<string, List<HeroData>>();
            heroDragDrops = new List<DragDrop>();
            dicGroupEffect = new Dictionary<int, GroupEffect>();
            currActiveEffect = null;
            OnAddedToCollection = null;
            OnRemovedFromCollection = null;
            diversity = 0;
            if (groupEffects ==null ||groupEffects.Length == 0)
            {
                return;
            }
            lowestDiversity = int.MaxValue;
            for (int i = 0; i < groupEffects.Length; i++)
            {
                dicGroupEffect.Add(groupEffects[i].neededDiversity, groupEffects[i]);
                if (groupEffects[i].neededDiversity < lowestDiversity)
                {
                    lowestDiversity = groupEffects[i].neededDiversity;
                }
            }
        }
        /// <summary>
        /// gets all the heroes of the collection and returns it as an array
        /// </summary>
        /// <returns></returns>
        public HeroData[] ToArray()
        {
            HeroData[] heroArray;
            int index = 0;
            int totalCount = 0;

            string[] keys = HeroesInCollection.Keys.ToArray();
            for (int i = 0; i < HeroesInCollection.Keys.Count; i++)
            {
               totalCount += HeroesInCollection[keys[i]].Count;
            }
            heroArray = new HeroData[totalCount];
            for (int i = 0; i < HeroesInCollection.Keys.Count; i++)
            {
                List<HeroData> hDatas = HeroesInCollection[keys[i]];
                for (int j = 0; j < hDatas.Count; j++)
                {
                    heroArray[index+j] = hDatas[j];
                }
                index += hDatas.Count;
            }
            return heroArray;
        }
    }

    /// <summary>
    /// data container for effects and the needed diversity to activate them
    /// </summary>
    [System.Serializable]
    public class GroupEffect
    {
        /// <summary>
        /// needed diversity to activate the effect
        /// </summary>
        public int neededDiversity;
        /// <summary>
        /// effect that gets activated
        /// </summary>
        public Effect effect;
        /// <summary>
        /// tooltip of the effect
        /// </summary>
        [TextArea] public string effectToolTip;
    }
}
